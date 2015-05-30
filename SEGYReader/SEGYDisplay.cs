using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using Antiufo.Controls;
using Unplugged.Segy;

namespace SegyView
{
    public partial class SEGYDisplay : Form
    {
        private ISegyFile _segyFile;

        public SEGYDisplay()
        {
            InitializeComponent();
        }

        private void SEGYDisplay_Load(object sender, EventArgs e)
        {
            menuStrip1.Renderer = Windows7Renderer.Instance;
            toolStrip1.Renderer = Windows7Renderer.Instance;
            GamaSeismicViewer.Setup(picBox1,panelX,panelY, panelImage, panelGap);
            GamaFileViewer.Setup(treeView1);
            GamaSeismicColor.Setup(picBox1, picColorScale);
            picBox1.MouseWheel += picBox1_MouseWheel;
            treeView1.NodeMouseClick += TreeView_SelectItem;

            // disable control panel in Seismic Viewer
            panSeismicMenu.Enabled = false;

            // prepare colormap
            GamaSeismicColor.Colormap_BlueWhiteRed();  
            //GamaSeismicColor.Colormap_Grayscale();
            //GamaSeismicColor.Colormap_Rainbow();
            GamaSeismicColor.UpdateColormapBitmap();

        }

        private void openToolStripButton_Click_1(object sender, EventArgs e)
        {
            var dlgresult = openFileDialog1.ShowDialog();

            if (dlgresult != DialogResult.OK) return;

            // load selected directory
            GamaFileViewer.LoadDirectory(openFileDialog1.FileName);

            // load selected segy file
            _segyFile = SEGYView.SegyView.Read(openFileDialog1.FileName);

            Debug.WriteLine(_segyFile.Traces.Count);
            Debug.WriteLine(_segyFile.Traces.ToList().First().Values.Count);
            Debug.Write("Job Number: ");
            Debug.WriteLine(_segyFile.Header.BinaryHeader.JobNumber);
            Debug.Write("Sample Interval Reel: ");
            Debug.WriteLine(_segyFile.Header.BinaryHeader.SampleIntervalReel);
            Debug.Write("Sample Interval Field: ");
            Debug.WriteLine(_segyFile.Header.BinaryHeader.SampleIntervalField);

            ExtractSegyHeaderInfo(_segyFile);
            DisplaySeismicSection(_segyFile, _segyFile.Header.BinaryHeader.NDataPerTraceReel, _segyFile.Header.BinaryHeader.SampleIntervalReel);

            GamaFileViewer.fileURLNowOpened = openFileDialog1.FileName;
        }

