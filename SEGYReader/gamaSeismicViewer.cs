using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Diagnostics;

namespace SegyView
{
    public static class GamaSeismicViewer
    {
        public static int[] ImgOriginalSize = new int[2];
        public static double ZoomFactor = 100;
        public static int[] ScrollbarValue = new int[2] {0, 0};
        public static List<Label> ListLabelX = new List<Label>();
        public static List<Label> ListLabelY = new List<Label>();
        public static List<PictureBox> ListTickV = new List<PictureBox>();
        public static List<PictureBox> ListTickH = new List<PictureBox>();
        public static PictureBox PicMaxX = new PictureBox();
        public static PictureBox PicMaxY = new PictureBox();
        public static PictureBox PicHoverX = new PictureBox();
        public static PictureBox PicHoverY = new PictureBox();
        public static Label LblHoverX = new Label();
        public static Label LblHoverY = new Label();
        public static int Dx = 1;
        public static double MinX;
        public static double MaxX;
        public static double MinY;
        public static double MaxY;
        public static int DLabelX = 50;
        public static int DLabelY = 50;
        // public variable for panning
        public static bool FPan = false;
        public static Point PanStartMouse;
        public static int PanStartHScroll;
        public static int PanStartVScroll;
        // public variable for axis scaling
        public static bool FScaleX = false;
        public static bool FScaleY = false;
        public static Point ScaleMouse0;
        public static Point PicStart;
        // public variable for control handler
        private static PictureBox _picbox;
        private static Panel _panelX;
        private static Panel _panelY;
        private static NoScrollPanel _panelImage;
        private static Panel _panelGap;

        public static void Setup(PictureBox pictureBox, Panel xPanel, Panel yPanel, NoScrollPanel imagePanel, Panel gapPanel)
        {
            _picbox = pictureBox;
            _panelX = xPanel;
            _panelY = yPanel;
            _panelImage = imagePanel;
            _panelGap = gapPanel;
        }

        public static void GetScrollbarValue()
        { ScrollbarValue = new int[2] {_panelImage.HorizontalScroll.Value, _panelImage.VerticalScroll.Value}; }

        public static void SetScrollbarValue()
        {
            var val0 = ScrollbarValue[0];
            if (val0 > _panelImage.HorizontalScroll.Maximum) val0 = _panelImage.HorizontalScroll.Maximum;

            var val1 = ScrollbarValue[1];
            if (val1 > _panelImage.VerticalScroll.Maximum) val1 = _panelImage.VerticalScroll.Maximum;

            _panelImage.AutoScrollPosition = new Point(val0, val1);
            _picbox.Update();
            _panelImage.Update();
        }

        public static void ShowSeismic(Bitmap seismic, int tracecount, int maxtime)
        {
            var bmp0 = seismic;
            MaxX = tracecount + Dx;
            MaxY = maxtime;

            ImgOriginalSize[0] = bmp0.Width;
            ImgOriginalSize[1] = bmp0.Height;
            _picbox.SizeMode = PictureBoxSizeMode.AutoSize;
            _picbox.SizeMode = PictureBoxSizeMode.StretchImage;
            _picbox.Width = ImgOriginalSize[0];
            _picbox.Height = ImgOriginalSize[1];
            _picbox.Image = bmp0;

            ZoomFactor = 100;
            Image_Axis_Initialize();
        }

        public static void ShowSeismic(Bitmap seismic)
        {
            _picbox.SizeMode = PictureBoxSizeMode.StretchImage;
            _picbox.Image = seismic;
            _picbox.Update();
        }

        public static void Image_Axis_Initialize()
        {
            ClearLabelTickIfExist();
            CreateLabelTickList();

            // resize Y-axis panel
            resizeYAxisPanel();
            // show X-axis label and tick
            showAxisLabelTick_X();
            // show Y-axis label and tick
            showAxisLabelTick_Y();

            // show maximum axis indicator
            var pixPtrace = (int) Math.Floor(_picbox.Width/(MaxX - MinX));
            if (pixPtrace < 1)
                pixPtrace = 1;

            var with5 = PicMaxX;
            with5.Parent = _panelX;
            with5.BringToFront();
            with5.BackColor = Color.Red;
            with5.Height = _panelX.Height;
            with5.Width = pixPtrace;
            with5.Top = 0;
            with5.Left = ImgOriginalSize[0] - pixPtrace + _picbox.Left + 1 + _panelY.Width;
            with5.Show();
            with5.Update();

            var with6 = PicMaxY;
            with6.Parent = _panelY;
            with6.BringToFront();
            with6.BackColor = Color.Red;
            with6.Height = 1;
            with6.Width = _panelY.Width;
            with6.Top = (ImgOriginalSize[1] - 1 + _picbox.Top) + _panelGap.Height;
            with6.Left = 0;
            with6.Show();
            with6.Update();

            // prepare value-on-hover
            PrepareValueOnHover();
        }

