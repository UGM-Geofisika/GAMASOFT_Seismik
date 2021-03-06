﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Antiufo.Controls;
using Unplugged.Segy;

namespace Gamaseis
{
    public partial class FrontPage : Form
    {
        public static ISegyFile SEGYFile4Bmp;
        private ISegyFile _segyFile;
        // initialize other forms
        public ColormapEditor FrmClrEdit = new ColormapEditor();

        public FrontPage()
        {
            InitializeComponent();
        }

        private void SEGYDisplay_Load(object sender, EventArgs e)
        {
            menuStrip1.Renderer = Windows7Renderer.Instance;
            toolStrip1.Renderer = Windows7Renderer.Instance;

            // initialize classes
            SeismicViewer.Setup(picBox1, panelX, panelY, panelImage, panelGap);
            SeismicFileManager.Setup(treeView1);
            SeismicColorMap.Setup(picBox1, picColorScale);
            SeismicColormapEditor.Setup(FrmClrEdit, FrmClrEdit.picColorScale, FrmClrEdit.dgvColormap,
                FrmClrEdit.panPickerHue, FrmClrEdit.panPickerSV, FrmClrEdit.picPickerHueLine,
                FrmClrEdit.comboPreset, FrmClrEdit.dgvPickColor, FrmClrEdit.picCurClr,
                FrmClrEdit.picNewClr, FrmClrEdit.panMarkerIsSelected);
            picBox1.MouseWheel += picBox1_MouseWheel;
            treeView1.NodeMouseClick += TreeView_SelectItem;

            // disable control panel in Seismic Viewer
            panSeismicMenu.Enabled = false;

            // prepare colormap
            SeismicColorMap.Colormap_BlackWhiteRed();
            SeismicColorMap.UpdateColormapBitmap();

            // set gain value to default
            ImageWriter.SetGainRangeToDefault(true, true);
        }

        // File Open Handler
        private void openToolStripButton_Click_1(object sender, EventArgs e)
        {
            var dlgresult = openFileDialog1.ShowDialog();

            if (dlgresult != DialogResult.OK)
            {
                SeismicViewer.SetScrollbarValue();
                return;
            }

            // load selected directory
            SeismicFileManager.LoadDirectory(openFileDialog1.FileName);

            // load selected segy file
            readerWorker.RunWorkerAsync(openFileDialog1.FileName);

            SeismicFileManager.FileUrlNowOpened = openFileDialog1.FileName;
            SeismicViewer.SetScrollbarValue();
        }

        private void ExtractSegyFileHeaderInfo(ISegyFile segy)
        {
            txtHeaderText.Text = segy.Header.Text;
            var headerInfo = segy.Header.BinaryHeader;

            var headerItems = new List<SegyFileHeaderItem>
            {
                new SegyFileHeaderItem("Job identification number.", headerInfo.JobNumber, "3201 - 3204"),
                new SegyFileHeaderItem("Line number.", headerInfo.LineNumber, "3205 - 3208"),
                new SegyFileHeaderItem("Reel number.", headerInfo.ReelNumber, "3209 - 3212"),
                new SegyFileHeaderItem("Number of data traces per record.", headerInfo.NDataTracesPerRecord,
                    "3213 - 3214"),
                new SegyFileHeaderItem("Number of auxiliary traces per record.", headerInfo.NAuxTracesPerRecord,
                    "3215 - 3216"),
                new SegyFileHeaderItem("Sample interval, microseconds, this file (reel).", headerInfo.SampleIntervalReel,
                    "3217 - 3218"),
                new SegyFileHeaderItem("Sample interval, microseconds, original field recording.",
                    headerInfo.SampleIntervalField, "3219 - 3220"),
                new SegyFileHeaderItem("Number of samples per data trace, this file (reel).",
                    headerInfo.NDataPerTraceReel,
                    "3221 - 3222"),
                new SegyFileHeaderItem("Number of samples per data trace, original field recording.",
                    headerInfo.NDataPerTraceField, "3223 - 3224"),
                new SegyFileHeaderItem("Data sample format code.", headerInfo.SampleFormatCode, "3225 - 3226"),
                new SegyFileHeaderItem("CDP fold.", headerInfo.CdpFold, "3227 - 3228"),
                new SegyFileHeaderItem("Trace sorting code.", headerInfo.TraceSortingCode, "3229 - 3230"),
                new SegyFileHeaderItem("Vertical sum code..", headerInfo.VerticalSumCode, "3231 - 3232"),
                new SegyFileHeaderItem("Sweep frequency at start.", headerInfo.SweepFreqStart, "3233 - 3234"),
                new SegyFileHeaderItem("Sweep frequency at end.", headerInfo.SweepFreqEnd, "3235 - 3236"),
                new SegyFileHeaderItem("Sweep length, ms.", headerInfo.SweepLength, "3237 - 3238"),
                new SegyFileHeaderItem("Sweep type code.", headerInfo.SweepTypeCode, "3239 - 3240"),
                new SegyFileHeaderItem("Trace number of sweep channel.", headerInfo.TraceNumberOfSweepChannel,
                    "3241 - 3242"),
                new SegyFileHeaderItem("Sweep trace taper length, ms, at start if tapered.",
                    headerInfo.SweepTraceTaperLengthEnd, "3243 - 3244"),
                new SegyFileHeaderItem("Sweep trace taper length, ms, at end.", headerInfo.SweepTraceTaperLengthEnd,
                    "3245 - 3246"),
                new SegyFileHeaderItem("Taper type.", headerInfo.TaperType, "3247 - 3248"),
                new SegyFileHeaderItem("Correlated data traces.", headerInfo.CorrelatedDataTraces, "3249 - 3250"),
                new SegyFileHeaderItem("Binary gain recovered.", headerInfo.BinaryGainRecovered, "3251 - 3252"),
                new SegyFileHeaderItem("Amplitude recovery method.", headerInfo.AmplitudeRecoveryMethod, "3253 - 3254"),
                new SegyFileHeaderItem("Measurement system.", headerInfo.MeasurementSystem, "3255 - 3256"),
                new SegyFileHeaderItem("Impulse signal.", headerInfo.ImpulseSignal, "3257 - 3258"),
                new SegyFileHeaderItem("Vibratory polarity code.", headerInfo.VibratoryPolarityCode, "3259 - 3260")
            };

            dgvFileHeader.DataSource = headerItems;

            foreach (var segyHeaderItem in headerItems)
            {
                Debug.WriteLine("{0}\t{1}\t{2}", segyHeaderItem.Description, segyHeaderItem.Value,
                    segyHeaderItem.BytePosition);
            }
        }

