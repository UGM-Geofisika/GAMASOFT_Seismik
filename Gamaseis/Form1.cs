using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Unplugged.Segy;

namespace Gamaseis
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void addFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = addFileDialog.ShowDialog();
            if (dlg != DialogResult.OK) return;
            var filename = addFileDialog.FileName;
            var sgyReader = new SegyReader();
            var sgyFile = sgyReader.Read(filename);
            
            var sgyItem = new SegyContainer() {FileName = addFileDialog.SafeFileName, Data = sgyFile};
            listBoxFiles.Items.Add(sgyItem);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listBoxFiles.DisplayMember = "FileName";
            listBoxFiles.ValueMember = "Data";
        }

        private void tsbPlotWiggle_Click(object sender, EventArgs e)
        {
            Debug.Write("Clicked: ");
            Debug.WriteLine((sender as ToolStripButton).Name);
            // get the selected listbox items
            var segy = listBoxFiles.SelectedItem as SegyContainer;
            if (segy == null)
            {
                Debug.WriteLine("Null detected, returning..");
                return;
            }
            // grab the requested shots
            var myShots = new ShotGather();
            var myTraces = segy.Data.Traces.Where(trace => trace.Header.Fldr == 1).ToList();
            myShots.InitializeTraces(myTraces);

            var gathers = new List<ShotGather> { myShots };
            var wigglePlot = new SeismicPlot(gathers, PlotType.Wiggle) { };
            wigglePlot.Plot();
            wigglePlot.Show();
        }

        private void tsbPlotDensityGray_Click(object sender, EventArgs e)
        {
            Debug.Write("Clicked: ");
            Debug.WriteLine((sender as ToolStripButton).Name);
            // get the selected listbox items
            var segy = listBoxFiles.SelectedItem as SegyContainer;
            if (segy == null)
            {
                Debug.WriteLine("Null detected, returning..");
                return;
            }
            // grab the requested shots
            var myShots = new ShotGather();
            var myTraces = segy.Data.Traces.Where(trace => trace.Header.Fldr == 1).ToList();
            myShots.InitializeTraces(myTraces);

            var gathers = new List<ShotGather> { myShots };
            var densityPlot = new SeismicPlot(gathers, PlotType.DensityGray) { };
            densityPlot.Plot();
            densityPlot.Show();
        }

        private void plotDensityColor_Click(object sender, EventArgs e)
        {
            Debug.Write("Clicked: ");
            Debug.WriteLine((sender as ToolStripButton).Name);
            // get the selected listbox items
            var segy = listBoxFiles.SelectedItem as SegyContainer;
            if (segy == null)
            {
                Debug.WriteLine("Null detected, returning..");
                return;
            }
            // grab the requested shots
            var myShots = new ShotGather();
            var myTraces = segy.Data.Traces.Where(trace => trace.Header.Fldr == 1).ToList();
            myShots.InitializeTraces(myTraces);

            var gathers = new List<ShotGather> { myShots };
            var densityColor = new SeismicPlot(gathers, PlotType.DensityColor) { };
            densityColor.Plot();
            densityColor.Show();
        }

        private void tsbPlotFx_Click(object sender, EventArgs e)
        {
            Debug.Write("Clicked: ");
            Debug.WriteLine((sender as ToolStripButton).Name);
            // get the selected listbox items
            var segy = listBoxFiles.SelectedItem as SegyContainer;
            if (segy == null)
            {
                Debug.WriteLine("Null detected, returning..");
                return;
            }
            // grab the requested shots
            var myShots = new ShotGather();
            var myTraces = segy.Data.Traces.Where(trace => trace.Header.Fldr == 1).ToList();
            myShots.InitializeTraces(myTraces);

            var gathers = new List<ShotGather> { myShots };
            var fxPlot = new SeismicPlot(gathers, PlotType.Fx) { };
            fxPlot.Plot();
            fxPlot.Show();
        }

        private void tsbPlotFk_Click(object sender, EventArgs e)
        {
            Debug.Write("Clicked: ");
            Debug.WriteLine((sender as ToolStripButton).Name);
            // get the selected listbox items
            var segy = listBoxFiles.SelectedItem as SegyContainer;
            if (segy == null)
            {
                Debug.WriteLine("Null detected, returning..");
                return;
            }
            // grab the requested shots
            var myShots = new ShotGather();
            var myTraces = segy.Data.Traces.Where(trace => trace.Header.Fldr == 1).ToList();
            myShots.InitializeTraces(myTraces);

            var gathers = new List<ShotGather> { myShots };
            var fkPlot = new SeismicPlot(gathers, PlotType.Fk) { };
            fkPlot.Plot();
            fkPlot.Show();
        }

        private void tsbSourceElev_Click(object sender, EventArgs e)
        {
            Debug.Write("Clicked: ");
            Debug.WriteLine((sender as ToolStripButton).Name);
            // get the selected listbox items
            var segy = listBoxFiles.SelectedItem as SegyContainer;
            if (segy == null)
            {
                Debug.WriteLine("Null detected, returning..");
                return;
            }

            var myShots = new ShotGather();
            myShots.InitializeTraces(segy.Data.Traces);
            var gathers = new List<ShotGather> {myShots};
            var sourceElevPlot = new SeismicPlot(gathers,PlotType.SourceElevationInfo);
            sourceElevPlot.Plot();
            sourceElevPlot.Show();



        }

        private void tsbReceiverElev_Click(object sender, EventArgs e)
        {
            Debug.Write("Clicked: ");
            Debug.WriteLine((sender as ToolStripButton).Name);
            // get the selected listbox items
            var segy = listBoxFiles.SelectedItem as SegyContainer;
            if (segy == null)
            {
                Debug.WriteLine("Null detected, returning..");
                return;
            }

            var myShots = new ShotGather();
            myShots.InitializeTraces(segy.Data.Traces);
            var gathers = new List<ShotGather> { myShots };
            var sourceElevPlot = new SeismicPlot(gathers, PlotType.ReceiverElevationInfo);
            sourceElevPlot.Plot();
            sourceElevPlot.Show();
        }

        private void tsbTracePerGather_Click(object sender, EventArgs e)
        {
            Debug.Write("Clicked: ");
            Debug.WriteLine((sender as ToolStripButton).Name);
            // get the selected listbox items
            var segy = listBoxFiles.SelectedItem as SegyContainer;
            if (segy == null)
            {
                Debug.WriteLine("Null detected, returning..");
                return;
            }

            var myShots = new ShotGather();
            myShots.InitializeTraces(segy.Data.Traces);
            var gathers = new List<ShotGather> { myShots };
            var tracePerGather = new SeismicPlot(gathers, PlotType.TracePerGatherInfo);
            tracePerGather.Plot();
            tracePerGather.Show();
        }

        private void amplitudeCorrectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // get the selected listbox items
            var segy = listBoxFiles.SelectedItem as SegyContainer;
            if (segy == null)
            {
                Debug.WriteLine("Null detected, returning..");
                return;
            }
            // grab the requested shots
            var myShots = new ShotGather();
            var myTraces = segy.Data.Traces.Where(trace => trace.Header.Fldr == 1).ToList();
            myShots.InitializeTraces(myTraces);

            PreProcessing.ExpGainCorrection(myShots,16);
            //PreProcessing.TimePowerCorrection(myShots,0.0000000005f);

            var gathers = new List<ShotGather> { myShots };
            var densityPlot = new SeismicPlot(gathers, PlotType.DensityGray) { };
            densityPlot.Plot();
            densityPlot.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            // get the selected listbox items
            var segy = listBoxFiles.SelectedItem as SegyContainer;
            if (segy == null)
            {
                Debug.WriteLine("Null detected, returning..");
                return;
            }
            // grab the requested shots
            var myShots = new ShotGather();
            var myTraces = segy.Data.Traces.Where(trace => trace.Header.Fldr == 1).ToList();
            myShots.InitializeTraces(myTraces);

            var gather = new List<ShotGather> {myShots};
            var plot = new SeismicPlot(gather,PlotType.VelocityAnalysis);
            plot.Plot();
            plot.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Debug.Write("Clicked: ");
            Debug.WriteLine((sender as ToolStripButton).Name);
            // get the selected listbox items
            var segy = listBoxFiles.SelectedItem as SegyContainer;
            if (segy == null)
            {
                Debug.WriteLine("Null detected, returning..");
                return;
            }
            // grab the requested shots
            var myShots = new ShotGather();
            var myTraces = segy.Data.Traces.Where(trace => trace.Header.Fldr == 1).ToList();
            myShots.InitializeTraces(myTraces);

            var gathers = new List<ShotGather> { myShots };
            var NmoPlot = new SeismicPlot(gathers, PlotType.NmoPlot) { };
            NmoPlot.Plot();
            NmoPlot.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Debug.Write("Clicked: ");
            Debug.WriteLine((sender as ToolStripButton).Name);
            // get the selected listbox items
            var segy = listBoxFiles.SelectedItem as SegyContainer;
            if (segy == null)
            {
                Debug.WriteLine("Null detected, returning..");
                return;
            }
            // grab the requested shots
            var myShots = new ShotGather();
            var myTraces = segy.Data.Traces.Where(trace => trace.Header.Fldr == 1).ToList();
            myShots.InitializeTraces(myTraces);

            var gathers = new List<ShotGather> { myShots };
            var stackPlot = new SeismicPlot(gathers, PlotType.Stack) { };
            stackPlot.Plot();
            stackPlot.Show();
        }

        private void mutingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // get the selected listbox items
            var segy = listBoxFiles.SelectedItem as SegyContainer;
            if (segy == null)
            {
                Debug.WriteLine("Null detected, returning..");
                return;
            }
            // grab the requested shots
            var myShots = new ShotGather();
            var myTraces = segy.Data.Traces.Where(trace => trace.Header.Fldr == 1).ToList();
            myShots.InitializeTraces(myTraces);
            
            var coordinate = new float[2,2];
            coordinate[0, 0] = 0.100f; //time
            coordinate[1, 0] = 50f; //offset
            coordinate[0, 1] = 2.6f; //time
            coordinate[1, 1] = 3100f; //offset

            PreProcessing.Muting(myShots,coordinate);
            //PreProcessing.TimePowerCorrection(myShots,0.0000000005f);

            var gathers = new List<ShotGather> { myShots };
            var densityPlot = new SeismicPlot(gathers, PlotType.DensityGray) { };
            densityPlot.Plot();
            densityPlot.Show();
        }
    }
}
