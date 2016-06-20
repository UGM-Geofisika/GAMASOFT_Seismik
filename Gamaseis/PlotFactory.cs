using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ILNumerics;
using ILNumerics.Drawing;
using ILNumerics.Drawing.Plotting;

namespace Gamaseis
{
    public static class PlotFactory
    {
        public static ILPlotCube BuildDefaultCube()
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

        public static void BuildSeismicPlot(this ILPlotCube cube, ShotGather shot, PlotType type)
        {
            switch (type)
            {
                case PlotType.Wiggle:
                    WigglePlot(shot,cube);
                    break;
                case PlotType.Density:
                    DensityPlot(shot, cube);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private static void DensityPlot(ShotGather shot, ILPlotCube cube)
        {
            var arr = shot.Traces.Select(trace => trace.Values.ToArray()).ToList();
            ILArray<float> ilArr = CreateRectangularArray(arr);

            var surf = new ILSurface(ilArr.T,colormap: Colormaps.Gray);
            surf.Wireframe.Visible = false;
            surf.DataRange = new Tuple<float, float>(-1.8f,1.8f);
            
            cube.Add(surf);
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

        public static void SetYAxisOnTop(this ILPlotCube cube)
        {
            if (cube == null) return;
            // reset view (zoom fully out and center)
            cube.Reset();

            // transform the plot cube so that y axis is on horizontal
            cube.Transform = Matrix4.Rotation(new Vector3(0, 0, 1), ILMath.pif / 2);

            // Place the YAxis Axes to top
            cube.Axes.YAxis.Position = new Vector3(0, 0, 1);
        }
    }

    public enum PlotType
    {
        Wiggle,
        Density
    }
}
