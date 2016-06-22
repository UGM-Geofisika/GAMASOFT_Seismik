namespace Gamaseis
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preProcessingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mdiClientController1 = new Slusser.Components.MdiClientController();
            this.addFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.listBoxFiles = new System.Windows.Forms.ListBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbPlotWiggle = new System.Windows.Forms.ToolStripButton();
            this.tsbPlotDensityGray = new System.Windows.Forms.ToolStripButton();
            this.plotDensityColor = new System.Windows.Forms.ToolStripButton();
            this.tsbPlotFx = new System.Windows.Forms.ToolStripButton();
            this.tsbPlotFk = new System.Windows.Forms.ToolStripButton();
            this.tsbSourceElev = new System.Windows.Forms.ToolStripButton();
            this.tsbReceiverElev = new System.Windows.Forms.ToolStripButton();
            this.tsbTracePerGather = new System.Windows.Forms.ToolStripButton();
            this.amplitudeCorrectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.preProcessingToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1094, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addFilesToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // addFilesToolStripMenuItem
            // 
            this.addFilesToolStripMenuItem.Name = "addFilesToolStripMenuItem";
            this.addFilesToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.addFilesToolStripMenuItem.Text = "Add Files";
            this.addFilesToolStripMenuItem.Click += new System.EventHandler(this.addFilesToolStripMenuItem_Click);
            // 
            // preProcessingToolStripMenuItem
            // 
            this.preProcessingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.amplitudeCorrectionToolStripMenuItem});
            this.preProcessingToolStripMenuItem.Name = "preProcessingToolStripMenuItem";
            this.preProcessingToolStripMenuItem.Size = new System.Drawing.Size(96, 20);
            this.preProcessingToolStripMenuItem.Text = "Pre Processing";
            // 
            // mdiClientController1
            // 
            this.mdiClientController1.BackColor = System.Drawing.Color.White;
            this.mdiClientController1.ParentForm = this;
            // 
            // addFileDialog
            // 
            this.addFileDialog.Filter = "Segy File (*.sgy) | *.sgy";
            // 
            // listBoxFiles
            // 
            this.listBoxFiles.Dock = System.Windows.Forms.DockStyle.Left;
            this.listBoxFiles.FormattingEnabled = true;
            this.listBoxFiles.Location = new System.Drawing.Point(0, 49);
            this.listBoxFiles.Name = "listBoxFiles";
            this.listBoxFiles.Size = new System.Drawing.Size(224, 562);
            this.listBoxFiles.TabIndex = 4;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbPlotWiggle,
            this.tsbPlotDensityGray,
            this.plotDensityColor,
            this.tsbPlotFx,
            this.tsbPlotFk,
            this.tsbSourceElev,
            this.tsbReceiverElev,
            this.tsbTracePerGather});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1094, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbPlotWiggle
            // 
            this.tsbPlotWiggle.Image = ((System.Drawing.Image)(resources.GetObject("tsbPlotWiggle.Image")));
            this.tsbPlotWiggle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPlotWiggle.Name = "tsbPlotWiggle";
            this.tsbPlotWiggle.Size = new System.Drawing.Size(88, 22);
            this.tsbPlotWiggle.Text = "Plot Wiggle";
            this.tsbPlotWiggle.Click += new System.EventHandler(this.tsbPlotWiggle_Click);
            // 
            // tsbPlotDensityGray
            // 
            this.tsbPlotDensityGray.Image = ((System.Drawing.Image)(resources.GetObject("tsbPlotDensityGray.Image")));
            this.tsbPlotDensityGray.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPlotDensityGray.Name = "tsbPlotDensityGray";
            this.tsbPlotDensityGray.Size = new System.Drawing.Size(90, 22);
            this.tsbPlotDensityGray.Text = "Plot Density";
            this.tsbPlotDensityGray.Click += new System.EventHandler(this.tsbPlotDensityGray_Click);
            // 
            // plotDensityColor
            // 
            this.plotDensityColor.Image = ((System.Drawing.Image)(resources.GetObject("plotDensityColor.Image")));
            this.plotDensityColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.plotDensityColor.Name = "plotDensityColor";
            this.plotDensityColor.Size = new System.Drawing.Size(122, 22);
            this.plotDensityColor.Text = "Plot Density Color";
            this.plotDensityColor.Click += new System.EventHandler(this.plotDensityColor_Click);
            // 
            // tsbPlotFx
            // 
            this.tsbPlotFx.Image = ((System.Drawing.Image)(resources.GetObject("tsbPlotFx.Image")));
            this.tsbPlotFx.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPlotFx.Name = "tsbPlotFx";
            this.tsbPlotFx.Size = new System.Drawing.Size(67, 22);
            this.tsbPlotFx.Text = "Plot F-x";
            this.tsbPlotFx.Click += new System.EventHandler(this.tsbPlotFx_Click);
            // 
            // tsbPlotFk
            // 
            this.tsbPlotFk.Image = ((System.Drawing.Image)(resources.GetObject("tsbPlotFk.Image")));
            this.tsbPlotFk.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPlotFk.Name = "tsbPlotFk";
            this.tsbPlotFk.Size = new System.Drawing.Size(68, 22);
            this.tsbPlotFk.Text = "Plot F-k";
            this.tsbPlotFk.Click += new System.EventHandler(this.tsbPlotFk_Click);
            // 
            // tsbSourceElev
            // 
            this.tsbSourceElev.Image = ((System.Drawing.Image)(resources.GetObject("tsbSourceElev.Image")));
            this.tsbSourceElev.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSourceElev.Name = "tsbSourceElev";
            this.tsbSourceElev.Size = new System.Drawing.Size(87, 22);
            this.tsbSourceElev.Text = "Source Elev";
            this.tsbSourceElev.Click += new System.EventHandler(this.tsbSourceElev_Click);
            // 
            // tsbReceiverElev
            // 
            this.tsbReceiverElev.Image = ((System.Drawing.Image)(resources.GetObject("tsbReceiverElev.Image")));
            this.tsbReceiverElev.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbReceiverElev.Name = "tsbReceiverElev";
            this.tsbReceiverElev.Size = new System.Drawing.Size(95, 22);
            this.tsbReceiverElev.Text = "Receiver Elev";
            this.tsbReceiverElev.Click += new System.EventHandler(this.tsbReceiverElev_Click);
            // 
            // tsbTracePerGather
            // 
            this.tsbTracePerGather.Image = ((System.Drawing.Image)(resources.GetObject("tsbTracePerGather.Image")));
            this.tsbTracePerGather.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTracePerGather.Name = "tsbTracePerGather";
            this.tsbTracePerGather.Size = new System.Drawing.Size(117, 22);
            this.tsbTracePerGather.Text = "Traces per gather";
            this.tsbTracePerGather.Click += new System.EventHandler(this.tsbTracePerGather_Click);
            // 
            // amplitudeCorrectionToolStripMenuItem
            // 
            this.amplitudeCorrectionToolStripMenuItem.Name = "amplitudeCorrectionToolStripMenuItem";
            this.amplitudeCorrectionToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.amplitudeCorrectionToolStripMenuItem.Text = "Amplitude Correction";
            this.amplitudeCorrectionToolStripMenuItem.Click += new System.EventHandler(this.amplitudeCorrectionToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1094, 611);
            this.Controls.Add(this.listBoxFiles);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Gamasoft Seismik";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private Slusser.Components.MdiClientController mdiClientController1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preProcessingToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog addFileDialog;
        private System.Windows.Forms.ListBox listBoxFiles;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbPlotWiggle;
        private System.Windows.Forms.ToolStripButton tsbPlotDensityGray;
        private System.Windows.Forms.ToolStripButton plotDensityColor;
        private System.Windows.Forms.ToolStripButton tsbPlotFx;
        private System.Windows.Forms.ToolStripButton tsbPlotFk;
        private System.Windows.Forms.ToolStripButton tsbSourceElev;
        private System.Windows.Forms.ToolStripButton tsbReceiverElev;
        private System.Windows.Forms.ToolStripButton tsbTracePerGather;
        private System.Windows.Forms.ToolStripMenuItem amplitudeCorrectionToolStripMenuItem;
    }
}

