using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using Unplugged.Segy;

namespace SegyView
{
    public static class GamaColormapEditor
    {
        // list for color values
        private static List<Panel> editCScale = new List<Panel>();
        public static List<ColorMarker> butCScale = new List<ColorMarker>();
        private static List<PictureBox> picCScale = new List<PictureBox>();
        public static List<Panel> panboxPicker = new List<Panel>();

        public static byte[,] editCMap = new byte[256, 3];
        public static byte[,] tempCMap = new byte[256, 3];
        public static Bitmap tmpEdit = new Bitmap(256, 1);
        public static Bitmap bmpEdit = new Bitmap(512, 1);
        public static int idxSelMarker = 0;
        public static bool fMarkerSelected = false;
        public static Point selectMarkerOffset = new Point();

        public static PictureBox _picCMap;
        public static DataGridView _dgvCMap;
        public static Form _frmCMap;
        public static Panel _panPickerHue;
        public static PictureBox _panPickerSV;
        public static DataGridView _dgvColorPick;
        public static PictureBox _picPickerHueLine;
        public static ToolStripComboBox _comboPreset;
        public static PictureBox _picCurClr;
        public static PictureBox _picNewClr;
        public static Panel _panMarkerIsSelected;

        public struct ColorRGB
        {
            public byte R;
            public byte G;
            public byte B;
            public ColorRGB(Color value)
            {
                this.R = value.R;
                this.G = value.G;
                this.B = value.B;
            }
            public static implicit operator Color(ColorRGB rgb)
            {
                Color c = Color.FromArgb(rgb.R, rgb.G, rgb.B);
                return c;
            }
            public static explicit operator ColorRGB(Color c)
            {
                return new ColorRGB(c);
            }
        }

        public static void Setup(Form frmCMap, PictureBox picColorScale, DataGridView dgvCMap, Panel panPickerHue, 
                                 PictureBox panPickerSV, PictureBox picPickerHueLine, ToolStripComboBox comboPreset,
                                 DataGridView dgvColorPick, PictureBox picCurClr, PictureBox picNewClr,
                                 Panel panMarkerIsSelected)
        {
            _frmCMap = frmCMap;
            _picCMap = picColorScale;
            _dgvCMap = dgvCMap;
            _panPickerHue = panPickerHue;
            _panPickerSV = panPickerSV;
            _picPickerHueLine = picPickerHueLine;
            _comboPreset = comboPreset;
            _dgvColorPick = dgvColorPick;
            _picCurClr = picCurClr;
            _picNewClr = picNewClr;
            _panMarkerIsSelected = panMarkerIsSelected;
        }

        public static void InitializeColormapEditor()
        {
           Color clr = new Color();
           // load current colormap   
           for (int i = 0; i < 256; i++)
           {
               clr = System.Drawing.Color.FromArgb(ImageWriter.cScale[i, 0], ImageWriter.cScale[i, 1], ImageWriter.cScale[i, 2]);
               bmpEdit.SetPixel(i * 2, 0, clr);
               bmpEdit.SetPixel((i * 2) + 1, 0, clr);
               _picCMap.BackgroundImage = bmpEdit;
           }

            // create images for color picker
            Bitmap cpHue = new Bitmap(1, 256);
            for (int i = 0; i < 256; i++)
            {
                ColorRGB cRGB = new ColorRGB();
                cRGB = HSL2RGB((((double)i) / 256), 1, 0.5);
                cpHue.SetPixel(0, i, Color.FromArgb(cRGB.R, cRGB.G, cRGB.B));
            }
            
            _panPickerHue.BackgroundImage = cpHue;
            LoadSaturationValueFromHue(Color.Red);

            // create picker box
            for (int i = 0; i < 4; i++) panboxPicker.Add(new Panel());
            for (int i = 0; i < 4; i++)
            {
                panboxPicker[i].Parent = _panPickerSV;
                panboxPicker[i].BackColor = Color.Black;
                panboxPicker[i].Show();
            }

            panboxPicker[0].Width = 1; panboxPicker[0].Height = 7;
            panboxPicker[1].Width = 7; panboxPicker[1].Height = 1;
            panboxPicker[2].Width = 1; panboxPicker[2].Height = 7;
            panboxPicker[3].Width = 7; panboxPicker[3].Height = 1;

            UpdateSelectedColor(_panPickerSV.PointToScreen(new Point(255, 0)));
        }

