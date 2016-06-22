using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ILNumerics.Drawing;
using ILNumerics.Drawing.Plotting;

namespace Gamaseis
{
    public partial class SeismicPlot : Form, IPLottingForm
    {
        private readonly PlotType _type;
        public IList<ShotGather> Shots { get; }
        public SeismicPlot(IList<ShotGather> shots, PlotType type)
        {
            InitializeComponent();
            Shots = shots;
            _type = type;
        }
        public void Plot()
        {
            ILPlotCube cube;
            switch (_type)
            {
                case PlotType.Wiggle:
                    Text = @"Wiggle Plot";
                    cube = PlotFactory.BuildSeismicPlot(Shots[0], _type, "Time (ms)", "Offset (m)");
                    break;
                case PlotType.DensityColor:
                    Text = @"Density Color Plot";
                    cube = PlotFactory.BuildSeismicPlot(Shots[0], _type, "Time (ms)", "Offset (m)");
                    break;
                    case PlotType.DensityGray:
                    Text = @"Density Gray Plot";
                    cube = PlotFactory.BuildSeismicPlot(Shots[0], _type, "Time (ms)", "Offset (m)", Colormaps.Gray);
                    break;
                case PlotType.Fx:
                    Text = @"F-x Plot";
                    cube = PlotFactory.BuildSeismicPlot(Shots[0], _type, "Frequency (Hz)", "Offset (m)");
                    break;
                case PlotType.Fk:
                    Text = @"F-k Plot";
                    cube = PlotFactory.BuildSeismicPlot(Shots[0], _type, "Frequency (Hz)", "Wavenumber (k)");
                    break;
                case PlotType.SourceElevationInfo:
                    Text = @"Source Elevation";
                    cube = PlotFactory.BuildGeometryPlotInfo(Shots[0], _type, "Number of Traces", "Elevation (m)");
                    break;
                case PlotType.ReceiverElevationInfo:
                    Text = @"Receiver Elevation";
                    cube = PlotFactory.BuildGeometryPlotInfo(Shots[0], _type, "Number of Traces", "Elevation (m)");
                    break;
                case PlotType.TracePerGatherInfo:
                    Text = @"Number of traces per shot";
                    cube = PlotFactory.BuildGeometryPlotInfo(Shots[0], _type, "Shot Number", "Number of traces");
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            ilPanel1.Scene.Add(cube);
        }

        public void Update(IEnumerable<ShotGather> shots)
        {
            throw new NotImplementedException();
        }
    }
}