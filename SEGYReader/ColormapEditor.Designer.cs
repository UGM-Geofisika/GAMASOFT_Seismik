namespace SegyView
{
    partial class ColormapEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColormapEditor));
            this.panColormapCont = new System.Windows.Forms.Panel();
            this.picColorScale = new System.Windows.Forms.PictureBox();
            this.dgvColormap = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColorRGB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.comboPreset = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panPickerSV = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panPickerHue = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.numR = new System.Windows.Forms.NumericUpDown();
            this.numH = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numS = new System.Windows.Forms.NumericUpDown();
            this.numB = new System.Windows.Forms.NumericUpDown();
            this.numG = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numV = new System.Windows.Forms.NumericUpDown();
            this.picPickerHueLine = new System.Windows.Forms.PictureBox();
            this.panColormapCont.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picColorScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColormap)).BeginInit();
            this.panel3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPickerHueLine)).BeginInit();
            this.SuspendLayout();
            // 
            // panColormapCont
            // 
            this.panColormapCont.BackColor = System.Drawing.Color.DimGray;
            this.panColormapCont.Controls.Add(this.picColorScale);
            this.panColormapCont.Location = new System.Drawing.Point(12, 12);
            this.panColormapCont.Name = "panColormapCont";
            this.panColormapCont.Padding = new System.Windows.Forms.Padding(1);
            this.panColormapCont.Size = new System.Drawing.Size(514, 30);
            this.panColormapCont.TabIndex = 2;
            // 
            // picColorScale
            // 
            this.picColorScale.BackColor = System.Drawing.Color.White;
            this.picColorScale.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picColorScale.Location = new System.Drawing.Point(1, 1);
            this.picColorScale.Name = "picColorScale";
            this.picColorScale.Size = new System.Drawing.Size(512, 28);
            this.picColorScale.TabIndex = 1;
            this.picColorScale.TabStop = false;
            // 
            // dgvColormap
            // 
            this.dgvColormap.AllowUserToAddRows = false;
            this.dgvColormap.AllowUserToDeleteRows = false;
            this.dgvColormap.AllowUserToResizeColumns = false;
            this.dgvColormap.AllowUserToResizeRows = false;
            this.dgvColormap.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvColormap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvColormap.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.ColorRGB,
            this.Index});
            this.dgvColormap.Location = new System.Drawing.Point(12, 188);
            this.dgvColormap.MultiSelect = false;
            this.dgvColormap.Name = "dgvColormap";
            this.dgvColormap.RowHeadersWidth = 10;
            this.dgvColormap.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvColormap.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvColormap.Size = new System.Drawing.Size(229, 208);
            this.dgvColormap.TabIndex = 3;
            // 
            // ID
            // 
            this.ID.Frozen = true;
            this.ID.HeaderText = "#";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ID.Width = 40;
            // 
            // ColorRGB
            // 
            this.ColorRGB.HeaderText = "Color (RGB)";
            this.ColorRGB.Name = "ColorRGB";
            this.ColorRGB.ReadOnly = true;
            this.ColorRGB.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColorRGB.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Index
            // 
            this.Index.HeaderText = "Index";
            this.Index.Name = "Index";
            this.Index.ReadOnly = true;
            this.Index.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Index.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Index.Width = 50;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panColormapCont);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 25);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(539, 70);
            this.panel3.TabIndex = 6;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.comboPreset,
            this.toolStripSeparator1,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(539, 25);
            this.toolStrip1.TabIndex = 7;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(68, 22);
            this.toolStripLabel1.Text = "Load Preset";
            // 
            // comboPreset
            // 
            this.comboPreset.AutoCompleteCustomSource.AddRange(new string[] {
            "Blue-White-Red",
            "Grayscale",
            "Rainbow"});
            this.comboPreset.Items.AddRange(new object[] {
            "Black-White-Red",
            "Blue-White-Red",
            "Grayscale",
            "Rainbow"});
            this.comboPreset.Name = "comboPreset";
            this.comboPreset.Size = new System.Drawing.Size(121, 25);
            this.comboPreset.SelectedIndexChanged += new System.EventHandler(this.comboPreset_SelectedIndexChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(114, 22);
            this.toolStripButton1.Text = "Apply Colormap";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.DimGray;
            this.panel4.Controls.Add(this.panel1);
            this.panel4.Location = new System.Drawing.Point(257, 96);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(1);
            this.panel4.Size = new System.Drawing.Size(269, 300);
            this.panel4.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.picPickerHueLine);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(267, 298);
            this.panel1.TabIndex = 9;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.DimGray;
            this.panel5.Controls.Add(this.panPickerSV);
            this.panel5.Location = new System.Drawing.Point(5, 36);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(1);
            this.panel5.Size = new System.Drawing.Size(257, 257);
            this.panel5.TabIndex = 1;
            // 
            // panPickerSV
            // 
            this.panPickerSV.BackColor = System.Drawing.Color.White;
            this.panPickerSV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panPickerSV.Location = new System.Drawing.Point(1, 1);
            this.panPickerSV.Name = "panPickerSV";
            this.panPickerSV.Size = new System.Drawing.Size(255, 255);
            this.panPickerSV.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DimGray;
            this.panel2.Controls.Add(this.panPickerHue);
            this.panel2.Location = new System.Drawing.Point(5, 5);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(1);
            this.panel2.Size = new System.Drawing.Size(257, 25);
            this.panel2.TabIndex = 0;
            // 
            // panPickerHue
            // 
            this.panPickerHue.BackColor = System.Drawing.Color.White;
            this.panPickerHue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panPickerHue.Location = new System.Drawing.Point(1, 1);
            this.panPickerHue.Name = "panPickerHue";
            this.panPickerHue.Size = new System.Drawing.Size(255, 23);
            this.panPickerHue.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Red";
            // 
            // numR
            // 
            this.numR.Location = new System.Drawing.Point(51, 98);
            this.numR.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numR.Name = "numR";
            this.numR.Size = new System.Drawing.Size(47, 20);
            this.numR.TabIndex = 3;
            this.numR.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // numH
            // 
            this.numH.Location = new System.Drawing.Point(194, 98);
            this.numH.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numH.Name = "numH";
            this.numH.Size = new System.Drawing.Size(47, 20);
            this.numH.TabIndex = 5;
            this.numH.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(132, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Hue";
            // 
            // numS
            // 
            this.numS.Location = new System.Drawing.Point(194, 124);
            this.numS.Name = "numS";
            this.numS.Size = new System.Drawing.Size(47, 20);
            this.numS.TabIndex = 6;
            this.numS.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numB
            // 
            this.numB.Location = new System.Drawing.Point(51, 150);
            this.numB.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numB.Name = "numB";
            this.numB.Size = new System.Drawing.Size(47, 20);
            this.numB.TabIndex = 9;
            this.numB.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // numG
            // 
            this.numG.Location = new System.Drawing.Point(51, 124);
            this.numG.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numG.Name = "numG";
            this.numG.Size = new System.Drawing.Size(47, 20);
            this.numG.TabIndex = 8;
            this.numG.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Green";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 152);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Blue";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(132, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Saturation";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(132, 152);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Value";
            // 
            // numV
            // 
            this.numV.Location = new System.Drawing.Point(194, 150);
            this.numV.Name = "numV";
            this.numV.Size = new System.Drawing.Size(47, 20);
            this.numV.TabIndex = 14;
            this.numV.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // picPickerHueLine
            // 
            this.picPickerHueLine.BackColor = System.Drawing.Color.Black;
            this.picPickerHueLine.Location = new System.Drawing.Point(2, 0);
            this.picPickerHueLine.Name = "picPickerHueLine";
            this.picPickerHueLine.Size = new System.Drawing.Size(1, 40);
            this.picPickerHueLine.TabIndex = 2;
            this.picPickerHueLine.TabStop = false;
            // 
            // ColormapEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 407);
            this.Controls.Add(this.numV);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.dgvColormap);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.numB);
            this.Controls.Add(this.numG);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numR);
            this.Controls.Add(this.numS);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numH);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ColormapEditor";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Colormap Editor";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ColormapEditor_Load);
            this.panColormapCont.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picColorScale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColormap)).EndInit();
            this.panel3.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPickerHueLine)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panColormapCont;
        public System.Windows.Forms.DataGridView dgvColormap;
        public System.Windows.Forms.PictureBox picColorScale;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ToolStripComboBox comboPreset;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Panel panPickerHue;
        public System.Windows.Forms.Panel panPickerSV;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColorRGB;
        private System.Windows.Forms.DataGridViewTextBoxColumn Index;
        public System.Windows.Forms.NumericUpDown numB;
        public System.Windows.Forms.NumericUpDown numG;
        public System.Windows.Forms.NumericUpDown numR;
        public System.Windows.Forms.NumericUpDown numV;
        public System.Windows.Forms.NumericUpDown numS;
        public System.Windows.Forms.NumericUpDown numH;
        public System.Windows.Forms.PictureBox picPickerHueLine;
    }
}