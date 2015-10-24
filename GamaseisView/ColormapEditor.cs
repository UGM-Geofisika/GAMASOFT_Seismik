using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Unplugged.Segy;

namespace SegyView
{
    public partial class ColormapEditor : Form
    {
        
        public ColormapEditor()
        {
            InitializeComponent();

            dgvPickColor.Rows.Add(6);
            dgvPickColor[0, 0].Value = "Red";
            dgvPickColor[0, 1].Value = "Green";
            dgvPickColor[0, 2].Value = "Blue";
            dgvPickColor[0, 3].Value = "Hue";
            dgvPickColor[0, 4].Value = "Saturation";
            dgvPickColor[0, 5].Value = "Value";
        }

        private void ColormapEditor_Load(object sender, EventArgs e)
        {
            GamaColormapEditor.InitializeColormapEditor();

            panAddMarkerIndicator.SendToBack();
            pictureBox1.SendToBack();
            pictureBox2.SendToBack();
            pictureBox3.SendToBack();
            pictureBox4.SendToBack();
        }

        private void comboPreset_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboPreset.SelectedIndex)
            {   
                case 0: // Preset: Black-White-Red
                    Color[] newColor = new Color[3] { Color.FromArgb(0, 0, 0), Color.FromArgb(255, 255, 255), Color.FromArgb(255, 0, 0) };
                    int[] newIndex = new int[3] { 0, 127, 255 };

                    GamaSeismicColor.CreateColormap(GamaColormapEditor.EditCMap, newColor, newIndex);
                    GamaColormapEditor.LoadColormapTable(newColor, newIndex);
                    GamaColormapEditor.UpdateBitmapColormap(GamaColormapEditor.EditCMap);
                    break;

                case 1: // Preset: Blue-White-Red
                    newColor = new Color[3] { Color.FromArgb(0, 0, 255), Color.FromArgb(255, 255, 255), Color.FromArgb(255, 0, 0) };
                    newIndex = new int[3] { 0, 127, 255 };

                    GamaSeismicColor.CreateColormap(GamaColormapEditor.EditCMap, newColor, newIndex);
                    GamaColormapEditor.LoadColormapTable(newColor, newIndex);
                    GamaColormapEditor.UpdateBitmapColormap(GamaColormapEditor.EditCMap);
                    break;

                case 2: // Preset: Grayscale
                    newColor = new Color[2] { Color.FromArgb(0, 0, 0), Color.FromArgb(255, 255, 255) };
                    newIndex = new int[2] { 0, 255 };

                    GamaSeismicColor.CreateColormap(GamaColormapEditor.EditCMap, newColor, newIndex);
                    GamaColormapEditor.LoadColormapTable(newColor, newIndex);
                    GamaColormapEditor.UpdateBitmapColormap(GamaColormapEditor.EditCMap);
                    break;

                case 3: // Preset: Rainbow
                    newColor = new Color[6] { Color.Red, Color.DarkOrange, Color.Yellow, Color.Lime, Color.Blue, Color.DarkViolet };
                    newIndex = new int[6] { 0, 51, 102, 153, 204, 255 };

                    GamaSeismicColor.CreateColormap(GamaColormapEditor.EditCMap, newColor, newIndex);
                    GamaColormapEditor.LoadColormapTable(newColor, newIndex);
                    GamaColormapEditor.UpdateBitmapColormap(GamaColormapEditor.EditCMap);
                    break;
            }
        }

        private void panPickerHue_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Bitmap hueImage = new Bitmap(panPickerHue.BackgroundImage);
                int mouseY = panPickerHue.PointToClient(MousePosition).Y;
                if (mouseY < 0) mouseY = 0;
                if (mouseY > 255) mouseY = 255;

                GamaColormapEditor.LoadSaturationValueFromHue(hueImage.GetPixel(0, mouseY));
                GamaColormapEditor.UpdateSelectedColor(panPickerSV.PointToScreen(new Point(GamaColormapEditor.PanboxPicker[0].Left + 3, GamaColormapEditor.PanboxPicker[1].Top + 3)));
                picNewClr.Update(); dgvPickColor.Update();
                hueImage.Dispose();
            }
        }

        private void panPickerHue_Click(object sender, EventArgs e)
        {
            Bitmap hueImage = new Bitmap(panPickerHue.BackgroundImage);
            int mouseY = panPickerHue.PointToClient(MousePosition).Y;
            if (mouseY < 0) mouseY = 0;
            if (mouseY > 255) mouseY = 255;

            GamaColormapEditor.LoadSaturationValueFromHue(hueImage.GetPixel(0, mouseY));
            GamaColormapEditor.UpdateSelectedColor(panPickerSV.PointToScreen(new Point(GamaColormapEditor.PanboxPicker[0].Left + 3, GamaColormapEditor.PanboxPicker[1].Top + 3)));
            picNewClr.Update(); dgvPickColor.Update();
            hueImage.Dispose();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            switch (comboPreset.SelectedIndex)
            {
                case 0: // Preset: Black-White-Red
                    GamaSeismicColor.Colormap_BlackWhiteRed(); break;
                case 1: // Preset: Blue-White-Red
                    GamaSeismicColor.Colormap_BlueWhiteRed(); break;
                case 2: // Preset: Grayscale
                    GamaSeismicColor.Colormap_Grayscale(); break;
                case 3: // Preset: Rainbow
                    GamaSeismicColor.Colormap_Rainbow(); break;
                case 4: // Custom Colormap
                    ImageWriter.cScale = GamaColormapEditor.TempCMap; break;
            }

            GamaSeismicViewer.ShowSeismic(SEGYView.SegyView.GetAllTracesBitmap(SegyFrontPage.SEGYFile4Bmp));
            GamaSeismicColor.UpdateColormapBitmap();
            ColormapEditor.ActiveForm.Hide();
        }

        // event handler on FormClosing event, to prevent disposal of Colormap Editor window
        private void ColormapEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void panel6_VisibleChanged(object sender, EventArgs e)
        {
            if (panel6.Visible == true)
            { toolStripButton1.Enabled = false; }//panel7.Padding = new Padding(12, 0, 0, 12); }
            else
            { toolStripButton1.Enabled = true; } //panel7.Padding = new Padding(12, 0, 12, 12); }
        }

        private void panPickerSV_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            { GamaColormapEditor.UpdateSelectedColor(MousePosition); }
        }

        private void panPickerSV_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Left)
            { GamaColormapEditor.UpdateSelectedColor(MousePosition); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel6.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GamaColormapEditor.UpdateCurrentColor(picNewClr.BackColor);
            GamaColormapEditor.UpdateColorTableData(picNewClr.BackColor, GamaColormapEditor.IdxSelMarker);
            GamaColormapEditor.UpdateCustomColormap(picNewClr.BackColor, GamaColormapEditor.IdxSelMarker);
            GamaColormapEditor.UpdateBitmapColormap(GamaColormapEditor.TempCMap);
            comboPreset.SelectedIndex = comboPreset.Items.Count - 1;
            picColorScale.Update(); 
            panel6.Hide();
        }

        
        private void picColorScale_MouseMove(object sender, MouseEventArgs e)
        {
            if (GamaColormapEditor.ButCScale != null)
            {
                if (picColorScale.PointToClient(MousePosition).X % 2 == 0)
                {
                    pictureBox2.Left = picColorScale.PointToClient(MousePosition).X + picColorScale.Parent.Left + 1;
                   
                }
                else
                {
                    pictureBox2.Left = picColorScale.PointToClient(MousePosition).X + picColorScale.Parent.Left;
                }

                panAddMarkerIndicator.Left = pictureBox2.Left - 4;
                pictureBox2.Show();
                panAddMarkerIndicator.Show();
            }
        }

        private void picColorScale_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Hide();
            panAddMarkerIndicator.Hide();
        }

        private void picColorScale_DoubleClick(object sender, EventArgs e)
        {
            var ee = (MouseEventArgs)e;
            Bitmap temp = new Bitmap(picColorScale.BackgroundImage);
            if (ee.Button == MouseButtons.Left)
            {
                GamaColormapEditor.AddColorMarker(temp.GetPixel(picColorScale.PointToClient(MousePosition).X, 0),
                                                                (pictureBox2.Left - picColorScale.Parent.Left - 1) / 2);
            }

            temp.Dispose();
        }



    }
}