        private static void CreateLabelTickList()
        {
            // create list of labels
            for (var i = 0; i <= Math.Floor(((double)Screen.PrimaryScreen.WorkingArea.Width / DLabelX)); i++)
            {
                if (ListLabelX != null) ListLabelX.Add(new Label());
                if (ListTickV != null) ListTickV.Add(new PictureBox());
            }


            for (var i = 0; i <= Math.Floor(((double)Screen.PrimaryScreen.WorkingArea.Height / DLabelY)) - 1; i++)
            {
                if (ListLabelY != null) ListLabelY.Add(new Label());
                if (ListTickH != null) ListTickH.Add(new PictureBox());
            }
        }

        private static void ClearLabelTickIfExist()
        {
            foreach (Label lbl in ListLabelY)
            {
                lbl.Hide();
            }

            foreach (Label lbl in ListLabelX)
            {
                lbl.Hide();
            }

            if (ListLabelX != null) ListLabelX.Clear();
            if (ListTickV != null) ListTickV.Clear();
            if (ListLabelY != null) ListLabelY.Clear();
            if (ListTickH != null) ListTickH.Clear();
        }

        private static void PrepareValueOnHover()
        {
            var with7 = PicHoverX;
            with7.Parent = _panelX;
            with7.BringToFront();
            with7.BackColor = Color.Green;
            with7.Height = _panelX.Height;
            with7.Width = 1;
            with7.Top = 0;
            with7.Left = -1;
            with7.Show();
            with7.Update();

            var with8 = LblHoverX;
            with8.Parent = _panelX;
            with8.BringToFront();
            with8.AutoSize = true;
            with8.BackColor = Color.Green;
            with8.ForeColor = Color.White;
            with8.Top = 10;

            var with9 = PicHoverY;
            with9.Parent = _panelY;
            with9.BringToFront();
            with9.BackColor = Color.Green;
            with9.Height = 1;
            with9.Width = _panelY.Width;
            with9.Top = -1;
            with9.Left = 0;
            with9.Show();
            with9.Update();

            var with10 = LblHoverY;
            with10.Parent = _panelY;
            with10.BringToFront();
            with10.AutoSize = true;
            with10.BackColor = Color.Green;
            with10.ForeColor = Color.White;
        }

        private static void resizeYAxisPanel()
        {
            Label dummy = new Label();
            dummy.Parent = _panelX; dummy.AutoSize = true;
            dummy.BackColor = _panelX.BackColor; dummy.ForeColor = _panelX.BackColor;
            dummy.Width = 1;
            dummy.Text = String.Concat(Math.Round(MaxY).ToString(), ".000");
            dummy.Update();

            _panelY.Parent.Width = dummy.Width + 15;

            dummy.Dispose();
        }

        private static void showAxisLabelTick_Y()
        {
            
            for (var i = 0; i <= ListLabelY.Count - 1; i++)
            {
                var with3 = ListTickH[i];
                with3.Parent = _panelY;
                with3.BackColor = Color.Black;
                with3.Height = 1;
                with3.Width = 10;
                with3.Top = (i * DLabelY) + _panelGap.Height;
                with3.Left = _panelY.Width - with3.Width;
                with3.Show();
                //with3.Update();

                var with4 = ListLabelY[i];
                with4.Parent = _panelY;
                with4.AutoSize = true;
                with4.Text =
                    Math.Round(MinY + (i * ((MaxY - MinY) / (_picbox.Height-1))), 3).ToString();
                with4.Left = ListTickH[i].Left - with4.Width;
                with4.Top = (int)Math.Round((double)(ListTickH[i].Top - (with4.Height / 2)));
                with4.Show();
                //with4.Update();
            }

            _panelY.Invalidate(); _panelY.Update();
        }

