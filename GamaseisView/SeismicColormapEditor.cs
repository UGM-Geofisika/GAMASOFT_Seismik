using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Unplugged.Segy;

namespace Gamaseis
{
    public static class SeismicColormapEditor
    {
        // list for color values
        private static List<Panel> _editCScale = new List<Panel>();
        public static List<ColorMarker> ButCScale = new List<ColorMarker>();
        private static readonly List<PictureBox> PicCScale = new List<PictureBox>();
        public static List<Panel> PanboxPicker = new List<Panel>();
        public static byte[,] EditCMap = new byte[256, 3];
        public static byte[,] TempCMap = new byte[256, 3];
        public static Bitmap TmpEdit = new Bitmap(256, 1);
        public static Bitmap BmpEdit = new Bitmap(512, 1);
        public static int IdxSelMarker;
        public static bool FMarkerSelected;
        public static Point SelectMarkerOffset;
        public static PictureBox PicCMap;
        public static DataGridView DgvCMap;
        public static Form FrmCMap;
        public static Panel PanPickerHue;
        public static PictureBox PanPickerSv;
        public static DataGridView DgvColorPick;
        public static PictureBox PicPickerHueLine;
        public static ToolStripComboBox ComboPreset;
        public static PictureBox PicCurClr;
        public static PictureBox PicNewClr;
        public static Panel PanMarkerIsSelected;

        public static void Setup(Form frmCMap, PictureBox picColorScale, DataGridView dgvCMap, Panel panPickerHue,
            PictureBox panPickerSv, PictureBox picPickerHueLine, ToolStripComboBox comboPreset,
            DataGridView dgvColorPick, PictureBox picCurClr, PictureBox picNewClr,
            Panel panMarkerIsSelected)
        {
            FrmCMap = frmCMap;
            PicCMap = picColorScale;
            DgvCMap = dgvCMap;
            PanPickerHue = panPickerHue;
            PanPickerSv = panPickerSv;
            PicPickerHueLine = picPickerHueLine;
            ComboPreset = comboPreset;
            DgvColorPick = dgvColorPick;
            PicCurClr = picCurClr;
            PicNewClr = picNewClr;
            PanMarkerIsSelected = panMarkerIsSelected;
        }

        public static void InitializeColormapEditor()
        {
            var clr = new Color();
            // load current colormap   
            for (var i = 0; i < 256; i++)
            {
                clr = Color.FromArgb(ImageWriter.cScale[i, 0], ImageWriter.cScale[i, 1], ImageWriter.cScale[i, 2]);
                BmpEdit.SetPixel(i*2, 0, clr);
                BmpEdit.SetPixel((i*2) + 1, 0, clr);
                PicCMap.BackgroundImage = BmpEdit;
            }

            // create images for color picker
            var cpHue = new Bitmap(1, 256);
            for (var i = 0; i < 256; i++)
            {
                var cRgb = new ColorRgb();
                cRgb = Hsl2Rgb((((double) i)/256), 1, 0.5);
                cpHue.SetPixel(0, i, Color.FromArgb(cRgb.R, cRgb.G, cRgb.B));
            }

            PanPickerHue.BackgroundImage = cpHue;
            LoadSaturationValueFromHue(Color.Red);

            // create picker box
            for (var i = 0; i < 4; i++) PanboxPicker.Add(new Panel());
            for (var i = 0; i < 4; i++)
            {
                PanboxPicker[i].Parent = PanPickerSv;
                PanboxPicker[i].BackColor = Color.Black;
                PanboxPicker[i].Show();
            }

            PanboxPicker[0].Width = 1;
            PanboxPicker[0].Height = 7;
            PanboxPicker[1].Width = 7;
            PanboxPicker[1].Height = 1;
            PanboxPicker[2].Width = 1;
            PanboxPicker[2].Height = 7;
            PanboxPicker[3].Width = 7;
            PanboxPicker[3].Height = 1;

            UpdateSelectedColor(PanPickerSv.PointToScreen(new Point(255, 0)));
        }