        public static void PickerBox_Move(Point mouse)
        {
            Point pointer = mouse;
            if (pointer.X < 0) pointer.X = 0;
            if (pointer.Y < 0) pointer.Y = 0;
            if (pointer.X > 255) pointer.X = 255;
            if (pointer.Y > 255) pointer.Y = 255;

            /* left   */ panboxPicker[0].Left = pointer.X - 3; panboxPicker[0].Top = pointer.Y - 3;
            /* top    */ panboxPicker[1].Left = pointer.X - 3; panboxPicker[1].Top = pointer.Y - 3;
            /* right  */ panboxPicker[2].Left = pointer.X + 3; panboxPicker[2].Top = pointer.Y - 3;
            /* bottom */ panboxPicker[3].Left = pointer.X - 3; panboxPicker[3].Top = pointer.Y + 3;

            for (int i = 0; i < 4; i++)
            {
                if (((Bitmap)_panPickerSV.Image).GetPixel(pointer.X, pointer.Y).GetBrightness() * 100 > 30)
                { panboxPicker[i].BackColor = Color.Black; }
                else
                { panboxPicker[i].BackColor = Color.White; }

                panboxPicker[i].Update();
            }
        }

        public static void UpdateBitmapColormap(byte[,] cScale)
        {
            bmpEdit = new Bitmap(512, 1);
            for (int i = 0; i < 256; i++)
            {
                Color clr = System.Drawing.Color.FromArgb(cScale[i, 0], cScale[i, 1], cScale[i, 2]);
                bmpEdit.SetPixel(i * 2, 0, clr);
                bmpEdit.SetPixel((i * 2) + 1, 0, clr);
                _picCMap.BackgroundImage = bmpEdit;
            }

            tempCMap = cScale;
        }

        public static void LoadColormapTable(Color[] colorVal, int[] colorByte)
        {
            // clear existing color marker and table
            foreach (Panel but in butCScale)
            {
                but.Hide();
            }
            foreach (PictureBox pic in picCScale)
            {
                pic.Hide();
            }
            
            // add color marker and table
            _dgvCMap.Rows.Clear();
            butCScale.Clear();
            picCScale.Clear();

            for (int i = 0; i < colorVal.Length; i++)
            { 
                _dgvCMap.Rows.Add();
                butCScale.Add(new ColorMarker());
                picCScale.Add(new PictureBox());
            }

            for (int i = 0; i < colorVal.Length; i++)
            {
                // for color table
                _dgvCMap[0, i].Value = (i + 1).ToString();
                _dgvCMap[1, i].Value = colorByte[i].ToString();

                UpdateColorTableData(colorVal[i], i);

                // for color marker button
                butCScale[i].ID = i;
                butCScale[i].Parent = (_picCMap.Parent).Parent;
                butCScale[i].Width = 10;
                butCScale[i].Height = 10;
                butCScale[i].BorderStyle = BorderStyle.FixedSingle;
                butCScale[i].BackColor = colorVal[i];
                butCScale[i].Left = (colorByte[i] * 2) + _picCMap.Parent.Left + _picCMap.Parent.Padding.Left - 4;
                butCScale[i].Top = _picCMap.Parent.Top + _picCMap.Parent.Height + 3;
                
                // adding event handler (this takes time)
                butCScale[i].MouseDown += ColorMarker_MouseDown;
                butCScale[i].MouseMove += ColorMarker_MouseMove;
                butCScale[i].MouseUp += ColorMarker_MouseUp;
                butCScale[i].DoubleClick += ColorMarker_DoubleClick;

                // for marker line
                picCScale[i].Parent = (_picCMap.Parent).Parent;
                picCScale[i].Width = 2;
                picCScale[i].Height = _picCMap.Parent.Height + 10;
                picCScale[i].BackColor = Color.Black;
                picCScale[i].Left = (colorByte[i] * 2) + _picCMap.Parent.Left + _picCMap.Parent.Padding.Left;
                picCScale[i].Top = _picCMap.Parent.Top - 5;

                picCScale[i].BringToFront(); picCScale[i].Show();
                butCScale[i].BringToFront(); butCScale[i].Show();
            }

            _picCMap.Parent.BringToFront();
            _dgvColorPick.Parent.Hide();
  
            // for current color, load first color or selected color
            if (idxSelMarker < butCScale.Count)
            {
                UpdateCurrentColor(colorVal[idxSelMarker]);
                _panMarkerIsSelected.Left = butCScale[idxSelMarker].Left;
            }
            else
            {
                UpdateCurrentColor(colorVal[0]);
                _panMarkerIsSelected.Left = butCScale[0].Left;
            }
          
        }