        private static void showAxisLabelTick_X()
        {
            // show X-axis label and tick
            if ((ListLabelX == null) || (ListTickV == null)) return;
            if ((ListLabelY == null) || (ListTickH == null)) return;

            for (var i = 0; i < ListLabelX.Count; i++)
            {
                var val0 = MinX + ((i * DLabelX) * ((MaxX - MinX) / _picbox.Width));

                var with1 = ListTickV[i];
                with1.Parent = _panelX;
                with1.BringToFront();
                with1.BackColor = Color.Black;
                with1.Height = 10;
                with1.Width = 1;
                with1.Top = _panelX.Height - with1.Height;
                with1.Left = (i * DLabelX) + _panelY.Width;
                with1.Show();
                //with1.Update();

                var with2 = ListLabelX[i];
                with2.Parent = _panelX;
                with2.BringToFront();
                with2.AutoSize = true;
                with2.Text = Math.Floor(val0).ToString(CultureInfo.InvariantCulture);
                with2.Top = 10;
                with2.Left = (int)Math.Round((double)(ListTickV[i].Left - (with2.Width / 2)));
                with2.Show();
                //with2.Update();
            }

            _panelX.Invalidate(); _panelX.Update();
        }

        public static void Image_Axis_Update()
        {
            // move maximum X-axis and Y-axis indicator
            var pixPtrace = (int) Math.Floor(_picbox.Width/(MaxX - MaxX));
            if (pixPtrace < 1)
                pixPtrace = 1;

            var with1 = PicMaxX;
            with1.Width = pixPtrace;
            with1.Left = _picbox.Width - pixPtrace + _picbox.Left + 1 + _panelY.Width;
            with1.Update();

            var with2 = PicMaxY;
            with2.Top = (_picbox.Height - 1 + _picbox.Top) + _panelGap.Height;
            with2.Update();

            // update X-axis label
            for (var i = 0; i <= ListLabelX.Count - 1; i++)
            {
                var val0 = MinX + (((i*DLabelX) - _picbox.Left)*((MaxX - MinX)/_picbox.Width));

                var with3 = ListTickV[i];
                with3.Height = 10;
                with3.Width = 1;
                if (with3.Left >= PicMaxX.Left)
                    with3.BackColor = Color.Red;
                else
                    with3.BackColor = Color.Black;
                with3.Update();

                var with4 = ListLabelX[i];
                with4.Text = Math.Floor(val0).ToString(CultureInfo.InvariantCulture);
                with4.Left = (int) Math.Round((double) ((ListTickV[i].Left + ListTickV[i].Width/2) - (with4.Width/2)));
                with4.ForeColor = ListTickV[i].BackColor;
                with4.Update();
            }

            // update Y-axis label
            for (var i = 0; i <= ListLabelY.Count - 1; i++)
            {
                var val0 = MinY + (((i*DLabelY) - _picbox.Top)*((MaxY - MinY)/(_picbox.Height-1)));

                var with5 = ListTickH[i];
                with5.Height = 1;
                with5.Width = 10;
                if (with5.Top >= PicMaxY.Top)
                    with5.BackColor = Color.Red;
                else
                    with5.BackColor = Color.Black;
                with5.Update();

                var with6 = ListLabelY[i];
                with6.Text = Math.Round(val0, 3).ToString();
                with6.Left = ListTickH[i].Left - with6.Width;
                with6.ForeColor = ListTickH[i].BackColor;
                with6.Update();
            }

            _panelImage.Invalidate();
            _panelImage.Update();
        }