        public static void PickerBox_Move(Point mouse)
        {
            var pointer = mouse;
            if (pointer.X < 0) pointer.X = 0;
            if (pointer.Y < 0) pointer.Y = 0;
            if (pointer.X > 255) pointer.X = 255;
            if (pointer.Y > 255) pointer.Y = 255;

            /* left   */
            PanboxPicker[0].Left = pointer.X - 3;
            PanboxPicker[0].Top = pointer.Y - 3;
            /* top    */
            PanboxPicker[1].Left = pointer.X - 3;
            PanboxPicker[1].Top = pointer.Y - 3;
            /* right  */
            PanboxPicker[2].Left = pointer.X + 3;
            PanboxPicker[2].Top = pointer.Y - 3;
            /* bottom */
            PanboxPicker[3].Left = pointer.X - 3;
            PanboxPicker[3].Top = pointer.Y + 3;

            for (var i = 0; i < 4; i++)
            {
                if (((Bitmap) PanPickerSv.Image).GetPixel(pointer.X, pointer.Y).GetBrightness()*100 > 30)
                {
                    PanboxPicker[i].BackColor = Color.Black;
                }
                else
                {
                    PanboxPicker[i].BackColor = Color.White;
                }

                PanboxPicker[i].Update();
            }
        }

        public static void UpdateBitmapColormap(byte[,] cScale)
        {
            BmpEdit = new Bitmap(512, 1);
            for (var i = 0; i < 256; i++)
            {
                var clr = Color.FromArgb(cScale[i, 0], cScale[i, 1], cScale[i, 2]);
                BmpEdit.SetPixel(i*2, 0, clr);
                BmpEdit.SetPixel((i*2) + 1, 0, clr);
                PicCMap.BackgroundImage = BmpEdit;
            }

            TempCMap = cScale;
        }

        public static void LoadColormapTable(Color[] colorVal, int[] colorByte)
        {
            // clear existing color marker and table
            foreach (Panel but in ButCScale)
            {
                but.Hide();
            }
            foreach (var pic in PicCScale)
            {
                pic.Hide();
            }

            // add color marker and table
            DgvCMap.Rows.Clear();
            ButCScale.Clear();
            PicCScale.Clear();

            for (var i = 0; i < colorVal.Length; i++)
            {
                DgvCMap.Rows.Add();
                ButCScale.Add(new ColorMarker());
                PicCScale.Add(new PictureBox());
            }

            for (var i = 0; i < colorVal.Length; i++)
            {
                // for color table
                DgvCMap[0, i].Value = (i + 1).ToString();
                DgvCMap[1, i].Value = colorByte[i].ToString();

                UpdateColorTableData(colorVal[i], i);

                // for color marker button
                ButCScale[i].ID = i;
                ButCScale[i].Parent = (PicCMap.Parent).Parent;
                ButCScale[i].Width = 10;
                ButCScale[i].Height = 10;
                ButCScale[i].BorderStyle = BorderStyle.FixedSingle;
                ButCScale[i].BackColor = colorVal[i];
                ButCScale[i].Left = (colorByte[i]*2) + PicCMap.Parent.Left + PicCMap.Parent.Padding.Left - 4;
                ButCScale[i].Top = PicCMap.Parent.Top + PicCMap.Parent.Height + 3;

                // adding event handler (this takes time)
                ButCScale[i].MouseDown += ColorMarker_MouseDown;
                ButCScale[i].MouseMove += ColorMarker_MouseMove;
                ButCScale[i].MouseUp += ColorMarker_MouseUp;
                ButCScale[i].DoubleClick += ColorMarker_DoubleClick;

                // for marker line
                PicCScale[i].Parent = (PicCMap.Parent).Parent;
                PicCScale[i].Width = 2;
                PicCScale[i].Height = PicCMap.Parent.Height + 10;
                PicCScale[i].BackColor = Color.Black;
                PicCScale[i].Left = (colorByte[i]*2) + PicCMap.Parent.Left + PicCMap.Parent.Padding.Left;
                PicCScale[i].Top = PicCMap.Parent.Top - 5;

                PicCScale[i].BringToFront();
                PicCScale[i].Show();
                ButCScale[i].BringToFront();
                ButCScale[i].Show();
            }

            PicCMap.Parent.BringToFront();
            DgvColorPick.Parent.Hide();

            // for current color, load first color or selected color
            if (IdxSelMarker < ButCScale.Count)
            {
                UpdateCurrentColor(colorVal[IdxSelMarker]);
                PanMarkerIsSelected.Left = ButCScale[IdxSelMarker].Left;
            }
            else
            {
                UpdateCurrentColor(colorVal[0]);
                PanMarkerIsSelected.Left = ButCScale[0].Left;
            }
        }

