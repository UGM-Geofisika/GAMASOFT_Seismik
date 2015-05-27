using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace SegyView
{
    public static class GamaSeismicViewer
    {
        public static int[] ImgOriginalSize = new int[3];
        public static double ZoomFactor = 100;
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
        public static double Dy = 0.033;
        public static double MinX;
        public static double MaxX;
        public static double MinY = 0;
        public static double MaxY = 5;
        public static int DLabelX = 50;
        public static int DLabelY = 50;
        // public variable for panning
        public static bool FPan = false;
        public static Point PanStartMouse;
        public static int PanStartHScroll;
        public static int PanStartVScroll;
        //public variable for axis scaling
        public static bool FScaleX = false;
        public static bool FScaleY = false;
        public static Point ScaleMouse0;
        public static Point PicStart;

        private static PictureBox picbox;
        private static Panel panelX;
        private static Panel panelY;
        private static NoScrollPanel panelImage;
        private static Panel panelGap;

        public static void Setup(PictureBox pictureBox, Panel xPanel, Panel yPanel, NoScrollPanel imagePanel, Panel gapPanel)
        {
            picbox = pictureBox;
            panelX = xPanel;
            panelY = yPanel;
            panelImage = imagePanel;
            panelGap = gapPanel;

        }
        public static void ShowSeismic(Bitmap seismic, int tracecount, int maxtime)
        {
            var bmp0 = seismic;
            MaxX = tracecount + Dx;
            MaxY = maxtime;

            ImgOriginalSize[0] = bmp0.Width;
            ImgOriginalSize[1] = bmp0.Height;
            picbox.SizeMode = PictureBoxSizeMode.AutoSize;

            picbox.SizeMode = PictureBoxSizeMode.StretchImage;
            picbox.Width = ImgOriginalSize[0];
            picbox.Height = ImgOriginalSize[1];
            picbox.Image = bmp0;
        }

        public static void Image_Axis_Initialize()
        {
            if (ListLabelX != null) ListLabelX.Clear();
            if (ListTickV != null) ListTickV.Clear();
            if (ListLabelY != null) ListLabelY.Clear();
            if (ListTickH != null) ListTickH.Clear();

            // create list of labels
            for (var i = 0; i <= Math.Floor(((double) ImgOriginalSize[0]/DLabelX)); i++)
            {
                if (ListLabelX != null) ListLabelX.Add(new Label());
                if (ListTickV != null) ListTickV.Add(new PictureBox());
            }


            for (var i = 0; i <= Math.Floor(((double) ImgOriginalSize[1]/DLabelY)) - 1; i++)
            {
                if (ListLabelY != null) ListLabelY.Add(new Label());
                if (ListTickH != null) ListTickH.Add(new PictureBox());
            }

            // auto-adjust Y-axis panel width based on last Y-value label position
            var dummy = new Label();
            var with0 = dummy;
            with0.Parent = panelY;
            with0.AutoSize = true;

            if (ListLabelY != null)
                with0.Text =
                    Math.Round(MinY + ((ListLabelY.Count - 1)*((MaxY - MinY)/ImgOriginalSize[1])), 3)
                        .ToString(CultureInfo.InvariantCulture);

            with0.Left = 5;
            with0.Hide();

            panelY.Parent.Width = with0.Left + with0.Width + 15;
            dummy.Dispose();

            // show X-axis label and tick
            if ((ListLabelX == null) || (ListTickV == null)) return;
            if ((ListLabelY == null) || (ListTickH == null)) return;

            for (var i = 0; i < ListLabelX.Count; i++)
            {
                var val0 = MinX + ((i*DLabelX)*((MaxX - MinX)/picbox.Width));

                var with1 = ListTickV[i];
                with1.Parent = panelX;
                with1.BringToFront();
                with1.BackColor = Color.Black;
                with1.Height = 10;
                with1.Width = 1;
                with1.Top = panelX.Height - with1.Height;
                with1.Left = (i*DLabelX) + panelY.Width;
                with1.Show();
                with1.Update();

                var with2 = ListLabelX[i];
                with2.Parent = panelX;
                with2.BringToFront();
                with2.AutoSize = true;
                with2.Text = Math.Floor(val0).ToString(CultureInfo.InvariantCulture);
                with2.Top = 10;
                with2.Left = (int) Math.Round((double) (ListTickV[i].Left - (with2.Width/2)));
                with2.Show();
                with2.Update();
            }

            // show Y-axis label and tick
            for (var i = 0; i <= ListLabelY.Count - 1; i++)
            {
                var with3 = ListTickH[i];
                with3.Parent = panelY;
                with3.BackColor = Color.Black;
                with3.Height = 1;
                with3.Width = 10;
                with3.Top = (i*DLabelY) + panelGap.Height;
                with3.Left = panelY.Width - with3.Width;
                with3.Show();
                with3.Update();

                var with4 = ListLabelY[i];
                with4.Parent = panelY;
                with4.AutoSize = true;
                with4.Text =
                    Math.Round(MinY + (i*((MaxY - MinY)/picbox.Height)), 3).ToString(CultureInfo.InvariantCulture);
                with4.Left = ListTickH[i].Left - with4.Width;
                with4.Top = (int) Math.Round((double) (ListTickH[i].Top - (with4.Height/2)));
                with4.Show();
                with4.Update();
            }

            // show maximum axis indicator
            var pixPtrace = (int) Math.Floor(picbox.Width/(MaxX - MinX));
            if (pixPtrace < 1)
                pixPtrace = 1;

            var with5 = PicMaxX;
            with5.Parent = panelX;
            with5.BringToFront();
            with5.BackColor = Color.Red;
            with5.Height = panelX.Height;
            with5.Width = pixPtrace;
            with5.Top = 0;
            with5.Left = ImgOriginalSize[0] - pixPtrace + picbox.Left + 1 + panelY.Width;
            with5.Show();
            with5.Update();

            var with6 = PicMaxY;
            with6.Parent = panelY;
            with6.BringToFront();
            with6.BackColor = Color.Red;
            with6.Height = 1;
            with6.Width = panelY.Width;
            with6.Top = (ImgOriginalSize[1] - 1 + picbox.Top) + panelGap.Height;
            with6.Left = 0;
            with6.Show();
            with6.Update();

            // prepare value-on-hover
            var with7 = PicHoverX;
            with7.Parent = panelX;
            with7.BringToFront();
            with7.BackColor = Color.Green;
            with7.Height = panelX.Height;
            with7.Width = 1;
            with7.Top = 0;
            with7.Left = -1;
            with7.Show();
            with7.Update();

            var with8 = LblHoverX;
            with8.Parent = panelX;
            with8.BringToFront();
            with8.AutoSize = true;
            with8.BackColor = Color.Green;
            with8.ForeColor = Color.White;
            with8.Top = 10;

            var with9 = PicHoverY;
            with9.Parent = panelY;
            with9.BringToFront();
            with9.BackColor = Color.Green;
            with9.Height = 1;
            with9.Width = panelY.Width;
            with9.Top = -1;
            with9.Left = 0;
            with9.Show();
            with9.Update();

            var with10 = LblHoverY;
            with10.Parent = panelY;
            with10.BringToFront();
            with10.AutoSize = true;
            with10.BackColor = Color.Green;
            with10.ForeColor = Color.White;

            // prepare grid lines
            //Bitmap gv = new Bitmap(1, 6);
            //Graphics ggv = Graphics.FromImage(gv);
            //ggv.DrawLine(Pens.Black, 0, 0, 0, 2);
            //ggv.DrawLine(Pens.White, 0, 3, 0, 5);
            //ggv.Dispose();
            //
            //Bitmap gh = new Bitmap(6, 1);
            //Graphics ggh = Graphics.FromImage(gh);
            //ggh.DrawLine(Pens.Black, 0, 0, 2, 0);
            //ggh.DrawLine(Pens.White, 3, 0, 5, 0);
            //ggh.Dispose();
        }

        public static void Image_Axis_Update()
        {
            // move maximum X-axis and Y-axis indicator
            var pixPtrace = (int) Math.Floor(picbox.Width/(MaxX - MaxX));
            if (pixPtrace < 1)
                pixPtrace = 1;

            var with1 = PicMaxX;
            with1.Width = pixPtrace;
            with1.Left = picbox.Width - pixPtrace + picbox.Left + 1 + panelY.Width;
            with1.Update();

            var with2 = PicMaxY;
            with2.Top = (picbox.Height - 1 + picbox.Top) + panelGap.Height;
            with2.Update();

            // update X-axis label
            for (var i = 0; i <= ListLabelX.Count - 1; i++)
            {
                var val0 = MinX + (((i*DLabelX) - picbox.Left)*((MaxX - MinX)/picbox.Width));

                var with3 = ListTickV[i];
                with3.Height = 10;
                with3.Width = 1;
                if (with3.Left >= PicMaxX.Left)
                    with3.BackColor = Color.Red;
                else
                    with3.BackColor = Color.Black;
                with3.Update();

                var with4 = ListLabelX[i];
                with4.Text = Math.Floor(val0).ToString();
                with4.Left = (int) Math.Round((double) ((ListTickV[i].Left + ListTickV[i].Width/2) - (with4.Width/2)));
                with4.ForeColor = ListTickV[i].BackColor;
                with4.Update();
            }

            // update Y-axis label
            for (var i = 0; i <= ListLabelY.Count - 1; i++)
            {
                var val0 = MinY + (((i*DLabelY) - picbox.Top)*((MaxY - MinY)/picbox.Height));

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

            panelImage.Invalidate();
            panelImage.Update();
        }

        public static void Image_ValueOnHover(Point mouse)
        {
            // calculate pointer in resized image
            var scaled = default(Point);
            scaled.X = (ImgOriginalSize[0]*picbox.PointToClient(mouse).X/picbox.Width);
            scaled.Y = (ImgOriginalSize[1]*picbox.PointToClient(mouse).Y/picbox.Height);

            // scale mouse position to unscaled axis and calculate selected trace
            var unscTrace = MinX + (scaled.X*(MaxX - MinX)/ImgOriginalSize[0]);
            var unscFloor = (int) Math.Floor(unscTrace);
            var unscCeiln = 0;
            if (Math.Ceiling(unscTrace) == unscFloor)
                unscCeiln = unscFloor + 1;
            else
                unscCeiln = (int) Math.Ceiling(unscTrace);

            // translate selected trace back to its pixel position
            var pixlFloor = (unscFloor - MinX)*ImgOriginalSize[0]/(MaxX - MinX);
            var pixlCeiln = (unscCeiln - MinX)*ImgOriginalSize[0]/(MaxX - MinX);
            var pixpTrace = pixlCeiln - pixlFloor;

            // scale pixel position to current image size
            var scalFloor = pixlFloor*picbox.Width/ImgOriginalSize[0];
            var scalCeiln = pixlCeiln*picbox.Width/ImgOriginalSize[0];
            var scalPxTrc = scalCeiln - scalFloor;
            if (scalPxTrc < 1)
                scalPxTrc = 1;

            var with1 = PicHoverX;
            with1.Width = (int) Math.Round(scalPxTrc);
            with1.Left = (int) Math.Round(scalFloor) + 1 + panelY.Width + picbox.Left;
            with1.Invalidate();
            with1.Update();

            var with2 = LblHoverX;
            with2.Text = unscFloor.ToString();
            with2.Left = (PicHoverX.Left + PicHoverX.Width/2) - (with2.Width/2);
            with2.Invalidate();
            with2.Update();

            var with3 = PicHoverY;
            with3.Top = picbox.PointToClient(mouse).Y + panelGap.Height + picbox.Top;
            with3.Invalidate();

            var with4 = LblHoverY;
            with4.Text = Math.Round(MinY + (scaled.Y*((MaxY - MinY)/ImgOriginalSize[1])), 3).ToString();
            with4.Top =
                (int)
                    Math.Round((double) (picbox.PointToClient(mouse).Y + panelGap.Height + picbox.Top - with4.Height/2));
            with4.Left = panelY.Width - with4.Width - 10;
            with4.Invalidate();
        }

        public static void Image_Pan(Point mouse)
        {
            var panEndMouse = panelImage.PointToClient(mouse);
            var panShiftMouse = panEndMouse - (Size) PanStartMouse;

            var panEndHScroll = PanStartHScroll - panShiftMouse.X;
            if (panEndHScroll <= 0)
                panEndHScroll = 0;
            if (panEndHScroll >= panelImage.HorizontalScroll.Maximum)
                panEndHScroll = panelImage.HorizontalScroll.Maximum;

            var panEndVScroll = PanStartVScroll - panShiftMouse.Y;
            if (panEndVScroll <= 0)
                panEndVScroll = 0;
            if (panEndVScroll >= panelImage.VerticalScroll.Maximum)
                panEndVScroll = panelImage.VerticalScroll.Maximum;

            if (panelImage.HorizontalScroll.Visible)
                panelImage.HorizontalScroll.Value = panEndHScroll;
            if (panelImage.VerticalScroll.Visible)
                panelImage.VerticalScroll.Value = panEndVScroll;

            panelImage.Invalidate();
            panelImage.Update();
        }

        public static void Image_ZoomByValue(int zoomfactor)
        {
            // zoom image
            var newWidth = Math.Round(ImgOriginalSize[0]*(double) zoomfactor/100);
            var newHeight = Math.Round(ImgOriginalSize[1]*(double) zoomfactor/100);
            var picNewSize = new Point((int) newWidth, (int) newHeight);

            picbox.SizeMode = PictureBoxSizeMode.StretchImage;
            picbox.Width = picNewSize.X;
            picbox.Height = picNewSize.Y;

            ZoomFactor = zoomfactor;

            panelImage.Invalidate();
            panelImage.Update();
        }

        public static void Image_MouseCenteredZoom(int zoomfactor, Point mouse)
        {
            // save initial parameter for mouse centered zoom
            var picStart = new Point(picbox.PointToClient(mouse).X, picbox.PointToClient(mouse).Y);
            var picSizeStart = new Point(picbox.Width, picbox.Height);

            // zoom image
            var newWidth = Math.Round(ImgOriginalSize[0]*(double) zoomfactor/100);
            var newHeight = Math.Round(ImgOriginalSize[1]*(double) zoomfactor/100);
            var picNewSize = new Point((int) newWidth, (int) newHeight);

            picbox.SizeMode = PictureBoxSizeMode.StretchImage;
            picbox.Width = picNewSize.X;
            picbox.Height = picNewSize.Y;

            panelImage.Invalidate();

            // mouse centered zoom
            var newShiftX = Math.Round(PicStart.X - (PicStart.X/(double) picSizeStart.X)*picNewSize.X);
            var newShiftY = Math.Round(picStart.Y - (picStart.Y/(double) picSizeStart.Y)*picNewSize.Y);
            var picPosShift = new Point((int) newShiftX, (int) newShiftY);

            var picPosEnd = new Point(picbox.Left + picPosShift.X, picbox.Top + picPosShift.Y);

            var panEndHScroll = -picPosEnd.X;
            if (panEndHScroll <= 0)
                panEndHScroll = 0;
            if (panEndHScroll >= panelImage.HorizontalScroll.Maximum)
                panEndHScroll = panelImage.HorizontalScroll.Maximum;

            var panEndVScroll = -picPosEnd.Y;
            if (panEndVScroll <= 0)
                panEndVScroll = 0;
            if (panEndVScroll >= panelImage.VerticalScroll.Maximum)
                panEndVScroll = panelImage.VerticalScroll.Maximum;

            panelImage.HorizontalScroll.Value = panEndHScroll;
            panelImage.VerticalScroll.Value = panEndVScroll;

            panelImage.Update();
        }

        public static void Image_Axis_StretchShrink(int newWidth, int newHeight)
        {
            // for X-axis stretch/shrink
            ImgOriginalSize[0] = newWidth;

            picbox.Width = ImgOriginalSize[0];
            picbox.Update();

            // for Y-axis stretch/shrink
            ImgOriginalSize[1] = newHeight;

            picbox.Height = ImgOriginalSize[1];
            picbox.Update();

            // redraw seismic view
            panelImage.Invalidate();
            panelImage.Update();
        }

        public static void Image_Axis_StretchShrink(Point mouse)
        {
            // for X-axis stretch/shrink
            if (FScaleX)
            {
                var scaleMouse1 = panelX.PointToClient(mouse);

                // translate mouse shift to the amount of size change
                double scl0 = (scaleMouse1.X - ScaleMouse0.X)*3;
                ImgOriginalSize[0] = (int) (ImgOriginalSize[0] + scl0);

                // resize in X-direction
                picbox.Width = ImgOriginalSize[0];
                picbox.Update();

                ScaleMouse0 = scaleMouse1;
            }

            // for Y-axis stretch/shrink
            if (FScaleY)
            {
                var scaleMouse1 = panelY.PointToClient(mouse);

                // translate mouse shift to the amount of size change
                double scl1 = (scaleMouse1.Y - ScaleMouse0.Y)*3;
                ImgOriginalSize[1] = (int) (ImgOriginalSize[1] + scl1);

                // resize in Y-direction
                picbox.Height = ImgOriginalSize[1];
                picbox.Update();

                ScaleMouse0 = scaleMouse1;
            }

            panelImage.Invalidate();
            panelImage.Update();
        }
    }
}