        public static void Image_ValueOnHover(Point mouse)
        {
            // calculate pointer in resized image
            var scaled = default(Point);
            scaled.X = (ImgOriginalSize[0]*_picbox.PointToClient(mouse).X/_picbox.Width);
            scaled.Y = (ImgOriginalSize[1]*_picbox.PointToClient(mouse).Y/_picbox.Height);

            // scale mouse position to unscaled axis and calculate selected trace
            var unscTrace = MinX + (scaled.X*(MaxX - MinX)/ImgOriginalSize[0]);
            var unscFloor = (int) Math.Floor(unscTrace);
            int unscCeiln;
            if (Math.Abs(Math.Ceiling(unscTrace) - unscFloor) < 0.000001)
                unscCeiln = unscFloor + 1;
            else
                unscCeiln = (int) Math.Ceiling(unscTrace);

            // translate selected trace back to its pixel position
            var pixlFloor = (unscFloor - MinX)*ImgOriginalSize[0]/(MaxX - MinX);
            var pixlCeiln = (unscCeiln - MinX)*ImgOriginalSize[0]/(MaxX - MinX);

            // scale pixel position to current image size
            var scalFloor = pixlFloor*_picbox.Width/ImgOriginalSize[0];
            var scalCeiln = pixlCeiln*_picbox.Width/ImgOriginalSize[0];
            var scalPxTrc = scalCeiln - scalFloor;
            if (scalPxTrc < 1)
                scalPxTrc = 1;

            var with1 = PicHoverX;
            with1.Width = (int) Math.Round(scalPxTrc);
            with1.Left = (int) Math.Round(scalFloor) + 1 + _panelY.Width + _picbox.Left;
            with1.Invalidate();
            with1.Update();

            var with2 = LblHoverX;
            with2.Text = unscFloor.ToString();
            with2.Left = (PicHoverX.Left + PicHoverX.Width/2) - (with2.Width/2);
            with2.Invalidate();
            with2.Update();

            var with3 = PicHoverY;
            with3.Top = _picbox.PointToClient(mouse).Y + _panelGap.Height + _picbox.Top;
            with3.Invalidate();

            var with4 = LblHoverY;
            with4.Text = Math.Round(MinY + (scaled.Y*((MaxY - MinY)/(ImgOriginalSize[1]-1))), 3).ToString(CultureInfo.InvariantCulture);
            with4.Top =
                (int)
                    Math.Round((double) (_picbox.PointToClient(mouse).Y + _panelGap.Height + _picbox.Top - with4.Height/2));
            with4.Left = _panelY.Width - with4.Width - 10;
            with4.Invalidate();
        }

        public static void Image_Pan(Point mouse)
        {
            var panEndMouse = _panelImage.PointToClient(mouse);
            var panShiftMouse = panEndMouse - (Size) PanStartMouse;

            var panEndHScroll = PanStartHScroll - panShiftMouse.X;
            if (panEndHScroll <= 0)
                panEndHScroll = 0;
            if (panEndHScroll >= _panelImage.HorizontalScroll.Maximum)
                panEndHScroll = _panelImage.HorizontalScroll.Maximum;

            var panEndVScroll = PanStartVScroll - panShiftMouse.Y;
            if (panEndVScroll <= 0)
                panEndVScroll = 0;
            if (panEndVScroll >= _panelImage.VerticalScroll.Maximum)
                panEndVScroll = _panelImage.VerticalScroll.Maximum;

            if (_panelImage.HorizontalScroll.Visible)
                _panelImage.HorizontalScroll.Value = panEndHScroll;
            if (_panelImage.VerticalScroll.Visible)
                _panelImage.VerticalScroll.Value = panEndVScroll;

            _panelImage.Invalidate();
            _panelImage.Update();
        }

        public static void Image_ZoomByValue(int zoomfactor)
        {
            // zoom image
            var newWidth = Math.Round(ImgOriginalSize[0]*(double) zoomfactor/100);
            var newHeight = Math.Round(ImgOriginalSize[1]*(double) zoomfactor/100);
            var picNewSize = new Point((int) newWidth, (int) newHeight);

            _picbox.SizeMode = PictureBoxSizeMode.StretchImage;
            _picbox.Width = picNewSize.X;
            _picbox.Height = picNewSize.Y;

            ZoomFactor = (double)zoomfactor;

            _panelImage.Invalidate();
            _panelImage.Update();
        }

        public static void Image_ZoomToScreen()
        {
            int zoomfactor = (int)ZoomFactor;

            // if image width > image height -> fit to width
            if (_picbox.Width >= _picbox.Height)
            {
                zoomfactor = (int)Math.Round((((double)_panelImage.Width - (double)SystemInformation.VerticalScrollBarWidth) /
                    (double)ImgOriginalSize[0]) * 100);
            }

            // if image width < image height -> fit to height
            if (_picbox.Width < _picbox.Height)
            {
                zoomfactor = (int)Math.Round((((double)_panelImage.Height - (double)SystemInformation.HorizontalScrollBarHeight) /
                    (double)ImgOriginalSize[1]) * 100);
            }

            Image_ZoomByValue(zoomfactor);
        }

