using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using ILNumerics;
using ILNumerics.Drawing;
using ILNumerics.Drawing.Plotting;

namespace Gamaseis
{
    public partial class WigglePlot : Form
    {
        public WigglePlot()
        {
            InitializeComponent();
        }

        private void WigglePlot_Load(object sender, EventArgs e)
        {
            ILArray<float> trace = ILMath.tosingle(SynthethicGenerator.BuildSingleTrace());
            ilPanel1.Scene.Add(new ILPlotCube("myPlotCube1")
            {
                Children = {
                    new ILLinePlot(ILMath.tosingle(trace)),
                },

                // configure some axis label properties
                Axes = {
                    YAxis = {
                        // place the label closer to the ticks
                        //LabelAnchor = new PointF(1, 0),
                        // configure the default style for the ticks
                        Ticks = {
                            // Ticks and tick labels are auto-generated at runtime. 
                            // A DefaultLabel serves as template for their style.
                            DefaultLabel = {
                                Font = new Font(ILLabel.DefaultFont, FontStyle.Bold),
                                Color = Color.Red,
                            }
                        }
                    },
                    // let's explicitly disable the Z axis. Otherwise rounding errors might cause it to show up. 
                    ZAxis = {
                        Visible = false,
                    }
                }
            });

            

            ReverseYAxis();
        }

        protected void ReverseYAxis()
        {
            var plotcube = ilPanel1.Scene.First<ILPlotCube>("myPlotCube1");
            if (plotcube != null)
            {
                // reset view (zoom fully out and center)
                plotcube.Reset();
                // rotate the plotcube so the Y axis appears in top down direction
                // we need to move the plotcube back or the rotation would rotate the y axis labels out of the viewing frustum
                // since the origin lays in (0,0,0) of the plotcube.
                plotcube.Transform = Matrix4.Rotation(new Vector3(0,0,1),ILMath.pif/2);
                plotcube.Axes.YAxis.Position = new Vector3(0,0,1);
            }
        }

        private class SynthethicGenerator : ILMath
        {
            public static ILRetArray<double> BuildSingleTrace()
            {
                using (ILScope.Enter())
                {
                    ILArray<double> x = linspace<double>(0, 100,100);
                    Debug.WriteLine(x.ToString());
                    ILArray<double> singleTrace = sin(x);//+ rand(1, x.S[1]) * 0.5;

                    return singleTrace;
                }
            } 
        }
    }
}