        public static void AddColorMarker(Color newColor, int newIndex)
        {
            // abort if newIndex already exist
            for (int i = 0; i < _dgvCMap.Rows.Count; i++)
            {
                if (Convert.ToInt32(_dgvCMap[1, i].Value) == newIndex) return; 
            }

            // define new Color[] and int[]
            Color[] tempColor = new Color[_dgvCMap.Rows.Count + 1];
            int[] tempIndex = new int[_dgvCMap.Rows.Count + 1];
            int newColorIdx = 0;

            // check if newIndex is the leftmost marker
            if (newIndex < Convert.ToInt32(_dgvCMap[1, 0].Value))
            { newColorIdx = 0; }

            // check if newIndex is the leftmost marker
            if (newIndex > Convert.ToInt32(_dgvCMap[1, _dgvCMap.Rows.Count-1].Value))
            { newColorIdx = _dgvCMap.Rows.Count; }

            // check if newIndex is between any two markers
            if (newIndex > Convert.ToInt32(_dgvCMap[1, 0].Value) & newIndex < Convert.ToInt32(_dgvCMap[1, _dgvCMap.Rows.Count-1].Value))
            {
                for (int i = 0; i < _dgvCMap.Rows.Count - 1; i++)
                {
                    if (newIndex > Convert.ToInt32(_dgvCMap[1, i].Value) & newIndex < Convert.ToInt32(_dgvCMap[1, i + 1].Value))
                    { newColorIdx = i + 1; }
                }
            }

            // create new color and index array
            if (newColorIdx == 0)
            {
                tempColor[0] = newColor; 
                tempIndex[0] = newIndex;

                for (int i = 1; i < tempColor.Length; i++)
                {
                    tempColor[i] = _dgvCMap[2, i - 1].Style.BackColor;
                    tempIndex[i] = Convert.ToInt32(_dgvCMap[1, i - 1].Value);
                }
            }

            if (newColorIdx == tempColor.Length - 1)
            {
                for (int i = 0; i < tempColor.Length-1; i++)
                {
                    tempColor[i] = _dgvCMap[2, i].Style.BackColor;
                    tempIndex[i] = Convert.ToInt32(_dgvCMap[1, i].Value);
                }

                tempColor[tempColor.Length - 1] = newColor; 
                tempIndex[tempColor.Length - 1] = newIndex;
            }

            if (newColorIdx != 0 & newColorIdx != tempColor.Length - 1)
            {
                for (int i = 0; i < newColorIdx; i++)
                {
                    tempColor[i] = _dgvCMap[2, i].Style.BackColor;
                    tempIndex[i] = Convert.ToInt32(_dgvCMap[1, i].Value);
                }

                tempColor[newColorIdx] = newColor;
                tempIndex[newColorIdx] = newIndex;

                for (int i = newColorIdx + 1; i < tempColor.Length; i++)
                {
                    tempColor[i] = _dgvCMap[2, i - 1].Style.BackColor;
                    tempIndex[i] = Convert.ToInt32(_dgvCMap[1, i - 1].Value);
                }
            }

            GamaSeismicColor.CreateColormap(editCMap, tempColor, tempIndex);
            LoadColormapTable(tempColor, tempIndex);
            UpdateBitmapColormap(editCMap);
            _dgvColorPick.Parent.Hide();
        }

