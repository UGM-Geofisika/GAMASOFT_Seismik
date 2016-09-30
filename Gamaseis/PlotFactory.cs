using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.CompilerServices;
using ILNumerics;
using ILNumerics.Drawing;
using ILNumerics.Drawing.Plotting;

namespace Gamaseis
{
    public static class PlotFactory
    {
        public static ILPlotCube BuildSeismicPlot(ShotGather shot, PlotType type, string xtitle, string ytitle, Colormaps cmap = Colormaps.Jet)
        {
            var cube = BuildDefaultCube();
            switch (type)
            {
                case PlotType.Wiggle:
                    WigglePlot(shot, cube);
                    break;
                case PlotType.DensityGray:
                case PlotType.DensityColor:
                    DensityPlot(shot, cube, cmap);
                    break;
                case PlotType.Fk:
                    FkPlot(shot, cube, cmap);
                    break;
                case PlotType.Fx:
                    FxPlot(shot, cube, cmap);
                    break;
                case PlotType.VelocityAnalysis:
                    DemoVelocityAnalysis(shot,cube,cmap);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            SetYAxisOnTop(cube);
            cube.Axes.YAxis.Label.Text = ytitle;
            cube.Axes.XAxis.Label.Text = xtitle;

            return cube;
        }

        public static ILPlotCube BuildGeometryPlotInfo(ShotGather shot, PlotType type, string xtitle, string ytitle)
        {
            var cube = BuildDefaultCube();
            switch (type)
            {
                case PlotType.SourceElevationInfo:
                    SourceElevationPlot(cube, shot);
                    break;
                case PlotType.ReceiverElevationInfo:
                    ReceiverElevationPlot(cube, shot);
                    break;
                case PlotType.TracePerGatherInfo:
                    TracePerGatherPlot(cube, shot);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            cube.Axes.YAxis.Label.Text = ytitle;
            cube.Axes.XAxis.Label.Text = xtitle;

            return cube;
        }

        private static ILPlotCube BuildDefaultCube()
        {
            return new ILPlotCube("myPlotCube1")
            {
                // configure some axis label properties
                Axes =
                {
                    YAxis =
                    {
                        // place the label closer to the ticks
                        //LabelAnchor = new PointF(1, 0),
                        // configure the default style for the ticks
                        Ticks =
                        {
                            // Ticks and tick labels are auto-generated at runtime. 
                            // A DefaultLabel serves as template for their style.
                            DefaultLabel =
                            {
                                Font = new Font(ILLabel.DefaultFont, FontStyle.Bold),
                                Color = Color.Red
                            }
                        }
                    },
                    // let's explicitly disable the Z axis. Otherwise rounding errors might cause it to show up. 
                    ZAxis =
                    {
                        Visible = false
                    }
                }
            };
        }

        private static void SourceElevationPlot(ILPlotCube cube, ShotGather shots)
        {
            ILArray<float> sourceElev = shots.Traces.Select(trace => trace.Header.Selev).Select(dummy => (float) dummy).ToArray();
            var plot = new ILLinePlot(sourceElev);
            cube.Add(plot);
        }

        private static void ReceiverElevationPlot(ILPlotCube cube, ShotGather shots)
        {
            ILArray<float> receiverElev =
                shots.Traces.Select(trace => trace.Header.Selev).Select(x => (float) x).ToArray();
            var plot = new ILLinePlot(receiverElev);
            cube.Add(plot);
        }

        private static void TracePerGatherPlot(ILPlotCube cube, ShotGather shot)
        {
            ILArray<int> shotnumber = shot.Traces.Select(trace => trace.Header.Fldr).Distinct().ToArray();
            ILArray<int> countsPerGather = shotnumber.Select(i => shot.Traces.Count(trace => trace.Header.Fldr == i)).ToArray();
            //var plot2 = new ILBarPlot()
            var plot = new ILLinePlot(ILMath.tosingle(shotnumber),ILMath.tosingle(countsPerGather));
            plot.Marker.Style = MarkerStyle.Dot;
            plot.Line.DashStyle = DashStyle.Dashed;
            cube.Add(plot);
        }

        private static void FxPlot(ShotGather shot, ILPlotCube cube, Colormaps cmap)
        {
            ILArray<float> inArray = ILMath.zeros<float>(shot.Traces[0].Values.Count, shot.Traces.Count);

            for (var i = 0; i < shot.Traces.Count; i++)
            {
                ILArray<float> fval = shot.Traces[i].Values.ToArray();
                inArray[":", i] = fval;
            }

            // Perform 1D FFT and took only until positive frequency nyquist
            ILArray<float> fx = ILMath.abs(ILMath.fft(inArray, 0))[ILMath.r(0, ILMath.end / 2 + 1), ILMath.full];
            ILArray<float> offset = CreateOffsetArray(shot);
            ILArray<float> freqs = CreateFreqArray(shot, fx.Size[0]);
            var surf = new ILSurface(fx.T, freqs,offset, colormap: cmap);
            surf.Wireframe.Visible = false;
            cube.Add(surf);
        }

        private static void FkPlot(ShotGather shot, ILPlotCube cube, Colormaps cmap)
        {
            ILArray<float> inArray = ILMath.zeros<float>(shot.Traces[0].Values.Count, shot.Traces.Count);

            for (var i = 0; i < shot.Traces.Count; i++)
            {
                ILArray<float> fval = shot.Traces[i].Values.ToArray();
                inArray[":", i] = fval;
            }

            // Perform 2D FFT and took only until positive frequency nyquist
            ILArray<float> fk = ILMath.abs(ILMath.fft2(inArray))[ILMath.r(0, ILMath.end / 2+1), ILMath.full];
            ILArray<float> fkShifted = FftShift(fk);
            ILArray<float> waveNumber = CreateWaveNumberArray(shot, fkShifted.Size[1]);
            ILArray<float> freqs = CreateFreqArray(shot, fkShifted.Size[0]);
            
            var surf = new ILSurface(fkShifted.T,freqs,waveNumber, colormap:cmap);
            surf.Wireframe.Visible = false;
            cube.Add(surf);

            //cube.Axes.YAxis.ScaleLabel.
        }

        private static ILRetArray<float> FftShift(ILInArray<float> inArray)
        {
            using (ILScope.Enter(inArray))
            {
                if (inArray.IsMatrix)
                {
                    ILArray<float> fftShifted = ILMath.zeros<float>(inArray.Size[0], inArray.Size[1]);
                    // If the vector column length is even, we swapped equally
                    if (inArray.Size[1] %2 == 0)
                    {
                        fftShifted[ILMath.full, ILMath.r(0, ILMath.end/2)] =
                            inArray[ILMath.full, ILMath.r(ILMath.end/2 + 1, ILMath.end)];
                        fftShifted[ILMath.full, ILMath.r(ILMath.end/2 + 1, ILMath.end)] =
                            inArray[ILMath.full, ILMath.r(0, ILMath.end/2)];
                    }

                    // If the vector column length is odd, we put the first array in the center
                    else
                    {
                        fftShifted[ILMath.full, ILMath.r(0, ILMath.end/2 - 1)] =
                            inArray[ILMath.full, ILMath.r(ILMath.end/2 + 1, ILMath.end)];
                        fftShifted[ILMath.full, ILMath.r(ILMath.end/2, ILMath.end)] =
                            inArray[ILMath.full, ILMath.r(0, ILMath.end/2)];
                    }

                    return fftShifted;
                }
                else
                {
                    Debug.WriteLine("It is a vector");
                    ILArray<float> fftShifted = ILMath.zeros<float>(inArray.Size[0]);

                    // If the length is even, we swapped equally
                    if (inArray.Size[1] % 2 == 0)
                    {
                        fftShifted[ILMath.r(0, ILMath.end / 2)] =
                            inArray[ILMath.r(ILMath.end / 2 + 1, ILMath.end)];
                        fftShifted[ILMath.r(ILMath.end / 2 + 1, ILMath.end)] =
                            inArray[ILMath.r(0, ILMath.end / 2)];
                    }

                    // If the length is odd, we put the first array in the center
                    else
                    {
                        fftShifted[ILMath.r(0, ILMath.end / 2 - 1)] =
                            inArray[ILMath.r(ILMath.end / 2 + 1, ILMath.end)];
                        fftShifted[ILMath.r(ILMath.end / 2, ILMath.end)] =
                            inArray[ILMath.r(0, ILMath.end / 2)];
                    }

                    return fftShifted;


                }
            }
        } 
        private static void DensityPlot(ShotGather shot, ILPlotCube cube, Colormaps cmap)
        {
            var arr = shot.Traces.Select(trace => trace.Values.ToArray()).ToList();
            ILArray<float> ilArr = CreateRectangularArray(arr);
           
            // Y- the offset
            ILArray<float> offset = CreateOffsetArray(shot);
            var duration = shot.Traces[0].Header.Dt/1000f*shot.Traces[0].Header.SampleCount;

            // X- the time
            ILArray<float> time = ILMath.linspace<float>(0, duration, shot.Traces[0].Header.SampleCount);

            var surf = new ILSurface(ilArr.T, time, offset, colormap: cmap);
            surf.Wireframe.Visible = false;
            surf.DataRange = new Tuple<float, float>(-1.8f, 1.8f);

            cube.Add(surf);
        }

        private static void DemoVelocityAnalysis(ShotGather shot, ILPlotCube cube, Colormaps cmap)
        {
            var minVelocity = 1000f;
            var maxVelocity = 2000f;
            var velocityStep = 100;
            var result = VelocityAnalysis.PerformVelocityAnalysis(shot, minVelocity, maxVelocity, velocityStep, 200f);
            ILArray<float> ilArr = result;

            // Y- the offset
            ILArray<float> offset = ILMath.linspace<float>(minVelocity, maxVelocity, (int) (maxVelocity-minVelocity)/velocityStep);
            var duration = shot.Traces[0].Header.Dt / 1000f * shot.Traces[0].Header.SampleCount;

            // X- the time
            ILArray<float> time = ILMath.linspace<float>(0, duration, shot.Traces[0].Header.SampleCount);

            var surf = new ILSurface(ilArr.T,time,offset,colormap:cmap);
            surf.Wireframe.Visible = false;

            cube.Add(surf);
        }

        private static void WigglePlot(ShotGather shot, ILPlotCube cube)
        {
            foreach (var trace in shot.Traces)
            {
                var fval = trace.Values.ToArray();
                ILArray<float> traceData = fval;
                traceData *= shot.Traces[0].Header.Offset;
                traceData += trace.Header.Offset;
                var linePlot = new ILLinePlot(traceData);
                linePlot.Line.Color = Color.Blue;
                cube.Add(linePlot);
            }
        }

        public static T[,] CreateRectangularArray<T>(IList<T[]> arrays)
        {
            // TODO: Validation and special-casing for arrays.Count == 0
            // thank you John Skeet
            var minorLength = arrays[0].Length;
            var ret = new T[arrays.Count, minorLength];
            for (var i = 0; i < arrays.Count; i++)
            {
                var array = arrays[i];
                if (array.Length != minorLength)
                {
                    throw new ArgumentException
                        ("All arrays must be the same length");
                }
                for (var j = 0; j < minorLength; j++)
                {
                    ret[i, j] = array[j];
                }
            }
            return ret;
        }

       

        private static ILRetArray<float> CreateOffsetArray(ShotGather shot)
        {
            using (ILScope.Enter())
            {
                // Y- the offset
                IList<float> listOffset =
                    shot.Traces.Select(trace => trace.Header.Offset).Select(dummy => (float)dummy).ToList();
                ILArray<float> offset = listOffset.ToArray();
                return offset;
            }
        }

        private static ILRetArray<float> CreateFreqArray(ShotGather shot, int n)
        {
            using (ILScope.Enter())
            {
                var dt = shot.Traces[0].Header.Dt; //in microseconds
                var nyquist = 0.5/(dt/10e6);
                ILArray<float> freqs = ILMath.linspace<float>(0, nyquist, n);
                return freqs;
            }
        }
        private static ILRetArray<float> CreateWaveNumberArray(ShotGather shot, int n)
        {
            using (ILScope.Enter())
            {
                var dx = shot.Traces[0].Header.Offset;
                var nyquist = 0.5/dx;
                ILArray<float> waveNumber = ILMath.zeros<float>(1,n);
                Debug.WriteLine(waveNumber);
                ILArray<float> positiveWn = ILMath.linspace<float>(0, nyquist, n/2 + 1);
                Debug.WriteLine(positiveWn);
                ILArray<float> negativeWn = ILMath.linspace<float>(-nyquist, 0, n/2 + 1);
                Debug.WriteLine(negativeWn);
                waveNumber[ILMath.r(0,ILMath.end/2+1)] =
                positiveWn;
                waveNumber[ILMath.r(ILMath.end/2 + 2, ILMath.end)] = negativeWn[ILMath.r(0, ILMath.end - 2)];
                Debug.WriteLine(waveNumber);
                return ILMath.sort(waveNumber);
            }
        }
        private static void SetYAxisOnTop(ILPlotCube cube)
        {
            if (cube == null) return;
            // reset view (zoom fully out and center)
            cube.Reset();

            // transform the plot cube so that y axis is on horizontal
            cube.Transform = Matrix4.Rotation(new Vector3(0, 0, 1), ILMath.pif/2);

            // Place the YAxis Axes to top
            cube.Axes.YAxis.Position = new Vector3(0, 0, 1);
        }
    }

    public enum PlotType
    {
        Wiggle,
        DensityGray,
        DensityColor,
        Fx,
        Fk,
        SourceElevationInfo,
        ReceiverElevationInfo,
        TracePerGatherInfo,
        VelocityAnalysis
    }
}