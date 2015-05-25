using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
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

            // Open Dummy Image (for test purpose only)
            GamaSeismicViewer.ShowDummyImages(picBox1);
            GamaSeismicViewer.Image_Axis_Initialize(picBox1, panelX, panelY, panelImage, panelGap);

            picBox1.MouseWheel += picBox1_MouseWheel;
        }

        private void openToolStripButton_Click_1(object sender, EventArgs e)
        {
            var dlgresult = openFileDialog1.ShowDialog();

            if (dlgresult != DialogResult.OK) return;

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
            DisplaySeismicSection(_segyFile);
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

        private void DisplaySeismicSection(ISegyFile segy)
        {
            picBox1.Image = SEGYView.SegyView.GetAllTracesBitmap(segy);
        }

        private void panelImage_Scroll(object sender, ScrollEventArgs e)
        {
            GamaSeismicViewer.Image_Axis_Update(picBox1, panelX, panelY, panelImage, panelGap);
        }

        private void picBox1_MouseMove(object sender, MouseEventArgs e)
        {
            picBox1.Focus();
            GamaSeismicViewer.Image_ValueOnHover(panelGap, panelY, picBox1, MousePosition);

            if (GamaSeismicViewer.FPan)
            {
                GamaSeismicViewer.Image_Pan(panelImage, MousePosition);
                GamaSeismicViewer.Image_Axis_Update(picBox1, panelX, panelY, panelImage, panelGap);
            }
        }

        private void picBox1_MouseDown(object sender, MouseEventArgs e)
        {
            // if left mouse is clicked, activate pan mode
            if (MouseButtons == MouseButtons.Left)
            {
                GamaSeismicViewer.FPan = true;
                GamaSeismicViewer.PanStartMouse = panelImage.PointToClient(MousePosition);
                Cursor.Current = Cursors.Hand;

                if (panelImage.HorizontalScroll.Visible)
                    GamaSeismicViewer.PanStartHScroll = panelImage.HorizontalScroll.Value;
                if (panelImage.VerticalScroll.Visible)
                    GamaSeismicViewer.PanStartVScroll = panelImage.VerticalScroll.Value;
            }
        }

        private void picBox1_MouseUp(object sender, MouseEventArgs e)
        {
            GamaSeismicViewer.FPan = false;
            Cursor.Current = Cursors.Default;
        }

        private void picBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            GamaSeismicViewer.ZoomFactor = GamaSeismicViewer.ZoomFactor + 10*(e.Delta/120);
            if (GamaSeismicViewer.ZoomFactor < 10)
                GamaSeismicViewer.ZoomFactor = 10;
            if (GamaSeismicViewer.ZoomFactor > 500)
                GamaSeismicViewer.ZoomFactor = 500;

            GamaSeismicViewer.PicStart = picBox1.PointToClient(MousePosition);
            GamaSeismicViewer.Image_MouseCenteredZoom((int)GamaSeismicViewer.ZoomFactor, picBox1, panelImage, MousePosition);
            GamaSeismicViewer.Image_Axis_Update(picBox1, panelX, panelY, panelImage, panelGap);
        }

        private void panelX_MouseDown(object sender, MouseEventArgs e)
        {
            if (MouseButtons == MouseButtons.Left)
            {
                GamaSeismicViewer.FScaleX = true;
                GamaSeismicViewer.ScaleMouse0 = panelX.PointToClient(MousePosition);
            }
        }

        private void panelX_MouseMove(object sender, MouseEventArgs e)
        {
            GamaSeismicViewer.Image_Axis_StretchShrink(picBox1, panelX, panelY, panelImage, MousePosition);
            GamaSeismicViewer.Image_Axis_Update(picBox1, panelX, panelY, panelImage, panelGap);
        }

        private void panelX_MouseUp(object sender, MouseEventArgs e)
        {
            GamaSeismicViewer.FScaleX = false;
        }

        private void panelY_MouseDown(object sender, MouseEventArgs e)
        {
            if (MouseButtons == MouseButtons.Left)
            {
                GamaSeismicViewer.FScaleY = true;
                GamaSeismicViewer.ScaleMouse0 = panelY.PointToClient(MousePosition);
            }
        }

        private void panelY_MouseMove(object sender, MouseEventArgs e)
        {
            GamaSeismicViewer.Image_Axis_StretchShrink(picBox1, panelX, panelY, panelImage, MousePosition);
            GamaSeismicViewer.Image_Axis_Update(picBox1, panelX, panelY, panelImage, panelGap);
        }

        private void panelY_MouseUp(object sender, MouseEventArgs e)
        {
            GamaSeismicViewer.FScaleY = false;
        }

        private void SEGYDisplay_Resize(object sender, EventArgs e)
        {
            GamaSeismicViewer.Image_Axis_Update(picBox1, panelX, panelY, panelImage, panelGap);
        }
    }
}