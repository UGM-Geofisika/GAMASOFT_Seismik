using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using Unplugged.Segy;

namespace SegyView
{
    public static class GamaSeismicColor
    {
        // obtain amplitude range of seismic data
        //public static double MinA;
        //public static double MaxA;

        //public static int[,] cScale = new int[256, 3] -> declared inside ImageWriter;
        public static Bitmap bmpScale = new Bitmap(256, 1);

        public static PictureBox _picbox;
        public static PictureBox _picCscale;

        public static void Setup(PictureBox picbox, PictureBox picColorScale)
        {
            _picbox = picbox;
            _picCscale = picColorScale;
        }

        public static void SetColormap_3Color(Color bottomVal, Color middleVal, Color upperVal)
        {
            // linear interpolation for each component color in RGB system
           
            // set input color
            ImageWriter.cScale[0, 0] = bottomVal.R; ImageWriter.cScale[0, 1] = bottomVal.G; ImageWriter.cScale[0, 2] = bottomVal.B;
            ImageWriter.cScale[127, 0] = middleVal.R; ImageWriter.cScale[127, 1] = middleVal.G; ImageWriter.cScale[127, 2] = middleVal.B;
            ImageWriter.cScale[255, 0] = upperVal.R; ImageWriter.cScale[255, 1] = upperVal.G; ImageWriter.cScale[255, 2] = upperVal.B;

            // for negative amplitude part
            double dR = ((double)middleVal.R - (double)bottomVal.R) / 127.0;
            double dG = ((double)middleVal.G - (double)bottomVal.G) / 127.0;
            double dB = ((double)middleVal.B - (double)bottomVal.B) / 127.0;

            for (int i=1;i<127;i++)
            {
                ImageWriter.cScale[i, 0] = (byte)(ImageWriter.cScale[i - 1, 0] + Math.Round(dR));
                ImageWriter.cScale[i, 1] = (byte)(ImageWriter.cScale[i - 1, 1] + Math.Round(dG));
                ImageWriter.cScale[i, 2] = (byte)(ImageWriter.cScale[i - 1, 2] + Math.Round(dB));
            }

            // for positive amplitude part
            dR = ((double)upperVal.R - (double)middleVal.R) / 127.0;
            dG = ((double)upperVal.G - (double)middleVal.G) / 127.0;
            dB = ((double)upperVal.B - (double)middleVal.B) / 127.0;

            for (int i=128;i<255;i++)
            {
                ImageWriter.cScale[i, 0] = (byte)(ImageWriter.cScale[i - 1, 0] + Math.Round(dR));
                ImageWriter.cScale[i, 1] = (byte)(ImageWriter.cScale[i - 1, 1] + Math.Round(dG));
                ImageWriter.cScale[i, 2] = (byte)(ImageWriter.cScale[i - 1, 2] + Math.Round(dB));
            }   
        }

        public static void UpdateColormapBitmap()
        {
            // create bitmap color scale
            for (int i = 0; i < 256; i++)
            {
                bmpScale.SetPixel(i, 0, Color.FromArgb(ImageWriter.cScale[i, 0], ImageWriter.cScale[i, 1], ImageWriter.cScale[i, 2]));
            }
            
            _picCscale.BackgroundImage = bmpScale;
            _picCscale.Update();
        }


    }
}