        public static void DeleteColorMarker(int markerIndex)
        {
            if (butCScale.Count <= 2) return;

            // define new Color[] and int[]
            Color[] tempColor = new Color[_dgvCMap.Rows.Count - 1];
            int[] tempIndex = new int[_dgvCMap.Rows.Count - 1];

            // if marker to be deleted is the leftmost marker
            if (markerIndex == 0)
            {
                for (int i = 1; i < _dgvCMap.Rows.Count; i++)
                {
                    tempColor[i-1] = _dgvCMap[2, i].Style.BackColor;
                    tempIndex[i-1] = Convert.ToInt32(_dgvCMap[1, i].Value);
                }
            }

            // if marker to be deleted is the rightmost marker
            if (markerIndex == _dgvCMap .Rows.Count - 1)
            {
                for (int i = 0; i < _dgvCMap.Rows.Count-1; i++)
                {
                    tempColor[i] = _dgvCMap[2, i].Style.BackColor;
                    tempIndex[i] = Convert.ToInt32(_dgvCMap[1, i].Value);
                }
            }

            // if marker to be deleted is any marker in the middle
            if (markerIndex != 0 & markerIndex != _dgvCMap.Rows.Count - 1)
            {
                for (int i = 0; i < markerIndex; i++)
                {
                    tempColor[i] = _dgvCMap[2, i].Style.BackColor;
                    tempIndex[i] = Convert.ToInt32(_dgvCMap[1, i].Value);
                }

                for (int i = markerIndex + 1; i < _dgvCMap.Rows.Count; i++)
                {
                    tempColor[i - 1] = _dgvCMap[2, i].Style.BackColor;
                    tempIndex[i - 1] = Convert.ToInt32(_dgvCMap[1, i].Value);
                }
            }

            GamaSeismicColor.CreateColormap(editCMap, tempColor, tempIndex);
            LoadColormapTable(tempColor, tempIndex);
            UpdateBitmapColormap(editCMap);
            _dgvColorPick.Parent.Hide();
        }

        public static ColorRGB HSL2RGB(double h, double sl, double l)
        {
            double v;
            double r, g, b;

            r = l; g = l; b = l;

            v = (l <= 0.5) ? (l * (1.0 + sl)) : (l + sl - l * sl);
            if (v > 0)
            {
                double m;
                double sv;
                int sextant;
                double fract, vsf, mid1, mid2;

                m = l + l - v;
                sv = (v - m) / v;
                h *= 6.0;
                sextant = (int)h;
                fract = h - sextant;
                vsf = v * sv * fract;
                mid1 = m + vsf;
                mid2 = v - vsf;
                switch (sextant)
                {
                    case 0:
                        r = v; g = mid1; b = m; break;
                    case 1:
                        r = mid2; g = v; b = m; break;
                    case 2:
                        r = m; g = v; b = mid1; break;
                    case 3:
                        r = m; g = mid2; b = v; break;
                    case 4:
                        r = mid1; g = m; b = v; break;
                    case 5:
                        r = v; g = m; b = mid2; break;
                }
            }
            ColorRGB rgb;
            rgb.R = Convert.ToByte(r * 255.0f);
            rgb.G = Convert.ToByte(g * 255.0f);
            rgb.B = Convert.ToByte(b * 255.0f);
            return rgb;
        }