        public static void AddColorMarker(Color newColor, int newIndex)
        {
            // abort if newIndex already exist
            for (var i = 0; i < DgvCMap.Rows.Count; i++)
            {
                if (Convert.ToInt32(DgvCMap[1, i].Value) == newIndex) return;
            }

            // define new Color[] and int[]
            var tempColor = new Color[DgvCMap.Rows.Count + 1];
            var tempIndex = new int[DgvCMap.Rows.Count + 1];
            var newColorIdx = 0;

            // check if newIndex is the leftmost marker
            if (newIndex < Convert.ToInt32(DgvCMap[1, 0].Value))
            {
                newColorIdx = 0;
            }

            // check if newIndex is the leftmost marker
            if (newIndex > Convert.ToInt32(DgvCMap[1, DgvCMap.Rows.Count - 1].Value))
            {
                newColorIdx = DgvCMap.Rows.Count;
            }

            // check if newIndex is between any two markers
            if (newIndex > Convert.ToInt32(DgvCMap[1, 0].Value) &
                newIndex < Convert.ToInt32(DgvCMap[1, DgvCMap.Rows.Count - 1].Value))
            {
                for (var i = 0; i < DgvCMap.Rows.Count - 1; i++)
                {
                    if (newIndex > Convert.ToInt32(DgvCMap[1, i].Value) &
                        newIndex < Convert.ToInt32(DgvCMap[1, i + 1].Value))
                    {
                        newColorIdx = i + 1;
                    }
                }
            }

            // create new color and index array
            if (newColorIdx == 0)
            {
                tempColor[0] = newColor;
                tempIndex[0] = newIndex;

                for (var i = 1; i < tempColor.Length; i++)
                {
                    tempColor[i] = DgvCMap[2, i - 1].Style.BackColor;
                    tempIndex[i] = Convert.ToInt32(DgvCMap[1, i - 1].Value);
                }
            }

            if (newColorIdx == tempColor.Length - 1)
            {
                for (var i = 0; i < tempColor.Length - 1; i++)
                {
                    tempColor[i] = DgvCMap[2, i].Style.BackColor;
                    tempIndex[i] = Convert.ToInt32(DgvCMap[1, i].Value);
                }

                tempColor[tempColor.Length - 1] = newColor;
                tempIndex[tempColor.Length - 1] = newIndex;
            }

            if (newColorIdx != 0 & newColorIdx != tempColor.Length - 1)
            {
                for (var i = 0; i < newColorIdx; i++)
                {
                    tempColor[i] = DgvCMap[2, i].Style.BackColor;
                    tempIndex[i] = Convert.ToInt32(DgvCMap[1, i].Value);
                }

                tempColor[newColorIdx] = newColor;
                tempIndex[newColorIdx] = newIndex;

                for (var i = newColorIdx + 1; i < tempColor.Length; i++)
                {
                    tempColor[i] = DgvCMap[2, i - 1].Style.BackColor;
                    tempIndex[i] = Convert.ToInt32(DgvCMap[1, i - 1].Value);
                }
            }

            SeismicColorMap.CreateColormap(EditCMap, tempColor, tempIndex);
            LoadColormapTable(tempColor, tempIndex);
            UpdateBitmapColormap(EditCMap);
            DgvColorPick.Parent.Hide();
        }