        private void ExtractTraceHeaderInfo(ISegyFile segy, int index)
        {
            var traceHeaderInfo = segy.Traces[index].Header;
            var traceHeaderItems = new List<SegyTraceHeaderItem>
            {
                new SegyTraceHeaderItem("Trace Number Sequence within line.", traceHeaderInfo.Tracl, "01 - 04"),
                new SegyTraceHeaderItem("Trace Number Sequence within file (reel)", traceHeaderInfo.Tracr, "05 - 08"),
                new SegyTraceHeaderItem("Original Field Record Number", traceHeaderInfo.Fldr, "09 - 12"),
                new SegyTraceHeaderItem("Trace number within original field record", traceHeaderInfo.Tracf, "13 - 16"),
                new SegyTraceHeaderItem("Energy source point number", traceHeaderInfo.Ep, "17 - 20"),
                new SegyTraceHeaderItem("CDP ensemble number", traceHeaderInfo.Cdp, "21 - 24"),
                new SegyTraceHeaderItem("Trace number within the CDP ensemble", traceHeaderInfo.Cdpt, "25 - 28"),
                new SegyTraceHeaderItem("Trace identification code", traceHeaderInfo.Trid, "29 - 30"),
                new SegyTraceHeaderItem("Number of vertically summed traces", traceHeaderInfo.Nvs, "31 - 32"),
                new SegyTraceHeaderItem("Number of horizontally stacked traces", traceHeaderInfo.Nhs,
                    "33 - 34"),
                new SegyTraceHeaderItem("Data use.", traceHeaderInfo.Duse, "35 - 36"),
                new SegyTraceHeaderItem("Distance from source point to receiver group", traceHeaderInfo.Offset,
                    "37 - 40"),
                new SegyTraceHeaderItem("Receiver group elevation", traceHeaderInfo.Gelev, "41 - 44"),
                new SegyTraceHeaderItem("Surface elevation at source", traceHeaderInfo.Selev, "45 - 48"),
                new SegyTraceHeaderItem("Source depth below surface (positive number)", traceHeaderInfo.Sdepth,
                    "49 - 52"),
                new SegyTraceHeaderItem("Datum elevation at receiver group", traceHeaderInfo.Gdel, "53 - 56"),
                new SegyTraceHeaderItem("Datum elevation at source", traceHeaderInfo.Sdel, "57 - 60"),
                new SegyTraceHeaderItem("Water depth at source", traceHeaderInfo.Swdep, "61 - 64"),
                new SegyTraceHeaderItem("Water depth at group", traceHeaderInfo.Gwdep, "65 - 68"),
                new SegyTraceHeaderItem("Scalar to all elevations & depths.", traceHeaderInfo.Scalel, "69 - 70"),
                new SegyTraceHeaderItem("Scalar to all coordinate", traceHeaderInfo.Scalco, "71 - 72"),
                new SegyTraceHeaderItem("Source coordinate - X", traceHeaderInfo.Sx, "73 - 76"),
                new SegyTraceHeaderItem("Source coordinate - Y", traceHeaderInfo.Sy, "77 - 80"),
                new SegyTraceHeaderItem("Group coordinate - X", traceHeaderInfo.Gx, "81 - 84"),
                new SegyTraceHeaderItem("Group coordinate - Y", traceHeaderInfo.Gy, "85 - 88"),
                new SegyTraceHeaderItem("Coordinate units", traceHeaderInfo.Counit, "89 - 90")
            };

            dgvTraceHeader.DataSource = traceHeaderItems;
        }