        public static void LoadSaturationValueFromHue(Color Hue)
        {
            Bitmap cpSV = new Bitmap(256, 256);
            Graphics g = Graphics.FromImage(cpSV);

            Bitmap tempHue = new Bitmap(_panPickerHue.BackgroundImage);
            int pointerindex = 0;
            for (int i = 0; i < 256; i++)
            {
                if (tempHue.GetPixel(0, i) == Hue)
                { pointerindex = i; break; }
            }

            _picPickerHueLine.Top = pointerindex + _panPickerHue.Parent.Top + _panPickerHue.Parent.Padding.Top - (_picPickerHueLine.Height / 2) + 1;
            _picPickerHueLine.Update();

            // create vertical guide gradient at leftmost and rightmost column of saturation-value map
            // create gradient at saturation = 0, value = 0-100%
            LinearGradientBrush lgb = new LinearGradientBrush(new Point(0, 0), new Point(0, 255), Color.White, Color.Black);
            g.DrawLine(new Pen(lgb), new Point(0, 0), new Point(0, 255));
            // create gradient at saturation = 100%, value = 0-100%
            lgb = new LinearGradientBrush(new Point(0, 0), new Point(0, 255), Hue, Color.Black);
            g.DrawLine(new Pen(lgb), new Point(255, 0), new Point(255, 255));

            for (int i = 0; i < 256; i++)
            {
                lgb = new LinearGradientBrush(new Point(0, 0), new Point(255, 0), cpSV.GetPixel(0, i), cpSV.GetPixel(255, i));
                g.DrawLine(new Pen(lgb), new Point(0, i), new Point(255, i));
            }

            //cpSV.RotateFlip(RotateFlipType.RotateNoneFlipY);
            _panPickerSV.Image = cpSV;
            _panPickerSV.Update();
            g.Dispose();
            lgb.Dispose();
        }

        public static void UpdateSelectedColor(Point pointer)
        {
            Bitmap temp = new Bitmap(_panPickerSV.Image);
            Point mouse = _panPickerSV.PointToClient(pointer);
            if (mouse.X < 0) mouse.X = 0;
            if (mouse.Y < 0) mouse.Y = 0;
            if (mouse.X > 255) mouse.X = 255;
            if (mouse.Y > 255) mouse.Y = 255;

            Color tmpclr = temp.GetPixel(mouse.X, mouse.Y);
            _picNewClr.BackColor = tmpclr;

            // RGB info
            _dgvColorPick[2, 0].Value = tmpclr.R.ToString();
            _dgvColorPick[2, 1].Value = tmpclr.G.ToString();
            _dgvColorPick[2, 2].Value = tmpclr.B.ToString();
            // HSV info
            _dgvColorPick[2, 3].Value = Math.Round(tmpclr.GetHue()).ToString();
            _dgvColorPick[2, 4].Value = Math.Round(tmpclr.GetSaturation() * 100).ToString();
            _dgvColorPick[2, 5].Value = Math.Round(tmpclr.GetBrightness() * 100).ToString();

            PickerBox_Move(mouse);
            temp.Dispose();
        }

        public static void UpdateCurrentColor(Color newColor)
        {
            _picCurClr.BackColor = newColor;

            // RGB info
            _dgvColorPick[1, 0].Value = newColor.R.ToString();
            _dgvColorPick[1, 1].Value = newColor.G.ToString();
            _dgvColorPick[1, 2].Value = newColor.B.ToString();
            // HSV info
            _dgvColorPick[1, 3].Value = Math.Round(newColor.GetHue()).ToString();
            _dgvColorPick[1, 4].Value = Math.Round(newColor.GetSaturation() * 100).ToString();
            _dgvColorPick[1, 5].Value = Math.Round(newColor.GetBrightness() * 100).ToString();
  
        }