        public static void DeleteColorMarker(int markerIndex)
        {
            if (ButCScale.Count <= 2) return;

            // define new Color[] and int[]
            var tempColor = new Color[DgvCMap.Rows.Count - 1];
            var tempIndex = new int[DgvCMap.Rows.Count - 1];

            // if marker to be deleted is the leftmost marker
            if (markerIndex == 0)
            {
                for (var i = 1; i < DgvCMap.Rows.Count; i++)
                {
                    tempColor[i - 1] = DgvCMap[2, i].Style.BackColor;
                    tempIndex[i - 1] = Convert.ToInt32(DgvCMap[1, i].Value);
                }
            }

            // if marker to be deleted is the rightmost marker
            if (markerIndex == DgvCMap.Rows.Count - 1)
            {
                for (var i = 0; i < DgvCMap.Rows.Count - 1; i++)
                {
                    tempColor[i] = DgvCMap[2, i].Style.BackColor;
                    tempIndex[i] = Convert.ToInt32(DgvCMap[1, i].Value);
                }
            }

            // if marker to be deleted is any marker in the middle
            if (markerIndex != 0 & markerIndex != DgvCMap.Rows.Count - 1)
            {
                for (var i = 0; i < markerIndex; i++)
                {
                    tempColor[i] = DgvCMap[2, i].Style.BackColor;
                    tempIndex[i] = Convert.ToInt32(DgvCMap[1, i].Value);
                }

                for (var i = markerIndex + 1; i < DgvCMap.Rows.Count; i++)
                {
                    tempColor[i - 1] = DgvCMap[2, i].Style.BackColor;
                    tempIndex[i - 1] = Convert.ToInt32(DgvCMap[1, i].Value);
                }
            }

            SeismicColorMap.CreateColormap(EditCMap, tempColor, tempIndex);
            LoadColormapTable(tempColor, tempIndex);
            UpdateBitmapColormap(EditCMap);
            DgvColorPick.Parent.Hide();
        }

        public static ColorRgb Hsl2Rgb(double h, double sl, double l)
        {
            double v;
            double r, g, b;

            r = l;
            g = l;
            b = l;

            v = (l <= 0.5) ? (l*(1.0 + sl)) : (l + sl - l*sl);
            if (v > 0)
            {
                double m;
                double sv;
                int sextant;
                double fract, vsf, mid1, mid2;

                m = l + l - v;
                sv = (v - m)/v;
                h *= 6.0;
                sextant = (int) h;
                fract = h - sextant;
                vsf = v*sv*fract;
                mid1 = m + vsf;
                mid2 = v - vsf;
                switch (sextant)
                {
                    case 0:
                        r = v;
                        g = mid1;
                        b = m;
                        break;
                    case 1:
                        r = mid2;
                        g = v;
                        b = m;
                        break;
                    case 2:
                        r = m;
                        g = v;
                        b = mid1;
                        break;
                    case 3:
                        r = m;
                        g = mid2;
                        b = v;
                        break;
                    case 4:
                        r = mid1;
                        g = m;
                        b = v;
                        break;
                    case 5:
                        r = v;
                        g = m;
                        b = mid2;
                        break;
                }
            }
            ColorRgb rgb;
            rgb.R = Convert.ToByte(r*255.0f);
            rgb.G = Convert.ToByte(g*255.0f);
            rgb.B = Convert.ToByte(b*255.0f);
            return rgb;
        }