        private void ExtractSegyHeaderInfo(ISegyFile segy)
        {
            txtHeaderText.Text = segy.Header.Text;
            var headerInfo = segy.Header.BinaryHeader;

            var headerItems = new List<SegyHeaderItem>
            {
                new SegyHeaderItem("Job identification number.", headerInfo.JobNumber, "3201 - 3204"),
                new SegyHeaderItem("Line number.", headerInfo.LineNumber, "3205 - 3208"),
                new SegyHeaderItem("Reel number.", headerInfo.ReelNumber, "3209 - 3212"),
                new SegyHeaderItem("Number of data traces per record.", headerInfo.NDataTracesPerRecord, "3213 - 3214"),
                new SegyHeaderItem("Number of auxiliary traces per record.", headerInfo.NAuxTracesPerRecord,
                    "3215 - 3216"),
                new SegyHeaderItem("Sample interval, microseconds, this file (reel).", headerInfo.SampleIntervalReel,
                    "3217 - 3218"),
                new SegyHeaderItem("Sample interval, microseconds, original field recording.",
                    headerInfo.SampleIntervalField, "3219 - 3220"),
                new SegyHeaderItem("Number of samples per data trace, this file (reel).", headerInfo.NDataPerTraceReel,
                    "3221 - 3222"),
                new SegyHeaderItem("Number of samples per data trace, original field recording.",
                    headerInfo.NDataPerTraceField, "3223 - 3224"),
                new SegyHeaderItem("Data sample format code.", headerInfo.SampleFormatCode, "3225 - 3226"),
                new SegyHeaderItem("CDP fold.", headerInfo.CdpFold, "3227 - 3228"),
                new SegyHeaderItem("Trace sorting code.", headerInfo.TraceSortingCode, "3229 - 3230"),
                new SegyHeaderItem("Vertical sum code..", headerInfo.VerticalSumCode, "3231 - 3232"),
                new SegyHeaderItem("Sweep frequency at start.", headerInfo.SweepFreqStart, "3233 - 3234"),
                new SegyHeaderItem("Sweep frequency at end.", headerInfo.SweepFreqEnd, "3235 - 3236"),
                new SegyHeaderItem("Sweep length, ms.", headerInfo.SweepLength, "3237 - 3238"),
                new SegyHeaderItem("Sweep type code.", headerInfo.SweepTypeCode, "3239 - 3240"),
                new SegyHeaderItem("Trace number of sweep channel.", headerInfo.TraceNumberOfSweepChannel, "3241 - 3242"),
                new SegyHeaderItem("Sweep trace taper length, ms, at start if tapered.",
                    headerInfo.SweepTraceTaperLengthEnd, "3243 - 3244"),
                new SegyHeaderItem("Sweep trace taper length, ms, at end.", headerInfo.SweepTraceTaperLengthEnd,
                    "3245 - 3246"),
                new SegyHeaderItem("Taper type.", headerInfo.TaperType, "3247 - 3248"),
                new SegyHeaderItem("Correlated data traces.", headerInfo.CorrelatedDataTraces, "3249 - 3250"),
                new SegyHeaderItem("Binary gain recovered.", headerInfo.BinaryGainRecovered, "3251 - 3252"),
                new SegyHeaderItem("Amplitude recovery method.", headerInfo.AmplitudeRecoveryMethod, "3253 - 3254"),
                new SegyHeaderItem("Measurement system.", headerInfo.MeasurementSystem, "3255 - 3256"),
                new SegyHeaderItem("Impulse signal.", headerInfo.ImpulseSignal, "3257 - 3258"),
                new SegyHeaderItem("Vibratory polarity code.", headerInfo.VibratoryPolarityCode, "3259 - 3260")
            };

            dataGridView1.DataSource = headerItems;

            foreach (var segyHeaderItem in headerItems)
            {
                Debug.WriteLine("{0}\t{1}\t{2}", segyHeaderItem.Description, segyHeaderItem.Value,
                    segyHeaderItem.BytePosition);
            }
        }

        private void DisplaySeismicSection(ISegyFile segy, int tracecount, int timeInterval)
        {
            var maxtime = tracecount*(timeInterval/1000);

            // display seismic section
            GamaSeismicViewer.ShowSeismic(SEGYView.SegyView.GetAllTracesBitmap(segy), tracecount, maxtime);
            GamaSeismicViewer.Image_Axis_Update();

            panSeismicMenu.Enabled = true; panSeismicMenu.Update();
            lblZoom.Text = String.Concat(GamaSeismicViewer.ZoomFactor, " %"); lblZoom.Update();
        }

        private void TreeView_SelectItem(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Name.EndsWith(GamaFileViewer.fileExt) & (e.Node.Name != GamaFileViewer.fileURLNowOpened))
            {
               // load selected segy file
                _segyFile = SEGYView.SegyView.Read(e.Node.Name);

                Debug.WriteLine(_segyFile.Traces.Count);
                Debug.WriteLine(_segyFile.Traces.ToList().First().Values.Count);
                Debug.Write("Job Number: ");
                Debug.WriteLine(_segyFile.Header.BinaryHeader.JobNumber);
                Debug.Write("Sample Interval Reel: ");
                Debug.WriteLine(_segyFile.Header.BinaryHeader.SampleIntervalReel);
                Debug.Write("Sample Interval Field: ");
                Debug.WriteLine(_segyFile.Header.BinaryHeader.SampleIntervalField);

                ExtractSegyHeaderInfo(_segyFile);
                DisplaySeismicSection(_segyFile, _segyFile.Header.BinaryHeader.NDataPerTraceReel, _segyFile.Header.BinaryHeader.SampleIntervalReel);

                GamaFileViewer.fileURLNowOpened = e.Node.Name;
            }
        }

        
                     
    }
}