        public static void UpdateColorTableData(Color newColor, int index)
        {
            // RGB info
            _dgvCMap[2, index].Value = newColor.R.ToString();
            _dgvCMap[3, index].Value = newColor.G.ToString();
            _dgvCMap[4, index].Value = newColor.B.ToString();
            // HSV info
            _dgvCMap[5, index].Value = Math.Round(newColor.GetHue()).ToString();
            _dgvCMap[6, index].Value = Math.Round(newColor.GetSaturation() * 100).ToString();
            _dgvCMap[7, index].Value = Math.Round(newColor.GetBrightness() * 100).ToString();
            // RGB color cell style
            for (int j = 2; j <= 7; j++)
            {
                _dgvCMap[j, index].Style.BackColor = newColor;
                _dgvCMap[j, index].Style.SelectionBackColor = newColor;

                var gB = (int)Math.Round((double)newColor.GetBrightness() * 100);
                var gH = (int)Math.Round((double)newColor.GetHue());
                if (gB < 50 | gH > 212)
                {
                    _dgvCMap[j, index].Style.ForeColor = Color.White;
                    _dgvCMap[j, index].Style.SelectionForeColor = Color.White;
                }
                else
                {
                    _dgvCMap[j, index].Style.ForeColor = Color.Black;
                    _dgvCMap[j, index].Style.SelectionForeColor = Color.Black;
                }
            }

            butCScale[index].BackColor = newColor; 
        }

