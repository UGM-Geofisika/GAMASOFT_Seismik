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
                GamaSeismicColor.CreateColormap(GamaColormapEditor.editCMap, Color.FromArgb(0, 0, 0), 0, Color.FromArgb(255, 255, 255), 127, Color.FromArgb(255, 0, 0), 255);

                GamaColormapEditor.LoadColormapTable(new Color[3] { Color.FromArgb(0, 0, 0), Color.FromArgb(255, 255, 255), Color.FromArgb(255, 0, 0) },
                                                     new int[3] { 0, 127, 255 });

                GamaColormapEditor.UpdateBitmapColormap(GamaColormapEditor.editCMap);
                break;

                case 1: // Preset: Blue-White-Red
                GamaSeismicColor.CreateColormap(GamaColormapEditor.editCMap, Color.FromArgb(0, 0, 255), 0, Color.FromArgb(255, 255, 255), 127, Color.FromArgb(255, 0, 0), 255);

                GamaColormapEditor.LoadColormapTable(new Color[3] { Color.FromArgb(0, 0, 255), Color.FromArgb(255, 255, 255), Color.FromArgb(255, 0, 0) },
                                                     new int[3] { 0, 127, 255 });

                GamaColormapEditor.UpdateBitmapColormap(GamaColormapEditor.editCMap);
                break;

                case 2: // Preset: Grayscale
                GamaSeismicColor.CreateColormap(GamaColormapEditor.editCMap, Color.FromArgb(0, 0, 0), 0, Color.FromArgb(255, 255, 255), 255);

                GamaColormapEditor.LoadColormapTable(new Color[2] {Color.FromArgb(0, 0, 0), Color.FromArgb(255, 255, 255)},
                                                 new int[2] {0, 255});

                GamaColormapEditor.UpdateBitmapColormap(GamaColormapEditor.editCMap);
                break;

                case 3: // Preset: Rainbow
                GamaSeismicColor.CreateColormap(GamaColormapEditor.editCMap, 
                new Color[6] {Color.Red, Color.DarkOrange, Color.Yellow, Color.Lime, Color.Blue, Color.DarkViolet}, 
                new int[6]   {0,         51,               102,          153,        204,        255             });

                GamaColormapEditor.LoadColormapTable(new Color[6] { Color.Red, Color.DarkOrange, Color.Yellow, Color.Lime, Color.Blue, Color.DarkViolet },
                                                 new int[6] { 0, 51, 102, 153, 204, 255 });

                GamaColormapEditor.UpdateBitmapColormap(GamaColormapEditor.editCMap);
                break;
            }
        }

        private void panPickerHue_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void panPickerHue_Click(object sender, EventArgs e)
        {
            var hue = (int)Math.Round(((double)panPickerHue.PointToClient(MousePosition).X/255)*359);
            Debug.WriteLine(hue);
            GamaColormapEditor.LoadSaturationValueFromHue(hue);
        }

        private void toolStripButton1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        

    }
}
