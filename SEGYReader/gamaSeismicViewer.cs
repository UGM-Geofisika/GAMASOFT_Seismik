using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms; 
using System.Drawing;

namespace SegyView
{
    public class GamaSeismicViewer
    {
        public static int[] ImgOriginalSize = new int[3];
        public static double ZoomFactor = 100;
        
        public List<Label> ListLabelX, ListLabelY = new List<Label>();
        public List<PictureBox> ListTickV, ListTickH = new List<PictureBox>();
        public PictureBox PicMaxX, PicMaxY, PicHoverX, PicHoverY = new PictureBox();
        public Label LblHoverX, LblHoverY = new Label();

        public static int dx = 1;
        public static double dy = 0.033;
        public static int MinX = 15;
        public static int MaxX = 1000 + dx;
        public static double MinY = 0;
	    public static double MaxY = 5;

        public int DLabelX = 50;
        public int DLabelY = 50;

        // public variable for panning
        public bool FPan = false;
        public Point PanStartMouse;
        public int PanStartHScroll;
        public int PanStartVScroll;

        //public variable for axis scaling
        public bool FScaleX = false;
        public bool FScaleY = false;
        public Point ScaleMouse0;

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
        
        	//Image_Axis_Initialize(picBox, Panel4, Panel2, Panel5, Panel7, listLabelX, listLabelY, listTickV, listTickH, minX,
        	//maxX, minY, maxY);
        }
    }
}