        public static void LoadSaturationValueFromHue(Color hue)
        {
            var cpSv = new Bitmap(256, 256);
            var g = Graphics.FromImage(cpSv);

            var tempHue = new Bitmap(PanPickerHue.BackgroundImage);
            var pointerindex = 0;
            for (var i = 0; i < 256; i++)
            {
                if (tempHue.GetPixel(0, i) == hue)
                {
                    pointerindex = i;
                    break;
                }
            }

            PicPickerHueLine.Top = pointerindex + PanPickerHue.Parent.Top + PanPickerHue.Parent.Padding.Top -
                                    (PicPickerHueLine.Height/2) + 1;
            PicPickerHueLine.Update();

            // create vertical guide gradient at leftmost and rightmost column of saturation-value map
            // create gradient at saturation = 0, value = 0-100%
            var lgb = new LinearGradientBrush(new Point(0, 0), new Point(0, 255), Color.White, Color.Black);
            g.DrawLine(new Pen(lgb), new Point(0, 0), new Point(0, 255));
            // create gradient at saturation = 100%, value = 0-100%
            lgb = new LinearGradientBrush(new Point(0, 0), new Point(0, 255), hue, Color.Black);
            g.DrawLine(new Pen(lgb), new Point(255, 0), new Point(255, 255));

            for (var i = 0; i < 256; i++)
            {
                lgb = new LinearGradientBrush(new Point(0, 0), new Point(255, 0), cpSv.GetPixel(0, i),
                    cpSv.GetPixel(255, i));
                g.DrawLine(new Pen(lgb), new Point(0, i), new Point(255, i));
            }

            //cpSV.RotateFlip(RotateFlipType.RotateNoneFlipY);
            PanPickerSv.Image = cpSv;
            PanPickerSv.Update();
            g.Dispose();
            lgb.Dispose();
        }

        public static void UpdateSelectedColor(Point pointer)
        {
            var temp = new Bitmap(PanPickerSv.Image);
            var mouse = PanPickerSv.PointToClient(pointer);
            if (mouse.X < 0) mouse.X = 0;
            if (mouse.Y < 0) mouse.Y = 0;
            if (mouse.X > 255) mouse.X = 255;
            if (mouse.Y > 255) mouse.Y = 255;

            var tmpclr = temp.GetPixel(mouse.X, mouse.Y);
            PicNewClr.BackColor = tmpclr;

            // RGB info
            DgvColorPick[2, 0].Value = tmpclr.R.ToString();
            DgvColorPick[2, 1].Value = tmpclr.G.ToString();
            DgvColorPick[2, 2].Value = tmpclr.B.ToString();
            // HSV info
            DgvColorPick[2, 3].Value = Math.Round(tmpclr.GetHue()).ToString();
            DgvColorPick[2, 4].Value = Math.Round(tmpclr.GetSaturation()*100).ToString();
            DgvColorPick[2, 5].Value = Math.Round(tmpclr.GetBrightness()*100).ToString();

            PickerBox_Move(mouse);
            temp.Dispose();
        }

        public static void UpdateCurrentColor(Color newColor)
        {
            PicCurClr.BackColor = newColor;

            // RGB info
            DgvColorPick[1, 0].Value = newColor.R.ToString();
            DgvColorPick[1, 1].Value = newColor.G.ToString();
            DgvColorPick[1, 2].Value = newColor.B.ToString();
            // HSV info
            DgvColorPick[1, 3].Value = Math.Round(newColor.GetHue()).ToString();
            DgvColorPick[1, 4].Value = Math.Round(newColor.GetSaturation()*100).ToString();
            DgvColorPick[1, 5].Value = Math.Round(newColor.GetBrightness()*100).ToString();
        }

