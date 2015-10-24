using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Unplugged.Segy;

namespace SegyView
{
    public static class GamaSeismicColor
    {
        // obtain amplitude range of seismic data
        //public static double MinA;
        //public static double MaxA;

        //public static int[,] cScale = new int[256, 3] -> declared inside ImageWriter;

        public static PictureBox Picbox;
        public static PictureBox PicCscale;

        public static void Setup(PictureBox picbox, PictureBox picColorScale)
        {
            Picbox = picbox;
            PicCscale = picColorScale;
        }

        // Colormap Presets
        public static void Colormap_BlackWhiteRed()
        {
            var newColor = new Color[3]
            {Color.FromArgb(0, 0, 0), Color.FromArgb(255, 255, 255), Color.FromArgb(255, 0, 0)};
            var newIndex = new int[3] {0, 127, 255};

            CreateColormap(ImageWriter.cScale, newColor, newIndex);
            GamaColormapEditor.LoadColormapTable(newColor, newIndex);
        }

        public static void Colormap_BlueWhiteRed()
        {
            var newColor = new Color[3]
            {Color.FromArgb(0, 0, 255), Color.FromArgb(255, 255, 255), Color.FromArgb(255, 0, 0)};
            var newIndex = new int[3] {0, 127, 255};

            CreateColormap(ImageWriter.cScale, newColor, newIndex);
            GamaColormapEditor.LoadColormapTable(newColor, newIndex);
        }

        public static void Colormap_Grayscale()
        {
            var newColor = new Color[2] {Color.FromArgb(0, 0, 0), Color.FromArgb(255, 255, 255)};
            var newIndex = new int[2] {0, 255};

            CreateColormap(ImageWriter.cScale, newColor, newIndex);
            GamaColormapEditor.LoadColormapTable(newColor, newIndex);
        }

        public static void Colormap_Rainbow()
        {
            var newColor = new Color[6]
            {Color.Red, Color.DarkOrange, Color.Yellow, Color.Lime, Color.Blue, Color.DarkViolet};
            var newIndex = new int[6] {0, 51, 102, 153, 204, 255};

            CreateColormap(ImageWriter.cScale, newColor, newIndex);
            GamaColormapEditor.LoadColormapTable(newColor, newIndex);
        }

        // linear color interpolation method
        public static void CreateColormap(byte[,] colorScale, Color[] colorValue, int[] colorIndex)
        {
            // linear interpolation for each component color in RGB system

            // set input color
            for (var i = 0; i < colorValue.Length; i++)
            {
                colorScale[colorIndex[i], 0] = colorValue[i].R;
                colorScale[colorIndex[i], 1] = colorValue[i].G;
                colorScale[colorIndex[i], 2] = colorValue[i].B;
            }

            // if bottom limit is > 0, assign bottom color to all index below it
            if (colorIndex[0] > 0)
            {
                for (var i = 0; i < colorIndex[0]; i++)
                {
                    colorScale[i, 0] = colorValue[0].R;
                    colorScale[i, 1] = colorValue[0].G;
                    colorScale[i, 2] = colorValue[0].B;
                }
            }

            // interpolate color of the colormap
            var newColormap = new Bitmap(256, 1);
            var g = Graphics.FromImage(newColormap);
            for (var i = 0; i < colorValue.Length - 1; i++)
            {
                var lgb = new LinearGradientBrush(new Point(colorIndex[i], 0),
                    new Point(colorIndex[i + 1], 0),
                    colorValue[i], colorValue[i + 1]);
                g.DrawLine(new Pen(lgb), new Point(colorIndex[i], 0), new Point(colorIndex[i + 1], 0));

                for (var j = colorIndex[i] + 1; j < colorIndex[i + 1]; j++)
                {
                    colorScale[j, 0] = newColormap.GetPixel(j, 0).R;
                    colorScale[j, 1] = newColormap.GetPixel(j, 0).G;
                    colorScale[j, 2] = newColormap.GetPixel(j, 0).B;
                }
            }

            // if upper limit is < 255, assign upper color to all index after it
            if (colorIndex[colorValue.Length - 1] < 255)
            {
                for (var i = colorIndex[colorValue.Length - 1] + 1; i <= 255; i++)
                {
                    colorScale[i, 0] = colorValue[colorValue.Length - 1].R;
                    colorScale[i, 1] = colorValue[colorValue.Length - 1].G;
                    colorScale[i, 2] = colorValue[colorValue.Length - 1].B;
                }
            }

            newColormap.Dispose();
            g.Dispose();
        }

        // update colormap everloads
        public static void UpdateColormapBitmap()
        {
            var bmpScale = new Bitmap(256, 1);

            // create bitmap color scale
            for (var i = 0; i < 256; i++)
            {
                bmpScale.SetPixel(i, 0,
                    Color.FromArgb(ImageWriter.cScale[i, 0], ImageWriter.cScale[i, 1], ImageWriter.cScale[i, 2]));
            }

            PicCscale.BackgroundImage = bmpScale;
            PicCscale.Update();

            GamaColormapEditor.TempCMap = ImageWriter.cScale;
        }
    }
}