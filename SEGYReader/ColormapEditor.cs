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
        }

        private void ColormapEditor_Load(object sender, EventArgs e)
        {
            GamaColormapEditor.InitializeColormapEditor();
        }

        private void comboPreset_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboPreset.SelectedIndex)
            {   
                case 0: // Preset: Black-White-Red
                    Color[] newColor = new Color[3] { Color.FromArgb(0, 0, 0), Color.FromArgb(255, 255, 255), Color.FromArgb(255, 0, 0) };
                    int[] newIndex = new int[3] { 0, 127, 255 };

                    GamaSeismicColor.CreateColormap(GamaColormapEditor.editCMap, newColor, newIndex);
                    GamaColormapEditor.LoadColormapTable(newColor, newIndex);
                    GamaColormapEditor.UpdateBitmapColormap(GamaColormapEditor.editCMap);
                    break;

                case 1: // Preset: Blue-White-Red
                    newColor = new Color[3] { Color.FromArgb(0, 0, 255), Color.FromArgb(255, 255, 255), Color.FromArgb(255, 0, 0) };
                    newIndex = new int[3] { 0, 127, 255 };

                    GamaSeismicColor.CreateColormap(GamaColormapEditor.editCMap, newColor, newIndex);
                    GamaColormapEditor.LoadColormapTable(newColor, newIndex);
                    GamaColormapEditor.UpdateBitmapColormap(GamaColormapEditor.editCMap);
                    break;

                case 2: // Preset: Grayscale
                    newColor = new Color[2] { Color.FromArgb(0, 0, 0), Color.FromArgb(255, 255, 255) };
                    newIndex = new int[2] { 0, 255 };

                    GamaSeismicColor.CreateColormap(GamaColormapEditor.editCMap, newColor, newIndex);
                    GamaColormapEditor.LoadColormapTable(newColor, newIndex);
                    GamaColormapEditor.UpdateBitmapColormap(GamaColormapEditor.editCMap);
                    break;

                case 3: // Preset: Rainbow
                    newColor = new Color[6] { Color.Red, Color.DarkOrange, Color.Yellow, Color.Lime, Color.Blue, Color.DarkViolet };
                    newIndex = new int[6] { 0, 51, 102, 153, 204, 255 };

                    GamaSeismicColor.CreateColormap(GamaColormapEditor.editCMap, newColor, newIndex);
                    GamaColormapEditor.LoadColormapTable(newColor, newIndex);
                    GamaColormapEditor.UpdateBitmapColormap(GamaColormapEditor.editCMap);
                    break;
            }
        }

        private void panPickerHue_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Bitmap hueImage = new Bitmap(panPickerHue.BackgroundImage);
                int mouseX = panPickerHue.PointToClient(MousePosition).X;
                if (mouseX < 0) mouseX = 0;
                if (mouseX > 255) mouseX = 255;

                GamaColormapEditor.LoadSaturationValueFromHue(hueImage.GetPixel(mouseX, 0));
                hueImage.Dispose();
            }
        }

        private void panPickerHue_Click(object sender, EventArgs e)
        {
            Bitmap hueImage = new Bitmap(panPickerHue.BackgroundImage);
            int mouseX = panPickerHue.PointToClient(MousePosition).X;
            if (mouseX < 0) mouseX = 0;
            if (mouseX > 255) mouseX = 255;

            GamaColormapEditor.LoadSaturationValueFromHue(hueImage.GetPixel(mouseX, 0));
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
                    ImageWriter.cScale = GamaColormapEditor.tempCMap; break;
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

        private void panPickerSV_Click(object sender, EventArgs e)
        {
            //if (MouseButtons == MouseButtons.Left)
            //{
                if (panel6.Visible == true)
                { panel6.Hide(); }
                else
                { panel6.Show(); }
            //}
        }

        private void panel6_VisibleChanged(object sender, EventArgs e)
        {
            if (panel6.Visible == true)
            { panel7.Padding = new Padding(12, 0, 0, 12); }
            else
            { panel7.Padding = new Padding(12, 0, 12, 12); }
        }



    }
}