        private void DisplaySeismicSection(ISegyFile segy, int tracecount, int timeInterval)
        {
            var maxtime = tracecount*(timeInterval/1000);

            // display seismic section
            SeismicViewer.ShowSeismic(SeismicFileHandler.GetAllTracesBitmap(segy), tracecount, maxtime);
            SeismicViewer.Image_Axis_Update();

            panSeismicMenu.Enabled = true;
            panSeismicMenu.Update();
            lblZoom.Text = String.Concat(SeismicViewer.ZoomFactor, " %");
            lblZoom.Update();
        }

        private void TreeView_SelectItem(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (
                !(e.Node.Name.EndsWith(SeismicFileManager.FileExt) &
                  (e.Node.Name != SeismicFileManager.FileUrlNowOpened))) return;
            if (readerWorker.IsBusy)
            {
                MessageBox.Show(@"There are unfinished background process in background");
                return;
            }
            // load selected segy file
            readerWorker.RunWorkerAsync(e.Node.Name);


            Debug.WriteLine(_segyFile.Traces.Count);
            Debug.WriteLine(_segyFile.Traces.ToList().First().Values.Count);
            Debug.Write("Job Number: ");
            Debug.WriteLine(_segyFile.Header.BinaryHeader.JobNumber);
            Debug.Write("Sample Interval Reel: ");
            Debug.WriteLine(_segyFile.Header.BinaryHeader.SampleIntervalReel);
            Debug.Write("Sample Interval Field: ");
            Debug.WriteLine(_segyFile.Header.BinaryHeader.SampleIntervalField);

            SeismicFileManager.FileUrlNowOpened = e.Node.Name;
        }

        #region seismic view event handler

        private void picBox1_MouseDown(object sender, MouseEventArgs e)
        {
            // if left mouse is clicked, activate pan mode
            if (MouseButtons == MouseButtons.Left)
            {
                SeismicViewer.FPan = true;
                SeismicViewer.PanStartMouse = panelImage.PointToClient(MousePosition);
                Cursor.Current = Cursors.Hand;

                if (panelImage.HorizontalScroll.Visible)
                    SeismicViewer.PanStartHScroll = panelImage.HorizontalScroll.Value;
                if (panelImage.VerticalScroll.Visible)
                    SeismicViewer.PanStartVScroll = panelImage.VerticalScroll.Value;
            }
        }

        private void picBox1_MouseLeave(object sender, EventArgs e)
        {
            SeismicViewer.GetScrollbarValue();
            SeismicViewer.Image_HideValueOnHover();
        }

        private void picBox1_MouseMove(object sender, MouseEventArgs e)
        {
            //if (this.Focused == false) return;
            //
            if (picBox1.Focused == false)
            {
                picBox1.Focus();
                SeismicViewer.SetScrollbarValue();
                SeismicViewer.Image_Axis_Update();
            }

            SeismicViewer.Image_ValueOnHover(MousePosition);

            if (SeismicViewer.FPan)
            {
                SeismicViewer.Image_Pan(MousePosition);
                SeismicViewer.Image_Axis_Update();
            }
        }

        private void picBox1_MouseUp(object sender, MouseEventArgs e)
        {
            SeismicViewer.FPan = false;
            Cursor.Current = Cursors.Default;
        }

        private void panelImage_Scroll(object sender, ScrollEventArgs e)
        {
            SeismicViewer.Image_Axis_Update();
        }

        #endregion

        // Reading of SEGY Files, done on a separate thread.
        private void readerWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var infile = e.Argument as string;
            e.Result = SeismicFileHandler.Read(infile);
        }

        //Display Header and Seismic Section after reading complete

        private void readerWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _segyFile = e.Result as ISegyFile;

            ExtractSegyFileHeaderInfo(_segyFile);
            BindCombobox(_segyFile);
            ExtractTraceHeaderInfo(_segyFile, 0);
            DisplaySeismicSection(_segyFile, _segyFile.Header.BinaryHeader.NDataPerTraceReel,
                _segyFile.Header.BinaryHeader.SampleIntervalReel);
            SEGYFile4Bmp = _segyFile;
        }

        // Automatically display trace header information when cbox index changed.

        private void cboxTraceSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            ExtractTraceHeaderInfo(_segyFile, cboxTraceSelection.SelectedIndex);
        }

        // Bind Combobox with traces in SEGY File
        private void BindCombobox(ISegyFile file)
        {
            var tracelist = new List<string>();

            for (var i = 0; i < file.Traces.Count; i++)
            {
                tracelist.Add(string.Format("Trace {0}", i + 1));
            }

            cboxTraceSelection.DataSource = tracelist;
        }

        private void btnWiener_Click_1(object sender, EventArgs e)
        {
            GamaseisProcessing.Deconvolution.WienerFilter(_segyFile, 0.05f, 0.3f);
            DisplaySeismicSection(_segyFile, _segyFile.Header.BinaryHeader.NDataPerTraceReel,
                _segyFile.Header.BinaryHeader.SampleIntervalReel);
            SEGYFile4Bmp = _segyFile;
            MessageBox.Show("Done.");
        }
    }
}