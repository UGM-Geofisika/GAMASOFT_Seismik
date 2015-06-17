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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panColormapCont = new System.Windows.Forms.Panel();
            this.picColorScale = new System.Windows.Forms.PictureBox();
            this.dgvColormap = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.comboPreset = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panPickerHue = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panPickerSV = new System.Windows.Forms.PictureBox();
            this.picPickerHueLine = new System.Windows.Forms.PictureBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel8 = new System.Windows.Forms.Panel();
            this.picNewClr = new System.Windows.Forms.PictureBox();
            this.panCurClrCont = new System.Windows.Forms.Panel();
            this.picCurClr = new System.Windows.Forms.PictureBox();
            this.dgvPickColor = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panColormapCont.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picColorScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColormap)).BeginInit();
            this.panel3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panPickerSV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPickerHueLine)).BeginInit();
            this.panel6.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picNewClr)).BeginInit();
            this.panCurClrCont.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCurClr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPickColor)).BeginInit();
            this.panel7.SuspendLayout();
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
            this.Index,
            this.cR,
            this.cG,
            this.cB,
            this.cH,
            this.cS,
            this.cV});
            this.dgvColormap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvColormap.Location = new System.Drawing.Point(12, 0);
            this.dgvColormap.MultiSelect = false;
            this.dgvColormap.Name = "dgvColormap";
            this.dgvColormap.RowHeadersWidth = 10;
            this.dgvColormap.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvColormap.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvColormap.Size = new System.Drawing.Size(0, 272);
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
            // Index
            // 
            this.Index.HeaderText = "Index";
            this.Index.Name = "Index";
            this.Index.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Index.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Index.Width = 50;
            // 
            // cR
            // 
            this.cR.HeaderText = "Red";
            this.cR.Name = "cR";
            this.cR.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.cR.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.cR.Width = 50;
            // 
            // cG
            // 
            this.cG.HeaderText = "Green";
            this.cG.Name = "cG";
            this.cG.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.cG.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.cG.Width = 50;
            // 
            // cB
            // 
            this.cB.HeaderText = "Blue";
            this.cB.Name = "cB";
            this.cB.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.cB.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.cB.Width = 50;
            // 
            // cH
            // 
            this.cH.HeaderText = "Hue";
            this.cH.Name = "cH";
            this.cH.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.cH.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.cH.Width = 50;
            // 
            // cS
            // 
            this.cS.HeaderText = "Sat";
            this.cS.Name = "cS";
            this.cS.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.cS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.cS.Width = 50;
            // 
            // cV
            // 
            this.cV.HeaderText = "Value";
            this.cV.Name = "cV";
            this.cV.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.cV.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.cV.Width = 50;
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
            "Rainbow",
            "Custom..."});
            this.comboPreset.Name = "comboPreset";
            this.comboPreset.Size = new System.Drawing.Size(121, 25);
            this.comboPreset.Text = "Custom...";
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
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.DimGray;
            this.panel4.Controls.Add(this.panel1);
            this.panel4.Location = new System.Drawing.Point(196, 0);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(1);
            this.panel4.Size = new System.Drawing.Size(318, 269);
            this.panel4.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.picPickerHueLine);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(316, 267);
            this.panel1.TabIndex = 9;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DimGray;
            this.panel2.Controls.Add(this.panPickerHue);
            this.panel2.Location = new System.Drawing.Point(268, 5);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(1);
            this.panel2.Size = new System.Drawing.Size(25, 258);
            this.panel2.TabIndex = 0;
            // 
            // panPickerHue
            // 
            this.panPickerHue.BackColor = System.Drawing.Color.White;
            this.panPickerHue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panPickerHue.Location = new System.Drawing.Point(1, 1);
            this.panPickerHue.Name = "panPickerHue";
            this.panPickerHue.Size = new System.Drawing.Size(23, 256);
            this.panPickerHue.TabIndex = 1;
            this.panPickerHue.Click += new System.EventHandler(this.panPickerHue_Click);
            this.panPickerHue.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panPickerHue_MouseMove);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.DimGray;
            this.panel5.Controls.Add(this.panPickerSV);
            this.panel5.Location = new System.Drawing.Point(5, 5);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(1);
            this.panel5.Size = new System.Drawing.Size(258, 258);
            this.panel5.TabIndex = 1;
            // 
            // panPickerSV
            // 
            this.panPickerSV.BackColor = System.Drawing.Color.White;
            this.panPickerSV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panPickerSV.Location = new System.Drawing.Point(1, 1);
            this.panPickerSV.Name = "panPickerSV";
            this.panPickerSV.Size = new System.Drawing.Size(256, 256);
            this.panPickerSV.TabIndex = 1;
            this.panPickerSV.TabStop = false;
            this.panPickerSV.Click += new System.EventHandler(this.panPickerSV_Click);
            this.panPickerSV.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panPickerSV_MouseMove);
            // 
            // picPickerHueLine
            // 
            this.picPickerHueLine.BackColor = System.Drawing.Color.Transparent;
            this.picPickerHueLine.Image = ((System.Drawing.Image)(resources.GetObject("picPickerHueLine.Image")));
            this.picPickerHueLine.Location = new System.Drawing.Point(293, 0);
            this.picPickerHueLine.Name = "picPickerHueLine";
            this.picPickerHueLine.Size = new System.Drawing.Size(18, 18);
            this.picPickerHueLine.TabIndex = 2;
            this.picPickerHueLine.TabStop = false;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.button2);
            this.panel6.Controls.Add(this.panel4);
            this.panel6.Controls.Add(this.button1);
            this.panel6.Controls.Add(this.panel8);
            this.panel6.Controls.Add(this.panCurClrCont);
            this.panel6.Controls.Add(this.dgvPickColor);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel6.Location = new System.Drawing.Point(12, 95);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.panel6.Size = new System.Drawing.Size(527, 280);
            this.panel6.TabIndex = 16;
            this.panel6.Visible = false;
            this.panel6.VisibleChanged += new System.EventHandler(this.panel6_VisibleChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(108, 246);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 20;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(108, 217);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 19;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.DimGray;
            this.panel8.Controls.Add(this.picNewClr);
            this.panel8.Location = new System.Drawing.Point(133, 0);
            this.panel8.Name = "panel8";
            this.panel8.Padding = new System.Windows.Forms.Padding(1);
            this.panel8.Size = new System.Drawing.Size(50, 40);
            this.panel8.TabIndex = 18;
            // 
            // picNewClr
            // 
            this.picNewClr.BackColor = System.Drawing.Color.White;
            this.picNewClr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picNewClr.Location = new System.Drawing.Point(1, 1);
            this.picNewClr.Name = "picNewClr";
            this.picNewClr.Size = new System.Drawing.Size(48, 38);
            this.picNewClr.TabIndex = 1;
            this.picNewClr.TabStop = false;
            // 
            // panCurClrCont
            // 
            this.panCurClrCont.BackColor = System.Drawing.Color.DimGray;
            this.panCurClrCont.Controls.Add(this.picCurClr);
            this.panCurClrCont.Location = new System.Drawing.Point(82, 0);
            this.panCurClrCont.Name = "panCurClrCont";
            this.panCurClrCont.Padding = new System.Windows.Forms.Padding(1);
            this.panCurClrCont.Size = new System.Drawing.Size(50, 40);
            this.panCurClrCont.TabIndex = 11;
            // 
            // picCurClr
            // 
            this.picCurClr.BackColor = System.Drawing.Color.White;
            this.picCurClr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picCurClr.Location = new System.Drawing.Point(1, 1);
            this.picCurClr.Name = "picCurClr";
            this.picCurClr.Size = new System.Drawing.Size(48, 38);
            this.picCurClr.TabIndex = 1;
            this.picCurClr.TabStop = false;
            // 
            // dgvPickColor
            // 
            this.dgvPickColor.AllowUserToAddRows = false;
            this.dgvPickColor.AllowUserToDeleteRows = false;
            this.dgvPickColor.AllowUserToResizeColumns = false;
            this.dgvPickColor.AllowUserToResizeRows = false;
            this.dgvPickColor.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvPickColor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPickColor.ColumnHeadersHeight = 32;
            this.dgvPickColor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvPickColor.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.dgvPickColor.EnableHeadersVisualStyles = false;
            this.dgvPickColor.Location = new System.Drawing.Point(22, 46);
            this.dgvPickColor.MultiSelect = false;
            this.dgvPickColor.Name = "dgvPickColor";
            this.dgvPickColor.RowHeadersVisible = false;
            this.dgvPickColor.RowHeadersWidth = 10;
            this.dgvPickColor.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvPickColor.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvPickColor.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPickColor.Size = new System.Drawing.Size(164, 165);
            this.dgvPickColor.TabIndex = 10;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewCellStyle28.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle28;
            this.dataGridViewTextBoxColumn1.Frozen = true;
            this.dataGridViewTextBoxColumn1.HeaderText = " ";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 60;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewCellStyle29.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle29;
            this.dataGridViewTextBoxColumn2.HeaderText = "Current Color";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Width = 50;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewCellStyle30.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle30;
            this.dataGridViewTextBoxColumn3.HeaderText = "New Color";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn3.Width = 50;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.dgvColormap);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(0, 95);
            this.panel7.Name = "panel7";
            this.panel7.Padding = new System.Windows.Forms.Padding(12, 0, 12, 8);
            this.panel7.Size = new System.Drawing.Size(12, 280);
            this.panel7.TabIndex = 17;
            // 
            // ColormapEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 375);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ColormapEditor";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Colormap Editor";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ColormapEditor_FormClosing);
            this.Load += new System.EventHandler(this.ColormapEditor_Load);
            this.panColormapCont.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picColorScale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColormap)).EndInit();
            this.panel3.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panPickerSV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPickerHueLine)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picNewClr)).EndInit();
            this.panCurClrCont.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picCurClr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPickColor)).EndInit();
            this.panel7.ResumeLayout(false);
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
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Panel panPickerHue;
        public System.Windows.Forms.PictureBox picPickerHueLine;
        public System.Windows.Forms.ToolStripComboBox comboPreset;
        public System.Windows.Forms.PictureBox panPickerSV;
        private System.Windows.Forms.Panel panel7;
        public System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Index;
        private System.Windows.Forms.DataGridViewTextBoxColumn cR;
        private System.Windows.Forms.DataGridViewTextBoxColumn cG;
        private System.Windows.Forms.DataGridViewTextBoxColumn cB;
        private System.Windows.Forms.DataGridViewTextBoxColumn cH;
        private System.Windows.Forms.DataGridViewTextBoxColumn cS;
        private System.Windows.Forms.DataGridViewTextBoxColumn cV;
        public System.Windows.Forms.DataGridView dgvPickColor;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.Panel panCurClrCont;
        public System.Windows.Forms.PictureBox picCurClr;
        private System.Windows.Forms.Panel panel8;
        public System.Windows.Forms.PictureBox picNewClr;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
    }
}