        public static void UpdateCustomColormap(Color newColor, int idxMarker)
        {
            Graphics g = Graphics.FromImage(tmpEdit);

            // for leftmost color marker
            if (idxMarker == 0)
            {
                //Color newColor = butCScale[idxSelMarker].BackColor;
                int newIndex = Convert.ToInt32(_dgvCMap[1, idxMarker].Value);
                Color nextColor = butCScale[idxMarker + 1].BackColor;
                int nextIndex = Convert.ToInt32(_dgvCMap[1, idxMarker + 1].Value);

                // if bottom limit is > 0, assign bottom color to all index below it
                if (newIndex > 0)
                {
                    for (int i = 0; i < newIndex; i++)
                    { tempCMap[i, 0] = newColor.R; tempCMap[i, 1] = newColor.G; tempCMap[i, 2] = newColor.B; }
                }

                LinearGradientBrush lgb = new LinearGradientBrush(new Point(newIndex, 0), new Point(nextIndex, 0), newColor, nextColor);
                g.DrawLine(new Pen(lgb), new Point(newIndex, 0), new Point(nextIndex, 0));

                for (int i = newIndex + 1; i < nextIndex; i++)
                { tempCMap[i, 0] = tmpEdit.GetPixel(i, 0).R; tempCMap[i, 1] = tmpEdit.GetPixel(i, 0).G; tempCMap[i, 2] = tmpEdit.GetPixel(i, 0).B; }

                tempCMap[newIndex, 0] = newColor.R; tempCMap[newIndex, 1] = newColor.G; tempCMap[newIndex, 2] = newColor.B;
            }

            // for rightmost color marker
            if (idxMarker == picCScale.Count - 1)
            {
                //Color newColor = butCScale[idxSelMarker].BackColor;
                int newIndex = Convert.ToInt32(_dgvCMap[1, idxMarker].Value);
                Color prevColor = butCScale[idxMarker - 1].BackColor;
                int prevIndex = Convert.ToInt32(_dgvCMap[1, idxMarker - 1].Value);

                // if upper limit is < 255, assign upper color to all index after it
                if (newIndex < 255)
                {
                    for (int i = newIndex + 1; i <= 255; i++)
                    { tempCMap[i, 0] = newColor.R; tempCMap[i, 1] = newColor.G; tempCMap[i, 2] = newColor.B; }
                }

                LinearGradientBrush lgb = new LinearGradientBrush(new Point(prevIndex, 0), new Point(newIndex, 0), prevColor, newColor);
                g.DrawLine(new Pen(lgb), new Point(prevIndex, 0), new Point(newIndex, 0));

                for (int i = prevIndex + 1; i < newIndex; i++)
                { tempCMap[i, 0] = tmpEdit.GetPixel(i, 0).R; tempCMap[i, 1] = tmpEdit.GetPixel(i, 0).G; tempCMap[i, 2] = tmpEdit.GetPixel(i, 0).B; }

                tempCMap[newIndex, 0] = newColor.R; tempCMap[newIndex, 1] = newColor.G; tempCMap[newIndex, 2] = newColor.B;
            }

            // for any color marker
            if (idxMarker != 0 & idxMarker != picCScale.Count - 1)
            {
                //Color newColor = butCScale[idxSelMarker].BackColor;
                int newIndex = Convert.ToInt32(_dgvCMap[1, idxMarker].Value);
                Color nextColor = butCScale[idxMarker + 1].BackColor;
                int nextIndex = Convert.ToInt32(_dgvCMap[1, idxMarker + 1].Value);
                Color prevColor = butCScale[idxMarker - 1].BackColor;
                int prevIndex = Convert.ToInt32(_dgvCMap[1, idxMarker - 1].Value);

                // for right-side of color marker
                LinearGradientBrush lgb = new LinearGradientBrush(new Point(newIndex, 0), new Point(nextIndex, 0), newColor, nextColor);
                g.DrawLine(new Pen(lgb), new Point(newIndex, 0), new Point(nextIndex, 0));

                for (int i = newIndex + 1; i < nextIndex; i++)
                { tempCMap[i, 0] = tmpEdit.GetPixel(i, 0).R; tempCMap[i, 1] = tmpEdit.GetPixel(i, 0).G; tempCMap[i, 2] = tmpEdit.GetPixel(i, 0).B; }

                // for left-side of color marker
                lgb = new LinearGradientBrush(new Point(prevIndex, 0), new Point(newIndex, 0), prevColor, newColor);
                g.DrawLine(new Pen(lgb), new Point(prevIndex, 0), new Point(newIndex, 0));

                for (int i = prevIndex + 1; i < newIndex; i++)
                { tempCMap[i, 0] = tmpEdit.GetPixel(i, 0).R; tempCMap[i, 1] = tmpEdit.GetPixel(i, 0).G; tempCMap[i, 2] = tmpEdit.GetPixel(i, 0).B; }

                tempCMap[newIndex, 0] = newColor.R; tempCMap[newIndex, 1] = newColor.G; tempCMap[newIndex, 2] = newColor.B;
            }

            g.Dispose();
        }

        #region "Color Marker Events"
        public static void ColorMarker_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            idxSelMarker = 0; fMarkerSelected = true;

            ColorMarker but = (ColorMarker)sender;
            // identify which button acts as the sender
            idxSelMarker = but.ID;
            _panMarkerIsSelected.Left = but.Left;
            selectMarkerOffset = butCScale[idxSelMarker].PointToClient(Control.MousePosition);

            // select row in color table
            _dgvCMap.ClearSelection();
            _dgvCMap.Rows[idxSelMarker].Selected = true;

            _picCurClr.BackColor = but.BackColor;
            _dgvColorPick[1, 0].Value = but.BackColor.R;
            _dgvColorPick[1, 1].Value = but.BackColor.G;
            _dgvColorPick[1, 2].Value = but.BackColor.B;
            _dgvColorPick[1, 3].Value = Math.Round(but.BackColor.GetHue());
            _dgvColorPick[1, 4].Value = Math.Round(but.BackColor.GetSaturation() * 100);
            _dgvColorPick[1, 5].Value = Math.Round(but.BackColor.GetBrightness() * 100); 
        }

