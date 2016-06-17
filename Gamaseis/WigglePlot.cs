using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ILNumerics;
using ILNumerics.Drawing;
using ILNumerics.Drawing.Plotting;

namespace Gamaseis
{
    public partial class WigglePlot : Form, IPLottingForm
    {
        public IList<ShotGather> Shots { get; }
        public WigglePlot(IEnumerable<ShotGather> shots)
        {
            InitializeComponent();
            Shots = shots as IList<ShotGather>;
        }
        public void Plot()
        {
            BuildPlotCube();
            PlotTraces();
            YAxisOnTop();

        }
        public void Update(IEnumerable<ShotGather> shots)
        {
            throw new NotImplementedException();
        }

        protected void BuildPlotCube()
        {
            ilPanel1.Scene.Add(new ILPlotCube("myPlotCube1")
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
            });
        }

        protected void YAxisOnTop()
        {
            var plotcube = ilPanel1.Scene.First<ILPlotCube>("myPlotCube1");
            if (plotcube != null)
            {
                // reset view (zoom fully out and center)
                plotcube.Reset();

                // transform the plot cube so that y axis is on horizontal
                plotcube.Transform = Matrix4.Rotation(new Vector3(0, 0, 1), ILMath.pif/2);

                // Place the YAxis Axes to top
                plotcube.Axes.YAxis.Position = new Vector3(0, 0, 1);
            }
        }

        protected void PlotTraces()
        {
            // first for demo purposes, extract the first gather
            var shot = Shots[0];
            var cube = ilPanel1.Scene.First<ILPlotCube>("myPlotCube1");

            foreach (var trace in shot.Traces)
            {
                var fval = trace.Values.ToArray();
                ILArray<float> traceData = fval;
                traceData *= shot.Traces[0].Header.Offset;
                traceData += trace.Header.Offset;
                var linePlot = new ILLinePlot(traceData);
                linePlot.Line.Color = Color.Blue;
                cube.Children.Add(linePlot);
            }

        }

        private class SynthethicGenerator : ILMath
        {
            public static ILRetArray<double> BuildSingleTrace()
            {
                using (ILScope.Enter())
                {
                    ILArray<double> x = linspace<double>(0, 100, 100);
                    Debug.WriteLine(x.ToString());
                    ILArray<double> singleTrace = sin(x); //+ rand(1, x.S[1]) * 0.5;

                    return singleTrace;
                }
            }
        }
    }
}