        public static void UpdateColorTableData(Color newColor, int index)
        {
            // RGB info
            DgvCMap[2, index].Value = newColor.R.ToString();
            DgvCMap[3, index].Value = newColor.G.ToString();
            DgvCMap[4, index].Value = newColor.B.ToString();
            // HSV info
            DgvCMap[5, index].Value = Math.Round(newColor.GetHue()).ToString();
            DgvCMap[6, index].Value = Math.Round(newColor.GetSaturation()*100).ToString();
            DgvCMap[7, index].Value = Math.Round(newColor.GetBrightness()*100).ToString();
            // RGB color cell style
            for (var j = 2; j <= 7; j++)
            {
                DgvCMap[j, index].Style.BackColor = newColor;
                DgvCMap[j, index].Style.SelectionBackColor = newColor;

                var gB = (int) Math.Round((double) newColor.GetBrightness()*100);
                var gH = (int) Math.Round(newColor.GetHue());
                if (gB < 50 | gH > 212)
                {
                    DgvCMap[j, index].Style.ForeColor = Color.White;
                    DgvCMap[j, index].Style.SelectionForeColor = Color.White;
                }
                else
                {
                    DgvCMap[j, index].Style.ForeColor = Color.Black;
                    DgvCMap[j, index].Style.SelectionForeColor = Color.Black;
                }
            }

            ButCScale[index].BackColor = newColor;
        }

        public static void UpdateCustomColormap(Color newColor, int idxMarker)
        {
            var g = Graphics.FromImage(TmpEdit);

            // for leftmost color marker
            if (idxMarker == 0)
            {
                //Color newColor = butCScale[idxSelMarker].BackColor;
                var newIndex = Convert.ToInt32(DgvCMap[1, idxMarker].Value);
                var nextColor = ButCScale[idxMarker + 1].BackColor;
                var nextIndex = Convert.ToInt32(DgvCMap[1, idxMarker + 1].Value);

                // if bottom limit is > 0, assign bottom color to all index below it
                if (newIndex > 0)
                {
                    for (var i = 0; i < newIndex; i++)
                    {
                        TempCMap[i, 0] = newColor.R;
                        TempCMap[i, 1] = newColor.G;
                        TempCMap[i, 2] = newColor.B;
                    }
                }

                var lgb = new LinearGradientBrush(new Point(newIndex, 0), new Point(nextIndex, 0), newColor, nextColor);
                g.DrawLine(new Pen(lgb), new Point(newIndex, 0), new Point(nextIndex, 0));

                for (var i = newIndex + 1; i < nextIndex; i++)
                {
                    TempCMap[i, 0] = TmpEdit.GetPixel(i, 0).R;
                    TempCMap[i, 1] = TmpEdit.GetPixel(i, 0).G;
                    TempCMap[i, 2] = TmpEdit.GetPixel(i, 0).B;
                }

                TempCMap[newIndex, 0] = newColor.R;
                TempCMap[newIndex, 1] = newColor.G;
                TempCMap[newIndex, 2] = newColor.B;
            }

            // for rightmost color marker
            if (idxMarker == PicCScale.Count - 1)
            {
                //Color newColor = butCScale[idxSelMarker].BackColor;
                var newIndex = Convert.ToInt32(DgvCMap[1, idxMarker].Value);
                var prevColor = ButCScale[idxMarker - 1].BackColor;
                var prevIndex = Convert.ToInt32(DgvCMap[1, idxMarker - 1].Value);

                // if upper limit is < 255, assign upper color to all index after it
                if (newIndex < 255)
                {
                    for (var i = newIndex + 1; i <= 255; i++)
                    {
                        TempCMap[i, 0] = newColor.R;
                        TempCMap[i, 1] = newColor.G;
                        TempCMap[i, 2] = newColor.B;
                    }
                }

                var lgb = new LinearGradientBrush(new Point(prevIndex, 0), new Point(newIndex, 0), prevColor, newColor);
                g.DrawLine(new Pen(lgb), new Point(prevIndex, 0), new Point(newIndex, 0));

                for (var i = prevIndex + 1; i < newIndex; i++)
                {
                    TempCMap[i, 0] = TmpEdit.GetPixel(i, 0).R;
                    TempCMap[i, 1] = TmpEdit.GetPixel(i, 0).G;
                    TempCMap[i, 2] = TmpEdit.GetPixel(i, 0).B;
                }

                TempCMap[newIndex, 0] = newColor.R;
                TempCMap[newIndex, 1] = newColor.G;
                TempCMap[newIndex, 2] = newColor.B;
            }

            // for any color marker
            if (idxMarker != 0 & idxMarker != PicCScale.Count - 1)
            {
                //Color newColor = butCScale[idxSelMarker].BackColor;
                var newIndex = Convert.ToInt32(DgvCMap[1, idxMarker].Value);
                var nextColor = ButCScale[idxMarker + 1].BackColor;
                var nextIndex = Convert.ToInt32(DgvCMap[1, idxMarker + 1].Value);
                var prevColor = ButCScale[idxMarker - 1].BackColor;
                var prevIndex = Convert.ToInt32(DgvCMap[1, idxMarker - 1].Value);

                // for right-side of color marker
                var lgb = new LinearGradientBrush(new Point(newIndex, 0), new Point(nextIndex, 0), newColor, nextColor);
                g.DrawLine(new Pen(lgb), new Point(newIndex, 0), new Point(nextIndex, 0));

                for (var i = newIndex + 1; i < nextIndex; i++)
                {
                    TempCMap[i, 0] = TmpEdit.GetPixel(i, 0).R;
                    TempCMap[i, 1] = TmpEdit.GetPixel(i, 0).G;
                    TempCMap[i, 2] = TmpEdit.GetPixel(i, 0).B;
                }

                // for left-side of color marker
                lgb = new LinearGradientBrush(new Point(prevIndex, 0), new Point(newIndex, 0), prevColor, newColor);
                g.DrawLine(new Pen(lgb), new Point(prevIndex, 0), new Point(newIndex, 0));

                for (var i = prevIndex + 1; i < newIndex; i++)
                {
                    TempCMap[i, 0] = TmpEdit.GetPixel(i, 0).R;
                    TempCMap[i, 1] = TmpEdit.GetPixel(i, 0).G;
                    TempCMap[i, 2] = TmpEdit.GetPixel(i, 0).B;
                }

                TempCMap[newIndex, 0] = newColor.R;
                TempCMap[newIndex, 1] = newColor.G;
                TempCMap[newIndex, 2] = newColor.B;
            }

            g.Dispose();
        }

