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
        private static List<Button> butCScale = new List<Button>();
        private static List<PictureBox> picCScale = new List<PictureBox>();

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
        public static NumericUpDown _numR;
        public static NumericUpDown _numG;
        public static NumericUpDown _numB;
        public static NumericUpDown _numH;
        public static NumericUpDown _numS;
        public static NumericUpDown _numV;
        public static PictureBox _picPickerHueLine;
        public static ToolStripComboBox _comboPreset;

        public static void Setup(Form frmCMap, PictureBox picColorScale, DataGridView dgvCMap, Panel panPickerHue, 
                                 PictureBox panPickerSV, PictureBox picPickerHueLine, ToolStripComboBox comboPreset)
        {
            _frmCMap = frmCMap;
            _picCMap = picColorScale;
            _dgvCMap = dgvCMap;
            _panPickerHue = panPickerHue;
            _panPickerSV = panPickerSV;
            _picPickerHueLine = picPickerHueLine;
            _comboPreset = comboPreset;
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
            Bitmap cpHue = new Bitmap(256, 1);
            for (int i = 0; i < 256; i++)
            {
                byte[] cRGB = new byte[3] { 0, 0, 0 };
                ColorFromHSV(cRGB, (int)Math.Round((((double)i+1)/256)*360), 100, 100);
                cpHue.SetPixel(i, 0, Color.FromArgb(cRGB[0], cRGB[1], cRGB[2]));
            }

            _panPickerHue.BackgroundImage = cpHue;

            LoadSaturationValueFromHue(Color.Red);
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
            foreach (Button but in butCScale)
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
                butCScale.Add(new Button());
                picCScale.Add(new PictureBox());
            }

            for (int i = 0; i < colorVal.Length; i++)
            {
                // for color table
                _dgvCMap[0, i].Value = (i + 1).ToString();
                _dgvCMap[1, i].Value = colorByte[i].ToString();
                // RGB info
                _dgvCMap[2, i].Value = colorVal[i].R.ToString();
                _dgvCMap[3, i].Value = colorVal[i].G.ToString();
                _dgvCMap[4, i].Value = colorVal[i].B.ToString();
                // HSV info
                _dgvCMap[5, i].Value = Math.Round(colorVal[i].GetHue()).ToString();
                _dgvCMap[6, i].Value = Math.Round(colorVal[i].GetSaturation() * 100).ToString();
                _dgvCMap[7, i].Value = Math.Round(colorVal[i].GetBrightness() * 100).ToString();
                // RGB color cell style
                for (int j = 2; j <= 7; j++)
                {
                    _dgvCMap[j, i].Style.BackColor = colorVal[i];

                    var gB = (int)Math.Round((double)colorVal[i].GetBrightness() * 100);
                    if (gB < 50)
                    { _dgvCMap[j, i].Style.ForeColor = Color.White; }
                    else
                    { _dgvCMap[j, i].Style.ForeColor = Color.Black; }
                }

                // for color marker button
                butCScale[i].Parent = (_picCMap.Parent).Parent;
                butCScale[i].Width = 10;
                butCScale[i].Height = 10;
                butCScale[i].FlatStyle = FlatStyle.Flat;
                butCScale[i].FlatAppearance.BorderColor = Color.Black;
                butCScale[i].FlatAppearance.BorderSize = 1;
                butCScale[i].FlatAppearance.MouseDownBackColor = colorVal[i];
                butCScale[i].FlatAppearance.MouseOverBackColor = colorVal[i];
                butCScale[i].BackColor = colorVal[i];
                butCScale[i].Left = (colorByte[i] * 2) + _picCMap.Parent.Left + _picCMap.Parent.Padding.Left - 4;
                butCScale[i].Top = _picCMap.Parent.Top + _picCMap.Parent.Height + 3;
                butCScale[i].Show();

                // adding event handler (this takes time)
                butCScale[i].MouseDown += ColorMarker_MouseDown;
                butCScale[i].MouseMove += ColorMarker_MouseMove;
                butCScale[i].MouseUp += ColorMarker_MouseUp;

                // for marker line
                picCScale[i].Parent = (_picCMap.Parent).Parent;
                picCScale[i].Width = 2;
                picCScale[i].Height = _picCMap.Parent.Height + 10;
                picCScale[i].BackColor = Color.Black;
                picCScale[i].Left = (colorByte[i] * 2) + _picCMap.Parent.Left + _picCMap.Parent.Padding.Left;
                picCScale[i].Top = _picCMap.Parent.Top - 5;
                picCScale[i].Show();
            }
        }

        private static void ColorFromHSV(byte[] cRGB, int H, int S, int V)
        {
            // scale the saturation and value to 0-1 range, hue is between 0-359
            double hue = (double)H;
            double sat = (double)S / 100.0;
            double val = (double)V / 100.0;
        
            double r = 0; double g = 0; double b = 0;
        
            if (sat == 0)
            {
                // if saturation is 0, then all color are the same (grayscale)
                r = val; g = val; b = val;
            }
            else
            {
                // calculate the appropriate sector of a six-part color wheel
                double sectorPos = hue / 60.0;
                int sectorNumber = (int)Math.Floor(sectorPos);
        
                // get the fractional part of the sector
                double fracSector = sectorPos - (double)sectorNumber;
        
                // calculate values for the three axes of the color
                double p = val * (1 - sat);
                double q = val * (1 - (sat * fracSector));
                double t = val * (1 - (sat * (1 - fracSector)));
        
                // assign the fractional colors to red, green, blue based on the sector the angle is in
                switch (sectorNumber)
                {
                    case 0:
                    case 6:
                        r = val; g = t; b = p; break;
                    case 1:
                        r = q; g = val; b = p; break;
                    case 2:
                        r = p; g = val; b = t; break;
                    case 3:
                        r = p; g = q; b = val; break;
                    case 4:
                        r = t; g = p; b = val; break;
                    case 5:
                        r = val; g = p; b = q; break;
                }
        
                // scale the rgb color to 0-255 range
                cRGB[0] = (byte)Math.Round(r * 255);
                cRGB[1] = (byte)Math.Round(g * 255);
                cRGB[2] = (byte)Math.Round(b * 255);
            }
        }

        //public static void LoadSaturationValueFromHue(int h)
        //{
        //    Bitmap cpSV = new Bitmap(256, 256);
        //    _picPickerHueLine.Left = (int)Math.Floor(((double)h / 360) * 256) + _panPickerHue.Parent.Left + _panPickerHue.Parent.Padding.Left;
        //    _picPickerHueLine.Update();
        //
        //    for (int s=0; s<256; s++)
        //        for (int v = 0; v<256; v++)
        //        {
        //            byte[] cRGB = new byte[3] { 0, 0, 0 };
        //            ColorFromHSV(cRGB, h, (int)Math.Round((((double)s + 2) / 256) * 100), (int)Math.Round((((double)v + 2) / 256) * 100));
        //            cpSV.SetPixel(s, v, Color.FromArgb(cRGB[0], cRGB[1], cRGB[2]));
        //        }
        //
        //    cpSV.RotateFlip(RotateFlipType.RotateNoneFlipY);
        //    _panPickerSV.BackgroundImage = cpSV;
        //    _panPickerSV.Update();
        //}

        public static void LoadSaturationValueFromHue(Color Hue)
        {
            Bitmap cpSV = new Bitmap(256, 256);
            Graphics g = Graphics.FromImage(cpSV);

            Bitmap tempHue = new Bitmap(_panPickerHue.BackgroundImage);
            int pointerindex = 0;
            for (int i = 0; i < 256; i++)
            {
                if (tempHue.GetPixel(i, 0) == Hue)
                { pointerindex = i; break; }
            }

            _picPickerHueLine.Left = pointerindex + _panPickerHue.Parent.Left + _panPickerHue.Parent.Padding.Left;
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

        public static void ColorMarker_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            idxSelMarker = 0; fMarkerSelected = true;
      
            Button but = (Button)sender;
            // identify which button acts as the sender
            for (int i = 0; i < butCScale.Count; i++)
            {
                if (but.BackColor == butCScale[i].BackColor)
                {
                    idxSelMarker = i; break;
                }
            }

            selectMarkerOffset = butCScale[idxSelMarker].PointToClient(Control.MousePosition);

            // select row in color table
            _dgvCMap.ClearSelection();
            _dgvCMap.Rows[idxSelMarker].Selected = true;
            _panPickerSV.Parent.Parent.Parent.Parent.Show(); 

            //LoadSaturationValueFromHue(but.BackColor);
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

                _dgvCMap[1, idxSelMarker].Value = byteval;

                // built-in color calculation here
                Graphics g = Graphics.FromImage(tmpEdit);

                // for leftmost color marker
                if (idxSelMarker == 0)
                {
                    Color newColor = butCScale[idxSelMarker].BackColor;
                    int newIndex = Convert.ToInt32(_dgvCMap[1, idxSelMarker].Value);
                    Color nextColor = butCScale[idxSelMarker + 1].BackColor;
                    int nextIndex = Convert.ToInt32(_dgvCMap[1, idxSelMarker + 1].Value);

                    // if bottom limit is > 0, assign bottom color to all index below it
                    if (newIndex > 0)
                    {
                        for (int i = 0; i < newIndex; i++)
                        { tempCMap[i, 0] = newColor.R; tempCMap[i, 1] = newColor.G; tempCMap[i, 2] = newColor.B; }
                    }

                    LinearGradientBrush lgb = new LinearGradientBrush(new Point(newIndex, 0), new Point(nextIndex, 0), newColor, nextColor);
                    g.DrawLine(new Pen(lgb),new Point(newIndex, 0),new Point(nextIndex, 0));

                    for (int i = newIndex+1; i < nextIndex; i++)
                    { tempCMap[i, 0] = tmpEdit.GetPixel(i, 0).R; tempCMap[i, 1] = tmpEdit.GetPixel(i, 0).G; tempCMap[i, 2] = tmpEdit.GetPixel(i, 0).B; }

                    tempCMap[newIndex, 0] = newColor.R; tempCMap[newIndex, 1] = newColor.G; tempCMap[newIndex, 2] = newColor.B;
                }

                // for rightmost color marker
                if (idxSelMarker == picCScale.Count - 1)
                {
                    Color newColor = butCScale[idxSelMarker].BackColor;
                    int newIndex = Convert.ToInt32(_dgvCMap[1, idxSelMarker].Value);
                    Color prevColor = butCScale[idxSelMarker - 1].BackColor;
                    int prevIndex = Convert.ToInt32(_dgvCMap[1, idxSelMarker - 1].Value);
                                
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
                if (idxSelMarker != 0 & idxSelMarker != picCScale.Count - 1)
                {
                    Color newColor = butCScale[idxSelMarker].BackColor;
                    int newIndex = Convert.ToInt32(_dgvCMap[1, idxSelMarker].Value);
                    Color nextColor = butCScale[idxSelMarker + 1].BackColor;
                    int nextIndex = Convert.ToInt32(_dgvCMap[1, idxSelMarker + 1].Value);
                    Color prevColor = butCScale[idxSelMarker - 1].BackColor;
                    int prevIndex = Convert.ToInt32(_dgvCMap[1, idxSelMarker - 1].Value);

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

                UpdateBitmapColormap(tempCMap);
                _picCMap.Update(); g.Dispose();
            }
                       
        }

        public static void ColorMarker_MouseUp(object sender, MouseEventArgs e)
        { fMarkerSelected = false; }


    }
}
