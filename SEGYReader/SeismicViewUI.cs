using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace SegyView
{
    partial class SegyFrontPage
    {
        /// THIS CLASS SHOULD ONLY CONTAIN DEFINITION/FUNCTION/EVENT RELATED TO SEISMIC VIEW UI
        /// DONT PUT ANY BUSINESS LOGIC HERE
        private void picBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            GamaSeismicViewer.ZoomFactor = GamaSeismicViewer.ZoomFactor + 10*(e.Delta/120);
            if (GamaSeismicViewer.ZoomFactor < 10)
                GamaSeismicViewer.ZoomFactor = 10;
            if (GamaSeismicViewer.ZoomFactor > 500)
                GamaSeismicViewer.ZoomFactor = 500;

            GamaSeismicViewer.PicStart = picBox1.PointToClient(MousePosition);
            GamaSeismicViewer.Image_MouseCenteredZoom((int)GamaSeismicViewer.ZoomFactor, MousePosition);
            GamaSeismicViewer.Image_Axis_Update();

            lblZoom.Text = String.Concat(GamaSeismicViewer.ZoomFactor.ToString(), " %"); lblZoom.Update();
        }

        private void panelX_MouseDown(object sender, MouseEventArgs e)
        {
            if (MouseButtons == MouseButtons.Left)
            {
                GamaSeismicViewer.FScaleX = true;
                GamaSeismicViewer.ScaleMouse0 = panelX.PointToClient(MousePosition);
            }
        }

        private void panelX_MouseMove(object sender, MouseEventArgs e)
        {
            if (picBox1.Image == null) return;
            
            GamaSeismicViewer.Image_Axis_StretchShrink(MousePosition);
            GamaSeismicViewer.Image_Axis_Update();

            lblXAxisScale.Text = String.Concat(Math.Round((GamaSeismicViewer.ImgOriginalSize[0] / (double)picBox1.Image.Width) * 100), " %"); lblXAxisScale.Update();
        }

        private void panelX_MouseUp(object sender, MouseEventArgs e)
        {
            GamaSeismicViewer.FScaleX = false;
        }

        private void panelY_MouseDown(object sender, MouseEventArgs e)
        {
            if (MouseButtons == MouseButtons.Left)
            {
                GamaSeismicViewer.FScaleY = true;
                GamaSeismicViewer.ScaleMouse0 = panelY.PointToClient(MousePosition);
            }
        }

        private void panelY_MouseMove(object sender, MouseEventArgs e)
        {
            if (picBox1.Image == null) return;

            GamaSeismicViewer.Image_Axis_StretchShrink(MousePosition);
            GamaSeismicViewer.Image_Axis_Update();

            lblYAxisScale.Text = String.Concat(Math.Round((GamaSeismicViewer.ImgOriginalSize[1] / (double)picBox1.Image.Height) * 100), " %"); lblYAxisScale.Update();
        }

        private void panelY_MouseUp(object sender, MouseEventArgs e)
        {
            GamaSeismicViewer.FScaleY = false;
        }

        private void SEGYDisplay_Resize(object sender, EventArgs e)
        {
            GamaSeismicViewer.Image_Axis_Update();
        }

        private void butSeismicZoom_Click(object sender, EventArgs e)
        {
            GamaSeismicViewer.Image_ZoomByValue(100);
            GamaSeismicViewer.Image_Axis_Update();

            lblZoom.Text = String.Concat(GamaSeismicViewer.ZoomFactor, " %"); lblZoom.Update();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GamaSeismicViewer.Image_Axis_StretchShrink(picBox1.Image.Width, GamaSeismicViewer.ImgOriginalSize[1]);
            GamaSeismicViewer.Image_Axis_Update();

            lblXAxisScale.Text = String.Concat(Math.Round((GamaSeismicViewer.ImgOriginalSize[0]/(double)picBox1.Image.Width)*100), " %"); lblXAxisScale.Update();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GamaSeismicViewer.Image_Axis_StretchShrink(GamaSeismicViewer.ImgOriginalSize[0], picBox1.Image.Height);
            GamaSeismicViewer.Image_Axis_Update();

            lblYAxisScale.Text = String.Concat(Math.Round((GamaSeismicViewer.ImgOriginalSize[1] / (double)picBox1.Image.Height) * 100), " %"); lblYAxisScale.Update();
        }

        private void XAxisFitWidth_Click(object sender, EventArgs e)
        {
            GamaSeismicViewer.Image_Axis_StretchShrink(true, false);
            GamaSeismicViewer.Image_Axis_Update();

            lblXAxisScale.Text = String.Concat(Math.Round((GamaSeismicViewer.ImgOriginalSize[0] / (double)picBox1.Image.Width) * 100), " %"); lblXAxisScale.Update();
        }

        private void YAxisFitHeight_Click(object sender, EventArgs e)
        {
            GamaSeismicViewer.Image_Axis_StretchShrink(false, true);
            GamaSeismicViewer.Image_Axis_Update();

            lblYAxisScale.Text = String.Concat(Math.Round((GamaSeismicViewer.ImgOriginalSize[1] / (double)picBox1.Image.Height) * 100), " %"); lblYAxisScale.Update();
        }

        private void butZoomFit_Click(object sender, EventArgs e)
        {
            GamaSeismicViewer.Image_ZoomToScreen();
            GamaSeismicViewer.Image_Axis_Update();

            lblZoom.Text = String.Concat(GamaSeismicViewer.ZoomFactor, " %"); lblZoom.Update();
        }

        private void butFitAllToScreen_Click(object sender, EventArgs e)
        {
            GamaSeismicViewer.Image_Axis_StretchShrink(true, true);
            GamaSeismicViewer.Image_Axis_Update();

            lblXAxisScale.Text = String.Concat(Math.Round((GamaSeismicViewer.ImgOriginalSize[0] / (double)picBox1.Image.Width) * 100), " %"); lblXAxisScale.Update();
            lblYAxisScale.Text = String.Concat(Math.Round((GamaSeismicViewer.ImgOriginalSize[1] / (double)picBox1.Image.Height) * 100), " %"); lblYAxisScale.Update();
        }

        private void butColormapEdit_Click(object sender, EventArgs e)
        {
            FrmClrEdit.Show();
        }
    }
}
