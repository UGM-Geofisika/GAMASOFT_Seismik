using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms; 
using System.Drawing;
using System.Globalization;

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
            public static PictureBox PicMaxY   = new PictureBox();
            public static PictureBox PicHoverX = new PictureBox();
            public static PictureBox PicHoverY = new PictureBox();
            public static Label LblHoverX = new Label();
            public static Label  LblHoverY = new Label();

        public static int dx = 1;
        public static double dy = 0.033;
        public static double MinX = 15;
        public static double MaxX = 1000 + dx;
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

        public static void ShowDummyImages(PictureBox picbox)
        {
        	Bitmap bmp0 = new Bitmap("C:/Users/Geoseismal/Documents/GitHub/GAMASOFT_Seismik/TestVBAxis2Bitmap/TestVBAxis2Bitmap/bin/Debug/segybitmap.bmp");
        	ImgOriginalSize[0] = bmp0.Width;
            ImgOriginalSize[1] = bmp0.Height;
        	picbox.SizeMode = PictureBoxSizeMode.AutoSize;
            
        	picbox.SizeMode = PictureBoxSizeMode.StretchImage;
        	picbox.Width = ImgOriginalSize[0];
        	picbox.Height = ImgOriginalSize[1];
        	picbox.Image = bmp0;
        }

        public static void Image_Axis_Initialize(PictureBox picbox, Panel panelX, Panel panelY, NoScrollPanel panelImage, Panel panelGap)
        {
            
            if (ListLabelX != null) ListLabelX.Clear();
            if (ListTickV != null) ListTickV.Clear();
            if (ListLabelY != null) ListLabelY.Clear();
            if (ListTickH != null) ListTickH.Clear();

            // create list of labels
            for (int i = 0; i <= Math.Floor(((double) ImgOriginalSize[0] / DLabelX)); i++)
            {
                if (ListLabelX != null) ListLabelX.Add(new Label());
                if (ListTickV != null) ListTickV.Add(new PictureBox());
            }
           

            for (int i = 0; i <= Math.Floor(((double) ImgOriginalSize[1] / DLabelY)) - 1; i++)
            {
                if (ListLabelY != null) ListLabelY.Add(new Label());
                if (ListTickH != null) ListTickH.Add(new PictureBox());
            }

            // auto-adjust Y-axis panel width based on last Y-value label position
            Label dummy = new Label();
            var with0 = dummy;
            with0.Parent = panelY; with0.AutoSize = true;

            if (ListLabelY != null)
                with0.Text = Math.Round(MinY + ((ListLabelY.Count - 1) * ((MaxY - MinY) / ImgOriginalSize[1])), 3).ToString(CultureInfo.InvariantCulture);

            with0.Left = 5;
            with0.Hide();
            
            panelY.Parent.Width = with0.Left + with0.Width + 15;
            dummy.Dispose();

            // show X-axis label and tick
            if ((ListLabelX == null) || (ListTickV == null)) return;
            if ((ListLabelY == null) || (ListTickH == null)) return;

            for (int i = 0; i < ListLabelX.Count; i++)
            {
                double val0 = MinX + ((i * DLabelX) * ((MaxX - MinX) / picbox.Width));
                Debug.WriteLine(picbox.Width);

                var with1 = ListTickV[i];
                with1.Parent = panelX;
                with1.BringToFront();
                with1.BackColor = Color.Black;
                with1.Height = 10;
                with1.Width = 1;
                with1.Top = panelX.Height - with1.Height;
                with1.Left = (i * DLabelX) + panelY.Width;
                with1.Show();
                with1.Update();
                
                var with2 = ListLabelX[i];
                with2.Parent = panelX;
                with2.BringToFront();
                with2.AutoSize = true;
                with2.Text = Math.Floor(val0).ToString(CultureInfo.InvariantCulture);
                with2.Top = 10;
                with2.Left = (int) Math.Round((double) (ListTickV[i].Left - (with2.Width / 2)));
                with2.Show();
                with2.Update();
            }

            // show Y-axis label and tick
            for (int i = 0; i <= ListLabelY.Count - 1; i++)
            {
                var with3 = ListTickH[i];
                with3.Parent = panelY;
                with3.BackColor = Color.Black;
                with3.Height = 1;
                with3.Width = 10;
                with3.Top = (i * DLabelY) + panelGap.Height;
                with3.Left = panelY.Width - with3.Width;
                with3.Show();
                with3.Update();
            
                var with4 = ListLabelY[i];
                with4.Parent = panelY;
                with4.AutoSize = true;
                with4.Text = Math.Round(MinY + (i * ((MaxY - MinY) / picbox.Height)), 3).ToString(CultureInfo.InvariantCulture);
                with4.Left = ListTickH[i].Left - with4.Width;
                with4.Top = (int) Math.Round((double) (ListTickH[i].Top - (with4.Height / 2)));
                with4.Show();
                with4.Update();
            }
            
            // show maximum axis indicator
            int pixPtrace = (int) Math.Floor((double) (picbox.Width / (MaxX - MinX)));
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

        public static void Image_Axis_Update(PictureBox picbox, Panel panelX, Panel panelY, Panel panelImage, Panel panelGap)
        {
            // move maximum X-axis and Y-axis indicator
            int pixPtrace = (int) Math.Floor(picbox.Width / (MaxX - MaxX));
            if (pixPtrace < 1)
                pixPtrace = 1;

            var _with1 = PicMaxX;
            _with1.Width = pixPtrace;
            _with1.Left = picbox.Width - pixPtrace + picbox.Left + 1 + panelY.Width;
            _with1.Update();

            var _with2 = PicMaxY;
            _with2.Top = (picbox.Height - 1 + picbox.Top) + panelGap.Height;
            _with2.Update();

            // update X-axis label
            for (int i = 0; i <= ListLabelX.Count - 1; i++)
            {
                double val0 = MinX + (((i * DLabelX) - picbox.Left) * ((MaxX - MinX) / picbox.Width));

                var _with3 = ListTickV[i];
                _with3.Height = 10;
                _with3.Width = 1;
                if (_with3.Left >= PicMaxX.Left)
                    _with3.BackColor = Color.Red;
                else
                    _with3.BackColor = Color.Black;
                _with3.Update();

                var _with4 = ListLabelX[i];
                _with4.Text = Math.Floor(val0).ToString();
                _with4.Left = (int) Math.Round((double) ((ListTickV[i].Left + ListTickV[i].Width / 2) - (_with4.Width / 2)));
                _with4.ForeColor = ListTickV[i].BackColor;
                _with4.Update();
            }

            // update Y-axis label
            for (int i = 0; i <= ListLabelY.Count - 1; i++)
            {
                double val0 = MinY + (((i * DLabelY) - picbox.Top) * ((MaxY - MinY) / picbox.Height));

                var _with5 = ListTickH[i];
                _with5.Height = 1;
                _with5.Width = 10;
                if (_with5.Top >= PicMaxY.Top)
                    _with5.BackColor = Color.Red;
                else
                    _with5.BackColor = Color.Black;
                _with5.Update();

                var _with6 = ListLabelY[i];
                _with6.Text = Math.Round(val0, 3).ToString();
                _with6.Left = ListTickH[i].Left - _with6.Width;
                _with6.ForeColor = ListTickH[i].BackColor;
                _with6.Update();
            }

            panelImage.Invalidate();
            panelImage.Update();

        }

        public static void Image_ValueOnHover(Panel panelGap, Panel panelY, PictureBox picbox, Point Mouse)
        {
            // calculate pointer in resized image
            Point scaled = default(Point);
            scaled.X = (ImgOriginalSize[0] * picbox.PointToClient(Mouse).X / picbox.Width);
            scaled.Y = (ImgOriginalSize[1] * picbox.PointToClient(Mouse).Y / picbox.Height);

            // scale mouse position to unscaled axis and calculate selected trace
            double unscTrace = MinX + (scaled.X * (MaxX - MinX) / ImgOriginalSize[0]);
            int unscFloor = (int)Math.Floor(unscTrace);
            int unscCeiln = 0;
            if (Math.Ceiling(unscTrace) == unscFloor)
                unscCeiln = unscFloor + 1;
            else
                unscCeiln = (int) Math.Ceiling(unscTrace);

            // translate selected trace back to its pixel position
            double pixlFloor = (unscFloor - MinX) * ImgOriginalSize[0] / (MaxX - MinX);
            double pixlCeiln = (unscCeiln - MinX) * ImgOriginalSize[0] / (MaxX - MinX);
            double pixpTrace = pixlCeiln - pixlFloor;

            // scale pixel position to current image size
            double scalFloor = pixlFloor * picbox.Width / ImgOriginalSize[0];
            double scalCeiln = pixlCeiln * picbox.Width / ImgOriginalSize[0];
            double scalPxTrc = scalCeiln - scalFloor;
            if (scalPxTrc < 1)
                scalPxTrc = 1;

            var _with1 = PicHoverX;
            _with1.Width = (int)Math.Round(scalPxTrc);
            _with1.Left = (int)Math.Round(scalFloor) + 1 + panelY.Width + picbox.Left;
            _with1.Invalidate();
            _with1.Update();

            var _with2 = LblHoverX;
            _with2.Text = unscFloor.ToString();
            _with2.Left = (PicHoverX.Left + PicHoverX.Width / 2) - (_with2.Width / 2);
            _with2.Invalidate();
            _with2.Update();

            var _with3 = PicHoverY;
            _with3.Top = picbox.PointToClient(Mouse).Y + panelGap.Height + picbox.Top;
            _with3.Invalidate();

            var _with4 = LblHoverY;
            _with4.Text = Math.Round(MinY + (scaled.Y * ((MaxY - MinY) / ImgOriginalSize[1])), 3).ToString();
            _with4.Top = (int) Math.Round((double) (picbox.PointToClient(Mouse).Y + panelGap.Height + picbox.Top - _with4.Height / 2));
            _with4.Left = panelY.Width - _with4.Width - 10;
            _with4.Invalidate();

        }

        public static void Image_Pan(Panel panelImage, Point Mouse)
        {
            Point panEndMouse = panelImage.PointToClient(Mouse);
            Point panShiftMouse = panEndMouse - (Size) PanStartMouse;

            int panEndHScroll = PanStartHScroll - panShiftMouse.X;
            if (panEndHScroll <= 0)
                panEndHScroll = 0;
            if (panEndHScroll >= panelImage.HorizontalScroll.Maximum)
                panEndHScroll = panelImage.HorizontalScroll.Maximum;

            int panEndVScroll = PanStartVScroll - panShiftMouse.Y;
            if (panEndVScroll <= 0)
                panEndVScroll = 0;
            if (panEndVScroll >= panelImage.VerticalScroll.Maximum)
                panEndVScroll = panelImage.VerticalScroll.Maximum;

            if (panelImage.HorizontalScroll.Visible == true)
                panelImage.HorizontalScroll.Value = panEndHScroll;
            if (panelImage.VerticalScroll.Visible == true)
                panelImage.VerticalScroll.Value = panEndVScroll;

            panelImage.Invalidate();
            panelImage.Update();
        }

        public static void Image_MouseCenteredZoom(Point Mouse, PictureBox picbox, Panel panelImage)
        {
            // save initial parameter for mouse centered zoom
            Point picStart = (Mouse);
            Point picSizeStart = new Point(picbox.Width, picbox.Height);

            // zoom image
            Point picNewSize = new Point((int) (ImgOriginalSize[0] * (ZoomFactor / 100)), (int) (ImgOriginalSize[1] * (ZoomFactor / 100)));
            var _with1 = picbox;
            _with1.SizeMode = PictureBoxSizeMode.StretchImage;
            _with1.Width = picNewSize.X;
            _with1.Height = picNewSize.Y;

            panelImage.Invalidate();

            // mouse centered zoom
            Point picPosShift = new Point(picStart.X - (picStart.X / picSizeStart.X) * picNewSize.X, picStart.Y - (picStart.Y / picSizeStart.Y) * picNewSize.Y);
            Point picPosEnd = new Point(picbox.Left + picPosShift.X, picbox.Top + picPosShift.Y);

            int panEndHScroll = -picPosEnd.X;
            if (panEndHScroll <= 0)
                panEndHScroll = 0;
            if (panEndHScroll >= panelImage.HorizontalScroll.Maximum)
                panEndHScroll = panelImage.HorizontalScroll.Maximum;

            int panEndVScroll = -picPosEnd.Y;
            if (panEndVScroll <= 0)
                panEndVScroll = 0;
            if (panEndVScroll >= panelImage.VerticalScroll.Maximum)
                panEndVScroll = panelImage.VerticalScroll.Maximum;

            panelImage.HorizontalScroll.Value = panEndHScroll;
            panelImage.VerticalScroll.Value = panEndVScroll;

            panelImage.Update();

        }

        public static void Image_Axis_StretchShrink(PictureBox picbox, Panel panelX, Panel panelY, Panel panelImage, Point Mouse)
        {
            // for X-axis stretch/shrink
            if (FScaleX == true)
            {
                Point scaleMouse1 = panelX.PointToClient(Mouse);

                // translate mouse shift to the amount of size change
                double scl0 = (scaleMouse1.X - ScaleMouse0.X) * 3;
                ImgOriginalSize[0] = (int) (ImgOriginalSize[0] + scl0);

                // resize in X-direction
                picbox.Width = ImgOriginalSize[0];
                picbox.Update();

                ScaleMouse0 = scaleMouse1;
            }

            // for Y-axis stretch/shrink
            if (FScaleY == true)
            {
                Point scaleMouse1 = panelY.PointToClient(Mouse);

                // translate mouse shift to the amount of size change
                double scl1 = (scaleMouse1.Y - ScaleMouse0.Y) * 3;
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
