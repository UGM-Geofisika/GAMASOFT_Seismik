using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using ILNumerics;
using ILNumerics.Drawing;
using ILNumerics.Drawing.Plotting;

namespace Gamaseis
{
    public static class PlotFactory
    {
        public static ILPlotCube BuildSeismicPlot(ShotGather shot, PlotType type, string xtitle, string ytitle)
        {
            var cube = BuildDefaultCube();
            switch (type)
            {
                case PlotType.Wiggle:
                    WigglePlot(shot, cube);
                    break;
                case PlotType.Density:
                    DensityPlot(shot, cube);
                    break;
                case PlotType.Fk:
                    FkPlot(shot, cube);
                    break;
                case PlotType.Fx:
                    FxPlot(shot, cube);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            SetYAxisOnTop(cube);
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

        private static void FxPlot(ShotGather shot, ILPlotCube cube)
        {
            ILArray<float> inArray = ILMath.zeros<float>(shot.Traces[0].Values.Count, shot.Traces.Count);

            for (var i = 0; i < shot.Traces.Count; i++)
            {
                ILArray<float> fval = shot.Traces[i].Values.ToArray();
                inArray[":", i] = fval;
            }

            ILArray<float> fx = ILMath.abs(ILMath.fft(inArray, 0))[ILMath.r(0, ILMath.end / 2 + 1), ILMath.full];
            var surf = new ILSurface(fx.T, colormap: Colormaps.Jet);
            surf.Wireframe.Visible = false;
            cube.Add(surf);
        }

        private static void FkPlot(ShotGather shot, ILPlotCube cube)
        {
            ILArray<float> inArray = ILMath.zeros<float>(11, 11);

            for (int i = 0; i < inArray.Size[1]; i++)
            {
                inArray[":", i] = i;
            }
            Debug.WriteLine(inArray);
        }

        //        private static void FkPlot(ShotGather shot, ILPlotCube cube)
        //        {
        ////            ILArray<float> fk = ILMath.zeros<float>(shot.Traces[0].Values.Count, shot.Traces.Count);
        ////
        ////            for (var i = 0; i < shot.Traces.Count; i++)
        ////            {
        ////                ILArray<float> fval = shot.Traces[i].Values.ToArray();
        ////                fk[":", i] = fval;
        ////            }
        //
        //            // Dummy
        //            ILArray<float> inArray = ILMath.zeros<float>(11, 11);
        //            inArray[0, ":"] = 1.00f;
        //
        //            Debug.WriteLine("Fk");
        //            Debug.WriteLine(inArray);
        //
        //            ILArray<float> fk = ILMath.abs(ILMath.fft2(inArray))[ILMath.r(0, ILMath.end/2), ILMath.full];
        //            Debug.WriteLine(fk[ILMath.full, ILMath.r(ILMath.end / 2 + 1, ILMath.end)]);
        //            //Now we do swapping so the zero wavenumber is at center
        //            ILArray<float> result = ILMath.zeros<float>(fk.Size[0],fk.Size[1]);
        //            
        //
        ////            Debug.WriteLine("Result of FFT2D, Shifted to center");
        ////            result[ILMath.full, ILMath.r(0, ILMath.end/2)] = fk[ILMath.full, ILMath.r(ILMath.end/2 + 1, ILMath.end)];
        ////            result[ILMath.full, ILMath.r(ILMath.end/2+1, ILMath.end)] = fk[ILMath.full, ILMath.r(0, ILMath.end/2)];
        ////            Debug.WriteLine(result);
        ////
        ////            var surf = new ILSurface(result.T, colormap: Colormaps.Jet);
        ////            surf.Wireframe.Visible = false;
        ////            cube.Add(surf);
        //        }

        private static void DensityPlot(ShotGather shot, ILPlotCube cube)
        {
            var arr = shot.Traces.Select(trace => trace.Values.ToArray()).ToList();
            ILArray<float> ilArr = CreateRectangularArray(arr);

            // Y- the offset
            IList<float> listOffset =
                shot.Traces.Select(trace => trace.Header.Offset).Select(dummy => (float) dummy).ToList();
            ILArray<float> offset = listOffset.ToArray();
            var duration = shot.Traces[0].Header.Dt/1000f*shot.Traces[0].Header.SampleCount;
            ILArray<float> time = ILMath.linspace<float>(0, duration, shot.Traces[0].Header.SampleCount);

            var surf = new ILSurface(ilArr.T, time, offset, colormap: Colormaps.Gray);
            surf.Wireframe.Visible = false;
            surf.DataRange = new Tuple<float, float>(-1.8f, 1.8f);

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

        private static T[,] CreateRectangularArray<T>(IList<T[]> arrays)
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
        Density,
        Fx,
        Fk
    }
}