        public struct ColorRgb
        {
            public byte B;
            public byte G;
            public byte R;

            public ColorRgb(Color value)
            {
                R = value.R;
                G = value.G;
                B = value.B;
            }

            public static implicit operator Color(ColorRgb rgb)
            {
                var c = Color.FromArgb(rgb.R, rgb.G, rgb.B);
                return c;
            }

            public static explicit operator ColorRgb(Color c)
            {
                return new ColorRgb(c);
            }
        }

        #region "Color Marker Events"

        public static void ColorMarker_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            IdxSelMarker = 0;
            FMarkerSelected = true;

            var but = (ColorMarker) sender;
            // identify which button acts as the sender
            IdxSelMarker = but.ID;
            PanMarkerIsSelected.Left = but.Left;
            SelectMarkerOffset = ButCScale[IdxSelMarker].PointToClient(Control.MousePosition);

            // select row in color table
            DgvCMap.ClearSelection();
            DgvCMap.Rows[IdxSelMarker].Selected = true;

            PicCurClr.BackColor = but.BackColor;
            DgvColorPick[1, 0].Value = but.BackColor.R;
            DgvColorPick[1, 1].Value = but.BackColor.G;
            DgvColorPick[1, 2].Value = but.BackColor.B;
            DgvColorPick[1, 3].Value = Math.Round(but.BackColor.GetHue());
            DgvColorPick[1, 4].Value = Math.Round(but.BackColor.GetSaturation()*100);
            DgvColorPick[1, 5].Value = Math.Round(but.BackColor.GetBrightness()*100);
        }

