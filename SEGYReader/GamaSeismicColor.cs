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

        // Colormap Presets
        public static void Colormap_BlackWhiteRed()
        {
            CreateColormap(ImageWriter.cScale, Color.FromArgb(0, 0, 0), 0, Color.FromArgb(255, 255, 255), 127, Color.FromArgb(255, 0, 0), 255);

            GamaColormapEditor.LoadColormapTable(new Color[3] { Color.FromArgb(0, 0, 0), Color.FromArgb(255, 255, 255), Color.FromArgb(255, 0, 0) },
                                                 new int[3] { 0, 127, 255 });
        }

        public static void Colormap_BlueWhiteRed()
        {
            CreateColormap(ImageWriter.cScale, Color.FromArgb(0, 0, 255), 0, Color.FromArgb(255, 255, 255), 127, Color.FromArgb(255, 0, 0), 255);

            GamaColormapEditor.LoadColormapTable(new Color[3] { Color.FromArgb(0, 0, 255), Color.FromArgb(255, 255, 255), Color.FromArgb(255, 0, 0) },
                                                 new int[3] { 0, 127, 255 });
        }

        public static void Colormap_Grayscale()
        {
            CreateColormap(ImageWriter.cScale, Color.FromArgb(0, 0, 0), 0, Color.FromArgb(255, 255, 255), 255);

            GamaColormapEditor.LoadColormapTable(new Color[2] {Color.FromArgb(0, 0, 0), Color.FromArgb(255, 255, 255)},
                                                 new int[2] {0, 255});
        }

        public static void Colormap_Rainbow()
        {
            CreateColormap(ImageWriter.cScale, 
                new Color[6] {Color.Red, Color.DarkOrange, Color.Yellow, Color.Lime, Color.Blue, Color.DarkViolet}, 
                new int[6]   {0,         51,               102,          153,        204,        255             });

            GamaColormapEditor.LoadColormapTable(new Color[6] { Color.Red, Color.DarkOrange, Color.Yellow, Color.Lime, Color.Blue, Color.DarkViolet },
                                                 new int[6] { 0, 51, 102, 153, 204, 255 });
        }

        // linear color interpolation method overloads
        public static void CreateColormap(byte[,] cScale, Color bottomVal, int bottomByte, 
                                                 Color middleVal, int middleByte,
                                                 Color upperVal, int upperByte)
        {
            // linear interpolation for each component color in RGB system
            
            // set input color
            // bottom limit
            cScale[bottomByte, 0] = bottomVal.R; 
            cScale[bottomByte, 1] = bottomVal.G; 
            cScale[bottomByte, 2] = bottomVal.B;
            // middle color
            cScale[middleByte, 0] = middleVal.R;
            cScale[middleByte, 1] = middleVal.G;
            cScale[middleByte, 2] = middleVal.B;
            // upper limit
            cScale[upperByte, 0] = upperVal.R;
            cScale[upperByte, 1] = upperVal.G;
            cScale[upperByte, 2] = upperVal.B;

            // if bottom limit is > 0, assign bottom color to all index below it
            if (bottomByte > 0)
            {
                for (int i = 0; i < bottomByte; i++)
                {
                    cScale[i, 0] = bottomVal.R;
                    cScale[i, 1] = bottomVal.G;
                    cScale[i, 2] = bottomVal.B;
                }
            }

            // calculate color for the lower part of the colormap
            double dR = ((double)middleVal.R - (double)bottomVal.R) / ((double)middleByte - (double)bottomByte);
            double dG = ((double)middleVal.G - (double)bottomVal.G) / ((double)middleByte - (double)bottomByte);
            double dB = ((double)middleVal.B - (double)bottomVal.B) / ((double)middleByte - (double)bottomByte);

            for (int i = bottomByte + 1; i < middleByte; i++)
            {
                cScale[i, 0] = (byte)(cScale[i - 1, 0] + Math.Round(dR));
                cScale[i, 1] = (byte)(cScale[i - 1, 1] + Math.Round(dG));
                cScale[i, 2] = (byte)(cScale[i - 1, 2] + Math.Round(dB));
            }

            // calculate color for the upper part of the colormap
            dR = ((double)upperVal.R - (double)middleVal.R) / ((double)upperByte - (double)middleByte);
            dG = ((double)upperVal.G - (double)middleVal.G) / ((double)upperByte - (double)middleByte);
            dB = ((double)upperVal.B - (double)middleVal.B) / ((double)upperByte - (double)middleByte);

            for (int i = middleByte + 1; i < upperByte; i++)
            {
                cScale[i, 0] = (byte)(cScale[i - 1, 0] + Math.Round(dR));
                cScale[i, 1] = (byte)(cScale[i - 1, 1] + Math.Round(dG));
                cScale[i, 2] = (byte)(cScale[i - 1, 2] + Math.Round(dB));
            }

            // if upper limit is < 255, assign upper color to all index after it
            if (upperByte < 255)
            {
                for (int i = upperByte + 1; i <= 255; i++)
                {
                    cScale[i, 0] = upperVal.R;
                    cScale[i, 1] = upperVal.G;
                    cScale[i, 2] = upperVal.B;
                }
            }
        }

        public static void CreateColormap(byte[,] cScale, Color bottomVal, int bottomByte, 
                                                 Color upperVal, int upperByte)
        {
            // linear interpolation for each component color in RGB system

            // set input color
            // bottom limit
            cScale[bottomByte, 0] = bottomVal.R;
            cScale[bottomByte, 1] = bottomVal.G;
            cScale[bottomByte, 2] = bottomVal.B;
            // upper limit
            cScale[upperByte, 0] = upperVal.R;
            cScale[upperByte, 1] = upperVal.G;
            cScale[upperByte, 2] = upperVal.B;

            // if bottom limit is > 0, assign bottom color to all index below it
            if (bottomByte > 0)
            {
                for (int i = 0; i < bottomByte; i++)
                {
                    cScale[i, 0] = bottomVal.R;
                    cScale[i, 1] = bottomVal.G;
                    cScale[i, 2] = bottomVal.B;
                }
            }

            // interpolate color of the colormap
            double dR = ((double)upperVal.R - (double)bottomVal.R) / ((double)upperByte - (double)bottomByte);
            double dG = ((double)upperVal.G - (double)bottomVal.G) / ((double)upperByte - (double)bottomByte);
            double dB = ((double)upperVal.B - (double)bottomVal.B) / ((double)upperByte - (double)bottomByte);

            for (int i = bottomByte + 1; i < upperByte; i++)
            {
                cScale[i, 0] = (byte)(cScale[i - 1, 0] + Math.Round(dR));
                cScale[i, 1] = (byte)(cScale[i - 1, 1] + Math.Round(dG));
                cScale[i, 2] = (byte)(cScale[i - 1, 2] + Math.Round(dB));
            }

            // if upper limit is < 255, assign upper color to all index after it
            if (upperByte < 255)
            {
                for (int i = upperByte + 1; i <= 255; i++)
                {
                    cScale[i, 0] = upperVal.R;
                    cScale[i, 1] = upperVal.G;
                    cScale[i, 2] = upperVal.B;
                }
            }
        }

        public static void CreateColormap(byte[,] cScale, Color[] colorVal, int[] colorByte)
        {
            // linear interpolation for each component color in RGB system

            // set input color
            for (int i = 0; i < colorVal.Length; i++)
            {
                cScale[colorByte[i], 0] = colorVal[i].R;
                cScale[colorByte[i], 1] = colorVal[i].G;
                cScale[colorByte[i], 2] = colorVal[i].B;
            }

            // if bottom limit is > 0, assign bottom color to all index below it
            if (colorByte[0] > 0)
            {
                for (int i = 0; i < colorByte[0]; i++)
                {
                    cScale[i, 0] = colorVal[0].R;
                    cScale[i, 1] = colorVal[0].G;
                    cScale[i, 2] = colorVal[0].B;
                }
            }

            // interpolate color of the colormap
            for (int i = 0; i < colorVal.Length - 1; i++)
            {
                double dR = ((double)(colorVal[i+1].R - colorVal[i].R)) / ((double)(colorByte[i+1] - colorByte[i] - 1));
                double dG = ((double)(colorVal[i+1].G - colorVal[i].G)) / ((double)(colorByte[i+1] - colorByte[i] - 1));
                double dB = ((double)(colorVal[i+1].B - colorVal[i].B)) / ((double)(colorByte[i+1] - colorByte[i] - 1));
            
                for (int j = colorByte[i]; j < colorByte[i+1]; j++)
                {
                    cScale[j+1, 0] = (byte)((double)cScale[j, 0] + Math.Round(dR));
                    cScale[j+1, 1] = (byte)((double)cScale[j, 1] + Math.Round(dG));
                    cScale[j+1, 2] = (byte)((double)cScale[j, 2] + Math.Round(dB));
                }
            }

            // if upper limit is < 255, assign upper color to all index after it
            if (colorByte[colorVal.Length-1] < 255)
            {
                for (int i = colorByte[colorVal.Length - 1] + 1; i <= 255; i++)
                {
                    cScale[i, 0] = colorVal[colorVal.Length - 1].R;
                    cScale[i, 1] = colorVal[colorVal.Length - 1].G;
                    cScale[i, 2] = colorVal[colorVal.Length - 1].B;
                }
            }

        }

        // update colormap everloads
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

        public static void UpdateColormapBitmap(byte[,] cScale, PictureBox picColormap)
        {
            // create bitmap color scale
            for (int i = 0; i < 256; i++)
            {
                bmpScale.SetPixel(i, 0, Color.FromArgb(cScale[i, 0], cScale[i, 1], cScale[i, 2]));
            }

            picColormap.BackgroundImage = bmpScale;
            picColormap.Update();
        }


    }
}
