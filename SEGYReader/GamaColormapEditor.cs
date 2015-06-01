using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
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
        private static Bitmap bmpEdit = new Bitmap(512, 1);

        public static PictureBox _picCMap;
        public static DataGridView _dgvCMap;
        public static Form _frmCMap;

        public static void Setup(Form frmCMap, PictureBox picColorScale, DataGridView dgvCMap)
        {
            _frmCMap = frmCMap;
            _picCMap = picColorScale;
            _dgvCMap = dgvCMap;

            // set color table width
            int dgvwidth = 0;
            foreach (DataGridViewColumn col in _dgvCMap.Columns)
            {
                dgvwidth += col.Width;
            }
            dgvwidth += _dgvCMap.RowHeadersWidth;

            _dgvCMap.Parent.Width = dgvwidth + SystemInformation.VerticalScrollBarWidth + _dgvCMap.Parent.Padding.Left + 2;
        }

        public static void InitializeControl()
        {
            // load current colormap   
            for (int i = 0; i < 256; i++)
            {
                Color clr = System.Drawing.Color.FromArgb(ImageWriter.cScale[i, 0], ImageWriter.cScale[i, 1], ImageWriter.cScale[i, 2]);
                bmpEdit.SetPixel(i * 2, 0, clr);
                bmpEdit.SetPixel((i * 2) + 1, 0, clr);
                _picCMap.BackgroundImage = bmpEdit;
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
                _dgvCMap[1, i].Value = String.Concat(colorVal[i].R.ToString(), ", ", colorVal[i].G.ToString(), ", ", colorVal[i].B.ToString());
                _dgvCMap[1, i].Style.BackColor = colorVal[i];
                _dgvCMap[2, i].Value = colorByte[i].ToString();

                var gB = (int)Math.Round((double)colorVal[i].GetBrightness() * 100);
                if (gB < 50)
                { _dgvCMap[1, i].Style.ForeColor = Color.White; }
                else
                { _dgvCMap[1, i].Style.ForeColor = Color.Black; }

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

    }
}
