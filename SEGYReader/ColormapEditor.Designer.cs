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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.comboPreset = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panColormapCont.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picColorScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColormap)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
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
            this.dgvColormap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvColormap.Location = new System.Drawing.Point(12, 29);
            this.dgvColormap.MultiSelect = false;
            this.dgvColormap.Name = "dgvColormap";
            this.dgvColormap.RowHeadersWidth = 10;
            this.dgvColormap.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvColormap.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvColormap.Size = new System.Drawing.Size(228, 174);
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
            this.Index.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Index.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Index.Width = 50;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.dgvColormap);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 95);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(12, 1, 1, 12);
            this.panel1.Size = new System.Drawing.Size(241, 215);
            this.panel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DimGray;
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(12, 1);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(1, 1, 1, 0);
            this.panel2.Size = new System.Drawing.Size(228, 28);
            this.panel2.TabIndex = 5;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.White;
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(1, 1);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(226, 28);
            this.panel5.TabIndex = 6;
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
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(241, 95);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(298, 215);
            this.panel4.TabIndex = 8;
            // 
            // ColormapEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 310);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.toolStrip1);
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
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panColormapCont;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.DataGridView dgvColormap;
        public System.Windows.Forms.PictureBox picColorScale;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ToolStripComboBox comboPreset;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColorRGB;
        private System.Windows.Forms.DataGridViewTextBoxColumn Index;
    }
}