        public static void ColorMarker_MouseMove(object sender, MouseEventArgs e)
        {
            // change marker position
            if (FMarkerSelected)
            {
                // set preset value to "Custom..."
                ComboPreset.SelectedIndex = ComboPreset.Items.Count - 1;

                // calculate new marker position
                var newleft = PicCMap.PointToClient(Control.MousePosition).X + PicCMap.Parent.Left +
                              PicCMap.Parent.Padding.Left;
                newleft = (int) (Math.Floor((double) newleft/2)*2) - 1;
                var byteval = (newleft - PicCMap.Parent.Left - PicCMap.Parent.Padding.Left)/2;

                // for leftmost color marker
                if (IdxSelMarker == 0)
                {
                    if (byteval < 0)
                    {
                        newleft = PicCMap.Parent.Left + PicCMap.Parent.Padding.Left;
                        byteval = 0;
                    }
                    if (byteval >= Convert.ToInt32(DgvCMap[1, IdxSelMarker + 1].Value))
                    {
                        newleft = (Convert.ToInt32(DgvCMap[1, IdxSelMarker + 1].Value) - 1)*2 + PicCMap.Parent.Left +
                                  PicCMap.Parent.Padding.Left;
                        byteval = (Convert.ToInt32(DgvCMap[1, IdxSelMarker + 1].Value) - 1);
                    }
                }

                // for rightmost color marker
                if (IdxSelMarker == PicCScale.Count - 1)
                {
                    if (byteval > 255)
                    {
                        newleft = (255*2) + PicCMap.Parent.Left + PicCMap.Parent.Padding.Left;
                        byteval = 255;
                    }
                    if (byteval <= Convert.ToInt32(DgvCMap[1, IdxSelMarker - 1].Value))
                    {
                        newleft = (Convert.ToInt32(DgvCMap[1, IdxSelMarker - 1].Value) + 1)*2 + PicCMap.Parent.Left +
                                  PicCMap.Parent.Padding.Left;
                        byteval = (Convert.ToInt32(DgvCMap[1, IdxSelMarker - 1].Value) + 1);
                    }
                }

                // for any color marker
                if (IdxSelMarker > 0 & IdxSelMarker < PicCScale.Count - 1)
                {
                    if (byteval >= Convert.ToInt32(DgvCMap[1, IdxSelMarker + 1].Value))
                    {
                        newleft = (Convert.ToInt32(DgvCMap[1, IdxSelMarker + 1].Value) - 1)*2 + PicCMap.Parent.Left +
                                  PicCMap.Parent.Padding.Left;
                        byteval = (Convert.ToInt32(DgvCMap[1, IdxSelMarker + 1].Value) - 1);
                    }
                    if (byteval <= Convert.ToInt32(DgvCMap[1, IdxSelMarker - 1].Value))
                    {
                        newleft = (Convert.ToInt32(DgvCMap[1, IdxSelMarker - 1].Value) + 1)*2 + PicCMap.Parent.Left +
                                  PicCMap.Parent.Padding.Left;
                        byteval = (Convert.ToInt32(DgvCMap[1, IdxSelMarker - 1].Value) + 1);
                    }
                }

                PicCScale[IdxSelMarker].Left = newleft;
                PicCScale[IdxSelMarker].Update();
                ButCScale[IdxSelMarker].Left = newleft - 4;
                ButCScale[IdxSelMarker].Update();
                PanMarkerIsSelected.Left = ButCScale[IdxSelMarker].Left;
                DgvCMap[1, IdxSelMarker].Value = byteval;

                // update color here
                UpdateCustomColormap(DgvCMap[2, IdxSelMarker].Style.BackColor, IdxSelMarker);
                UpdateBitmapColormap(TempCMap);
                PicCMap.Update();
            }
        }

        public static void ColorMarker_MouseUp(object sender, MouseEventArgs e)
        {
            FMarkerSelected = false;
        }

        public static void ColorMarker_DoubleClick(object sender, EventArgs e)
        {
            if (((MouseEventArgs) e).Button == MouseButtons.Left)
            {
                PanPickerSv.Parent.Parent.Parent.Parent.Show();
            }

            if (((MouseEventArgs) e).Button == MouseButtons.Right)
            {
                DeleteColorMarker(((ColorMarker) sender).ID);
            }
        }

        #endregion
    }
}