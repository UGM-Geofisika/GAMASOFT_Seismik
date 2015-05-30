namespace SegyView
{
    partial class SEGYDisplay
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SEGYDisplay));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.sEGYReadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imglistFileTree = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panelSeismicParent = new System.Windows.Forms.Panel();
            this.panelImage = new SegyView.NoScrollPanel();
            this.picBox1 = new System.Windows.Forms.PictureBox();
            this.panelGap = new System.Windows.Forms.Panel();
            this.Panel3 = new System.Windows.Forms.Panel();
            this.panelY = new System.Windows.Forms.Panel();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.panelX = new System.Windows.Forms.Panel();
            this.panSeismicMenu = new System.Windows.Forms.Panel();
            this.panSeismicColor = new System.Windows.Forms.Panel();
            this.panColormapCont = new System.Windows.Forms.Panel();
            this.picColorScale = new System.Windows.Forms.PictureBox();
            this.panSeismicZoom = new System.Windows.Forms.Panel();
            this.butFitAllToScreen = new System.Windows.Forms.Button();
            this.butZoomFit = new System.Windows.Forms.Button();
            this.YAxisFitHeight = new System.Windows.Forms.Button();
            this.XAxisFitWidth = new System.Windows.Forms.Button();
            this.lblYAxisScale = new System.Windows.Forms.Label();
            this.lblXAxisScale = new System.Windows.Forms.Label();
            this.lblZoom = new System.Windows.Forms.Label();
            this.butSeismicZoom = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.txtHeaderText = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.colDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBytePosition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label5 = new System.Windows.Forms.Label();
            this.butColormapEdit = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panelSeismicParent.SuspendLayout();
            this.panelImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox1)).BeginInit();
            this.Panel3.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.panSeismicMenu.SuspendLayout();
            this.panSeismicColor.SuspendLayout();
            this.panColormapCont.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picColorScale)).BeginInit();
            this.panSeismicZoom.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sEGYReadToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(1037, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // sEGYReadToolStripMenuItem
            // 
            this.sEGYReadToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.sEGYReadToolStripMenuItem.Name = "sEGYReadToolStripMenuItem";
            this.sEGYReadToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.sEGYReadToolStripMenuItem.Text = "SEGYRead";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator5,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(113, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.Size = new System.Drawing.Size(668, 604);
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.statusStrip1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1037, 542);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 24);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(1037, 567);
            this.toolStripContainer1.TabIndex = 6;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Enabled = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(1037, 520);
            this.splitContainer1.SplitterDistance = 250;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 9;
            // 
            // treeView1
            // 
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.FullRowSelect = true;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imglistFileTree;
            this.treeView1.ItemHeight = 19;
            this.treeView1.Location = new System.Drawing.Point(0, 45);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 2;
            this.treeView1.ShowLines = false;
            this.treeView1.Size = new System.Drawing.Size(250, 474);
            this.treeView1.TabIndex = 1;
            // 
            // imglistFileTree
            // 
            this.imglistFileTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglistFileTree.ImageStream")));
            this.imglistFileTree.TransparentColor = System.Drawing.Color.Transparent;
            this.imglistFileTree.Images.SetKeyName(0, "folder_Closed_32xLG.png");
            this.imglistFileTree.Images.SetKeyName(1, "Miscellaneousfile_8304.png");
            this.imglistFileTree.Images.SetKeyName(2, "Symbols_Complete_and_ok_32xLG.png");
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 20);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(250, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(70, 22);
            this.openToolStripButton.Text = "&Add File";
            this.openToolStripButton.Click += new System.EventHandler(this.openToolStripButton_Click_1);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Moccasin;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(250, 20);
            this.panel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "File";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(786, 520);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panelSeismicParent);
            this.tabPage1.Controls.Add(this.panSeismicMenu);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(778, 494);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "Seismic";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panelSeismicParent
            // 
            this.panelSeismicParent.AutoSize = true;
            this.panelSeismicParent.Controls.Add(this.panelImage);
            this.panelSeismicParent.Controls.Add(this.panelGap);
            this.panelSeismicParent.Controls.Add(this.Panel3);
            this.panelSeismicParent.Controls.Add(this.Panel1);
            this.panelSeismicParent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSeismicParent.Location = new System.Drawing.Point(3, 100);
            this.panelSeismicParent.Name = "panelSeismicParent";
            this.panelSeismicParent.Size = new System.Drawing.Size(772, 391);
            this.panelSeismicParent.TabIndex = 0;
            // 
            // panelImage
            // 
            this.panelImage.AutoScroll = true;
            this.panelImage.Controls.Add(this.picBox1);
            this.panelImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelImage.Location = new System.Drawing.Point(54, 45);
            this.panelImage.Name = "panelImage";
            this.panelImage.Size = new System.Drawing.Size(718, 346);
            this.panelImage.TabIndex = 7;
            this.panelImage.Scroll += new System.Windows.Forms.ScrollEventHandler(this.panelImage_Scroll);
            // 
            // picBox1
            // 
            this.picBox1.Location = new System.Drawing.Point(0, 0);
            this.picBox1.Name = "picBox1";
            this.picBox1.Size = new System.Drawing.Size(161, 120);
            this.picBox1.TabIndex = 0;
            this.picBox1.TabStop = false;
            this.picBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBox1_MouseDown);
            this.picBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picBox1_MouseMove);
            this.picBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBox1_MouseUp);
            // 
            // panelGap
            // 
            this.panelGap.BackColor = System.Drawing.Color.White;
            this.panelGap.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelGap.Location = new System.Drawing.Point(54, 35);
            this.panelGap.Name = "panelGap";
            this.panelGap.Size = new System.Drawing.Size(718, 10);
            this.panelGap.TabIndex = 6;
            // 
            // Panel3
            // 
            this.Panel3.BackColor = System.Drawing.Color.Blue;
            this.Panel3.Controls.Add(this.panelY);
            this.Panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.Panel3.Location = new System.Drawing.Point(0, 35);
            this.Panel3.Name = "Panel3";
            this.Panel3.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.Panel3.Size = new System.Drawing.Size(54, 356);
            this.Panel3.TabIndex = 4;
            // 
            // panelY
            // 
            this.panelY.AutoSize = true;
            this.panelY.BackColor = System.Drawing.SystemColors.Control;
            this.panelY.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.panelY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelY.Location = new System.Drawing.Point(0, 0);
            this.panelY.Name = "panelY";
            this.panelY.Size = new System.Drawing.Size(53, 356);
            this.panelY.TabIndex = 1;
            this.panelY.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelY_MouseDown);
            this.panelY.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelY_MouseMove);
            this.panelY.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelY_MouseUp);
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.Blue;
            this.Panel1.Controls.Add(this.panelX);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel1.Location = new System.Drawing.Point(0, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.Panel1.Size = new System.Drawing.Size(772, 35);
            this.Panel1.TabIndex = 1;
            // 
            // panelX
            // 
            this.panelX.BackColor = System.Drawing.SystemColors.Control;
            this.panelX.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.panelX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelX.Location = new System.Drawing.Point(0, 0);
            this.panelX.Name = "panelX";
            this.panelX.Size = new System.Drawing.Size(772, 34);
            this.panelX.TabIndex = 1;
            this.panelX.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelX_MouseDown);
            this.panelX.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelX_MouseMove);
            this.panelX.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelX_MouseUp);
            // 
            // panSeismicMenu
            // 
            this.panSeismicMenu.Controls.Add(this.panSeismicColor);
            this.panSeismicMenu.Controls.Add(this.panSeismicZoom);
            this.panSeismicMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panSeismicMenu.Location = new System.Drawing.Point(3, 3);
            this.panSeismicMenu.Name = "panSeismicMenu";
            this.panSeismicMenu.Size = new System.Drawing.Size(772, 97);
            this.panSeismicMenu.TabIndex = 4;
            // 
            // panSeismicColor
            // 
            this.panSeismicColor.Controls.Add(this.butColormapEdit);
            this.panSeismicColor.Controls.Add(this.label5);
            this.panSeismicColor.Controls.Add(this.panColormapCont);
            this.panSeismicColor.Dock = System.Windows.Forms.DockStyle.Left;
            this.panSeismicColor.Location = new System.Drawing.Point(193, 0);
            this.panSeismicColor.Name = "panSeismicColor";
            this.panSeismicColor.Size = new System.Drawing.Size(273, 97);
            this.panSeismicColor.TabIndex = 2;
            // 
            // panColormapCont
            // 
            this.panColormapCont.BackColor = System.Drawing.Color.DimGray;
            this.panColormapCont.Controls.Add(this.picColorScale);
            this.panColormapCont.Location = new System.Drawing.Point(9, 27);
            this.panColormapCont.Name = "panColormapCont";
            this.panColormapCont.Padding = new System.Windows.Forms.Padding(1);
            this.panColormapCont.Size = new System.Drawing.Size(258, 20);
            this.panColormapCont.TabIndex = 1;
            // 
            // picColorScale
            // 
            this.picColorScale.BackColor = System.Drawing.Color.White;
            this.picColorScale.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picColorScale.Location = new System.Drawing.Point(1, 1);
            this.picColorScale.Name = "picColorScale";
            this.picColorScale.Size = new System.Drawing.Size(256, 18);
            this.picColorScale.TabIndex = 0;
            this.picColorScale.TabStop = false;
            // 
            // panSeismicZoom
            // 
            this.panSeismicZoom.Controls.Add(this.butFitAllToScreen);
            this.panSeismicZoom.Controls.Add(this.butZoomFit);
            this.panSeismicZoom.Controls.Add(this.YAxisFitHeight);
            this.panSeismicZoom.Controls.Add(this.XAxisFitWidth);
            this.panSeismicZoom.Controls.Add(this.lblYAxisScale);
            this.panSeismicZoom.Controls.Add(this.lblXAxisScale);
            this.panSeismicZoom.Controls.Add(this.lblZoom);
            this.panSeismicZoom.Controls.Add(this.butSeismicZoom);
            this.panSeismicZoom.Controls.Add(this.label4);
            this.panSeismicZoom.Controls.Add(this.button3);
            this.panSeismicZoom.Controls.Add(this.button2);
            this.panSeismicZoom.Controls.Add(this.label3);
            this.panSeismicZoom.Controls.Add(this.label2);
            this.panSeismicZoom.Dock = System.Windows.Forms.DockStyle.Left;
            this.panSeismicZoom.Location = new System.Drawing.Point(0, 0);
            this.panSeismicZoom.Name = "panSeismicZoom";
            this.panSeismicZoom.Size = new System.Drawing.Size(193, 97);
            this.panSeismicZoom.TabIndex = 1;
            // 
            // butFitAllToScreen
            // 
            this.butFitAllToScreen.Location = new System.Drawing.Point(76, 72);
            this.butFitAllToScreen.Name = "butFitAllToScreen";
            this.butFitAllToScreen.Size = new System.Drawing.Size(110, 20);
            this.butFitAllToScreen.TabIndex = 26;
            this.butFitAllToScreen.Text = "Fit View To Screen";
            this.butFitAllToScreen.UseVisualStyleBackColor = true;
            this.butFitAllToScreen.Click += new System.EventHandler(this.butFitAllToScreen_Click);
            // 
            // butZoomFit
            // 
            this.butZoomFit.Location = new System.Drawing.Point(107, 3);
            this.butZoomFit.Name = "butZoomFit";
            this.butZoomFit.Size = new System.Drawing.Size(79, 20);
            this.butZoomFit.TabIndex = 25;
            this.butZoomFit.Text = "Fit To Screen";
            this.butZoomFit.UseVisualStyleBackColor = true;
            this.butZoomFit.Click += new System.EventHandler(this.butZoomFit_Click);
            // 
            // YAxisFitHeight
            // 
            this.YAxisFitHeight.Location = new System.Drawing.Point(107, 49);
            this.YAxisFitHeight.Name = "YAxisFitHeight";
            this.YAxisFitHeight.Size = new System.Drawing.Size(79, 20);
            this.YAxisFitHeight.TabIndex = 24;
            this.YAxisFitHeight.Text = "Fit To Height";
            this.YAxisFitHeight.UseVisualStyleBackColor = true;
            this.YAxisFitHeight.Click += new System.EventHandler(this.YAxisFitHeight_Click);
            // 
            // XAxisFitWidth
            // 
            this.XAxisFitWidth.Location = new System.Drawing.Point(107, 26);
            this.XAxisFitWidth.Name = "XAxisFitWidth";
            this.XAxisFitWidth.Size = new System.Drawing.Size(79, 20);
            this.XAxisFitWidth.TabIndex = 23;
            this.XAxisFitWidth.Text = "Fit To Width";
            this.XAxisFitWidth.UseVisualStyleBackColor = true;
            this.XAxisFitWidth.Click += new System.EventHandler(this.XAxisFitWidth_Click);
            // 
            // lblYAxisScale
            // 
            this.lblYAxisScale.AutoSize = true;
            this.lblYAxisScale.Location = new System.Drawing.Point(37, 52);
            this.lblYAxisScale.Name = "lblYAxisScale";
            this.lblYAxisScale.Size = new System.Drawing.Size(36, 13);
            this.lblYAxisScale.TabIndex = 22;
            this.lblYAxisScale.Text = "100 %";
            this.lblYAxisScale.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblXAxisScale
            // 
            this.lblXAxisScale.AutoSize = true;
            this.lblXAxisScale.Location = new System.Drawing.Point(37, 30);
            this.lblXAxisScale.Name = "lblXAxisScale";
            this.lblXAxisScale.Size = new System.Drawing.Size(36, 13);
            this.lblXAxisScale.TabIndex = 21;
            this.lblXAxisScale.Text = "100 %";
            this.lblXAxisScale.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblZoom
            // 
            this.lblZoom.AutoSize = true;
            this.lblZoom.Location = new System.Drawing.Point(37, 7);
            this.lblZoom.Name = "lblZoom";
            this.lblZoom.Size = new System.Drawing.Size(36, 13);
            this.lblZoom.TabIndex = 20;
            this.lblZoom.Text = "100 %";
            this.lblZoom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // butSeismicZoom
            // 
            this.butSeismicZoom.Location = new System.Drawing.Point(76, 3);
            this.butSeismicZoom.Name = "butSeismicZoom";
            this.butSeismicZoom.Size = new System.Drawing.Size(31, 20);
            this.butSeismicZoom.TabIndex = 1;
            this.butSeismicZoom.Text = "1:1";
            this.butSeismicZoom.UseVisualStyleBackColor = true;
            this.butSeismicZoom.Click += new System.EventHandler(this.butSeismicZoom_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Y-Axis";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(76, 49);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(31, 20);
            this.button3.TabIndex = 16;
            this.button3.Text = "1:1";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(76, 26);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(31, 20);
            this.button2.TabIndex = 15;
            this.button2.Text = "1:1";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "X-Axis";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Zoom";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(778, 494);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Header";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.txtHeaderText);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer2.Size = new System.Drawing.Size(772, 488);
            this.splitContainer2.SplitterDistance = 386;
            this.splitContainer2.TabIndex = 1;
            // 
            // txtHeaderText
            // 
            this.txtHeaderText.BackColor = System.Drawing.Color.White;
            this.txtHeaderText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtHeaderText.Location = new System.Drawing.Point(0, 0);
            this.txtHeaderText.Multiline = true;
            this.txtHeaderText.Name = "txtHeaderText";
            this.txtHeaderText.ReadOnly = true;
            this.txtHeaderText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtHeaderText.Size = new System.Drawing.Size(386, 488);
            this.txtHeaderText.TabIndex = 0;
            this.txtHeaderText.WordWrap = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDesc,
            this.colValue,
            this.colBytePosition});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 45;
            this.dataGridView1.Size = new System.Drawing.Size(382, 488);
            this.dataGridView1.TabIndex = 1;
            // 
            // colDesc
            // 
            this.colDesc.DataPropertyName = "Description";
            this.colDesc.HeaderText = "Description";
            this.colDesc.Name = "colDesc";
            this.colDesc.ReadOnly = true;
            // 
            // colValue
            // 
            this.colValue.DataPropertyName = "Value";
            this.colValue.HeaderText = "Value";
            this.colValue.Name = "colValue";
            this.colValue.ReadOnly = true;
            // 
            // colBytePosition
            // 
            this.colBytePosition.DataPropertyName = "BytePosition";
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colBytePosition.DefaultCellStyle = dataGridViewCellStyle1;
            this.colBytePosition.HeaderText = "Byte Position";
            this.colBytePosition.Name = "colBytePosition";
            this.colBytePosition.ReadOnly = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(778, 494);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Trace";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 520);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1037, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "\"SEG-Y Files\"|*.sgy";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 27;
            this.label5.Text = "Colormap";
            // 
            // butColormapEdit
            // 
            this.butColormapEdit.Location = new System.Drawing.Point(185, 3);
            this.butColormapEdit.Name = "butColormapEdit";
            this.butColormapEdit.Size = new System.Drawing.Size(82, 20);
            this.butColormapEdit.TabIndex = 28;
            this.butColormapEdit.Text = "Edit Colormap";
            this.butColormapEdit.UseVisualStyleBackColor = true;
            // 
            // SEGYDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(244)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(1037, 591);
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "SEGYDisplay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SEG-Y SegyView";
            this.Load += new System.EventHandler(this.SEGYDisplay_Load);
            this.Resize += new System.EventHandler(this.SEGYDisplay_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ContentPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.panelSeismicParent.ResumeLayout(false);
            this.panelImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBox1)).EndInit();
            this.Panel3.ResumeLayout(false);
            this.Panel3.PerformLayout();
            this.Panel1.ResumeLayout(false);
            this.panSeismicMenu.ResumeLayout(false);
            this.panSeismicColor.ResumeLayout(false);
            this.panSeismicColor.PerformLayout();
            this.panColormapCont.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picColorScale)).EndInit();
            this.panSeismicZoom.ResumeLayout(false);
            this.panSeismicZoom.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem sEGYReadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panelSeismicParent;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.PictureBox picBox1;
        internal System.Windows.Forms.Panel panelX;
        internal System.Windows.Forms.Panel Panel3;
        internal System.Windows.Forms.Panel panelY;
        internal System.Windows.Forms.Panel panelGap;
        private NoScrollPanel panelImage;
        private System.Windows.Forms.Panel panSeismicMenu;
        private System.Windows.Forms.Panel panSeismicZoom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button butSeismicZoom;
        private System.Windows.Forms.Label lblZoom;
        private System.Windows.Forms.Label lblYAxisScale;
        private System.Windows.Forms.Label lblXAxisScale;
        internal System.Windows.Forms.Panel Panel1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TextBox txtHeaderText;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBytePosition;
        private System.Windows.Forms.Button XAxisFitWidth;
        private System.Windows.Forms.Button YAxisFitHeight;
        private System.Windows.Forms.Button butZoomFit;
        private System.Windows.Forms.Button butFitAllToScreen;
        public System.Windows.Forms.ImageList imglistFileTree;
        private System.Windows.Forms.Panel panSeismicColor;
        private System.Windows.Forms.PictureBox picColorScale;
        private System.Windows.Forms.Panel panColormapCont;
        private System.Windows.Forms.Button butColormapEdit;
        private System.Windows.Forms.Label label5;

    }
}