        public static void ColorMarker_MouseMove(object sender, MouseEventArgs e)
        {
                // change marker position
            if (fMarkerSelected == true)
            {
                // set preset value to "Custom..."
                _comboPreset.SelectedIndex = _comboPreset.Items.Count - 1;

                // calculate new marker position
                int newleft = _picCMap.PointToClient(Control.MousePosition).X + _picCMap.Parent.Left + _picCMap.Parent.Padding.Left;
                newleft = (int)(Math.Floor((double)newleft / 2) * 2) - 1;
                int byteval = (newleft - _picCMap.Parent.Left - _picCMap.Parent.Padding.Left) / 2;

                // for leftmost color marker
                if (idxSelMarker == 0)
                {
                    if (byteval < 0)
                    {
                        newleft = _picCMap.Parent.Left + _picCMap.Parent.Padding.Left;
                        byteval = 0;
                    }
                    if (byteval >= Convert.ToInt32(_dgvCMap[1, idxSelMarker + 1].Value))
                    {
                        newleft = (Convert.ToInt32(_dgvCMap[1, idxSelMarker + 1].Value) - 1) * 2 + _picCMap.Parent.Left + _picCMap.Parent.Padding.Left;
                        byteval = (Convert.ToInt32(_dgvCMap[1, idxSelMarker + 1].Value) - 1);
                    }
                }

                // for rightmost color marker
                if (idxSelMarker == picCScale.Count - 1)
                {
                    if (byteval > 255)
                    {
                        newleft = (255 * 2) + _picCMap.Parent.Left + _picCMap.Parent.Padding.Left;
                        byteval = 255;
                    }
                    if (byteval <= Convert.ToInt32(_dgvCMap[1, idxSelMarker - 1].Value))
                    {
                        newleft = (Convert.ToInt32(_dgvCMap[1, idxSelMarker - 1].Value) + 1) * 2 + _picCMap.Parent.Left + _picCMap.Parent.Padding.Left;
                        byteval = (Convert.ToInt32(_dgvCMap[1, idxSelMarker - 1].Value) + 1);
                    }
                }

                // for any color marker
                if (idxSelMarker > 0 & idxSelMarker < picCScale.Count - 1)
                {
                    if (byteval >= Convert.ToInt32(_dgvCMap[1, idxSelMarker + 1].Value))
                    {
                        newleft = (Convert.ToInt32(_dgvCMap[1, idxSelMarker + 1].Value) - 1) * 2 + _picCMap.Parent.Left + _picCMap.Parent.Padding.Left;
                        byteval = (Convert.ToInt32(_dgvCMap[1, idxSelMarker + 1].Value) - 1);
                    }
                    if (byteval <= Convert.ToInt32(_dgvCMap[1, idxSelMarker - 1].Value))
                    {
                        newleft = (Convert.ToInt32(_dgvCMap[1, idxSelMarker - 1].Value) + 1) * 2 + _picCMap.Parent.Left + _picCMap.Parent.Padding.Left;
                        byteval = (Convert.ToInt32(_dgvCMap[1, idxSelMarker - 1].Value) + 1);
                    }
                }

                picCScale[idxSelMarker].Left = newleft; picCScale[idxSelMarker].Update();
                butCScale[idxSelMarker].Left = newleft - 4; butCScale[idxSelMarker].Update();
                _panMarkerIsSelected.Left = butCScale[idxSelMarker].Left;
                _dgvCMap[1, idxSelMarker].Value = byteval;

                // update color here
                UpdateCustomColormap(_dgvCMap[2, idxSelMarker].Style.BackColor, idxSelMarker);
                UpdateBitmapColormap(tempCMap);
                _picCMap.Update(); 
            }
                       
        }

        public static void ColorMarker_MouseUp(object sender, MouseEventArgs e)
        { fMarkerSelected = false; }

        public static void ColorMarker_DoubleClick(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Left)
            { _panPickerSV.Parent.Parent.Parent.Parent.Show(); }

            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            { DeleteColorMarker(((ColorMarker)sender).ID); } 
        }
        #endregion
    }
}