        public static void Image_MouseCenteredZoom(int zoomfactor, Point mouse)
        {
            // save initial parameter for mouse centered zoom
            var picStart = new Point(_picbox.PointToClient(mouse).X, _picbox.PointToClient(mouse).Y);
            var picSizeStart = new Point(_picbox.Width, _picbox.Height);

            // zoom image
            var newWidth = Math.Round(ImgOriginalSize[0]*(double) zoomfactor/100);
            var newHeight = Math.Round(ImgOriginalSize[1]*(double) zoomfactor/100);
            var picNewSize = new Point((int) newWidth, (int) newHeight);

            _picbox.SizeMode = PictureBoxSizeMode.StretchImage;
            _picbox.Width = picNewSize.X;
            _picbox.Height = picNewSize.Y;

            _panelImage.Invalidate();

            // mouse centered zoom
            var newShiftX = Math.Round(PicStart.X - (PicStart.X/(double) picSizeStart.X)*picNewSize.X);
            var newShiftY = Math.Round(picStart.Y - (picStart.Y/(double) picSizeStart.Y)*picNewSize.Y);
            var picPosShift = new Point((int) newShiftX, (int) newShiftY);

            var picPosEnd = new Point(_picbox.Left + picPosShift.X, _picbox.Top + picPosShift.Y);

            var panEndHScroll = -picPosEnd.X;
            if (panEndHScroll <= 0)
                panEndHScroll = 0;
            if (panEndHScroll >= _panelImage.HorizontalScroll.Maximum)
                panEndHScroll = _panelImage.HorizontalScroll.Maximum;

            var panEndVScroll = -picPosEnd.Y;
            if (panEndVScroll <= 0)
                panEndVScroll = 0;
            if (panEndVScroll >= _panelImage.VerticalScroll.Maximum)
                panEndVScroll = _panelImage.VerticalScroll.Maximum;

            _panelImage.HorizontalScroll.Value = panEndHScroll;
            _panelImage.VerticalScroll.Value = panEndVScroll;

            _panelImage.Update();
        }

        public static void Image_Axis_StretchShrink(int newWidth, int newHeight)
        {
            // for X-axis stretch/shrink
            ImgOriginalSize[0] = newWidth;

            _picbox.Width = ImgOriginalSize[0];
            _picbox.Update();

            // for Y-axis stretch/shrink
            ImgOriginalSize[1] = newHeight;

            _picbox.Height = ImgOriginalSize[1];
            _picbox.Update();

            // redraw seismic view
            _panelImage.Invalidate();
            _panelImage.Update();
        }

        public static void Image_Axis_StretchShrink(Point mouse)
        {
            // for X-axis stretch/shrink
            if (FScaleX)
            {
                var scaleMouse1 = _panelX.PointToClient(mouse);

                // translate mouse shift to the amount of size change
                double scl0 = (scaleMouse1.X - ScaleMouse0.X)*3;
                ImgOriginalSize[0] = (int) (ImgOriginalSize[0] + scl0);

                // resize in X-direction
                _picbox.Width = ImgOriginalSize[0];
                _picbox.Update();

                ScaleMouse0 = scaleMouse1;
            }

            // for Y-axis stretch/shrink
            if (FScaleY)
            {
                var scaleMouse1 = _panelY.PointToClient(mouse);

                // translate mouse shift to the amount of size change
                double scl1 = (scaleMouse1.Y - ScaleMouse0.Y)*3;
                ImgOriginalSize[1] = (int) (ImgOriginalSize[1] + scl1);

                // resize in Y-direction
                _picbox.Height = ImgOriginalSize[1];
                _picbox.Update();

                ScaleMouse0 = scaleMouse1;
            }

            _panelImage.Invalidate();
            _panelImage.Update();
        }

        public static void Image_Axis_StretchShrink(bool FitToWidth, bool FitToHeight)
        {
            // Resize X-Axis -> Fit To Width
            if (FitToWidth == true)
            {
                ImgOriginalSize[0] = _panelImage.Width - SystemInformation.VerticalScrollBarWidth;

                _picbox.Width = ImgOriginalSize[0];
                _picbox.Update();
            }

            // Resize Y-Axis -> Fit To Height
            if (FitToHeight == true)
            {
                ImgOriginalSize[1] = _panelImage.Height;

                _picbox.Height = ImgOriginalSize[1] - SystemInformation.HorizontalScrollBarHeight;
                _picbox.Update();
            }

            // redraw seismic view
            _panelImage.Invalidate();
            _panelImage.Update();
        }


    }
}