using System;
using System.Windows.Forms;
using Unplugged.Segy;

namespace Gamaseis
{
    partial class FrontPage
    {
        /// THIS CLASS SHOULD ONLY CONTAIN DEFINITION/FUNCTION/EVENT RELATED TO SEISMIC VIEW UI
        /// DONT PUT ANY BUSINESS LOGIC HERE
        private void picBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            SeismicViewer.ZoomFactor = SeismicViewer.ZoomFactor + 10*(e.Delta/120);
            if (SeismicViewer.ZoomFactor < 10)
                SeismicViewer.ZoomFactor = 10;
            if (SeismicViewer.ZoomFactor > 500)
                SeismicViewer.ZoomFactor = 500;

            SeismicViewer.PicStart = picBox1.PointToClient(MousePosition);
            SeismicViewer.Image_MouseCenteredZoom((int)SeismicViewer.ZoomFactor, MousePosition);
            SeismicViewer.Image_Axis_Update();

            lblZoom.Text = String.Concat(SeismicViewer.ZoomFactor.ToString(), " %"); lblZoom.Update();
        }

        private void panelX_MouseDown(object sender, MouseEventArgs e)
        {
            if (MouseButtons == MouseButtons.Left)
            {
                SeismicViewer.FScaleX = true;
                SeismicViewer.ScaleMouse0 = panelX.PointToClient(MousePosition);
            }
        }

        private void panelX_MouseMove(object sender, MouseEventArgs e)
        {
            if (picBox1.Image == null) return;
            
            SeismicViewer.Image_Axis_StretchShrink(MousePosition);
            SeismicViewer.Image_Axis_Update();

            lblXAxisScale.Text = String.Concat(Math.Round((SeismicViewer.ImgOriginalSize[0] / (double)picBox1.Image.Width) * 100), " %"); lblXAxisScale.Update();
        }

        private void panelX_MouseUp(object sender, MouseEventArgs e)
        {
            SeismicViewer.FScaleX = false;
        }

        private void panelY_MouseDown(object sender, MouseEventArgs e)
        {
            if (MouseButtons == MouseButtons.Left)
            {
                SeismicViewer.FScaleY = true;
                SeismicViewer.ScaleMouse0 = panelY.PointToClient(MousePosition);
            }
        }

        private void panelY_MouseMove(object sender, MouseEventArgs e)
        {
            if (picBox1.Image == null) return;

            SeismicViewer.Image_Axis_StretchShrink(MousePosition);
            SeismicViewer.Image_Axis_Update();

            lblYAxisScale.Text = String.Concat(Math.Round((SeismicViewer.ImgOriginalSize[1] / (double)picBox1.Image.Height) * 100), " %"); lblYAxisScale.Update();
        }

        private void panelY_MouseUp(object sender, MouseEventArgs e)
        {
            SeismicViewer.FScaleY = false;
        }

        private void SEGYDisplay_Resize(object sender, EventArgs e)
        {
            SeismicViewer.Image_Axis_Update();
        }

        private void butSeismicZoom_Click(object sender, EventArgs e)
        {
            SeismicViewer.Image_ZoomByValue(100);
            SeismicViewer.Image_Axis_Update();

            lblZoom.Text = String.Concat(SeismicViewer.ZoomFactor, " %"); lblZoom.Update();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SeismicViewer.Image_Axis_StretchShrink(picBox1.Image.Width, SeismicViewer.ImgOriginalSize[1]);
            SeismicViewer.Image_Axis_Update();

            lblXAxisScale.Text = String.Concat(Math.Round((SeismicViewer.ImgOriginalSize[0]/(double)picBox1.Image.Width)*100), " %"); lblXAxisScale.Update();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SeismicViewer.Image_Axis_StretchShrink(SeismicViewer.ImgOriginalSize[0], picBox1.Image.Height);
            SeismicViewer.Image_Axis_Update();

            lblYAxisScale.Text = String.Concat(Math.Round((SeismicViewer.ImgOriginalSize[1] / (double)picBox1.Image.Height) * 100), " %"); lblYAxisScale.Update();
        }

        private void XAxisFitWidth_Click(object sender, EventArgs e)
        {
            SeismicViewer.Image_Axis_StretchShrink(true, false);
            SeismicViewer.Image_Axis_Update();

            lblXAxisScale.Text = String.Concat(Math.Round((SeismicViewer.ImgOriginalSize[0] / (double)picBox1.Image.Width) * 100), " %"); lblXAxisScale.Update();
        }

        private void YAxisFitHeight_Click(object sender, EventArgs e)
        {
            SeismicViewer.Image_Axis_StretchShrink(false, true);
            SeismicViewer.Image_Axis_Update();

            lblYAxisScale.Text = String.Concat(Math.Round((SeismicViewer.ImgOriginalSize[1] / (double)picBox1.Image.Height) * 100), " %"); lblYAxisScale.Update();
        }

        private void butZoomFit_Click(object sender, EventArgs e)
        {
            SeismicViewer.Image_ZoomToScreen();
            SeismicViewer.Image_Axis_Update();

            lblZoom.Text = String.Concat(SeismicViewer.ZoomFactor, " %"); lblZoom.Update();
        }

        private void butFitAllToScreen_Click(object sender, EventArgs e)
        {
            SeismicViewer.Image_Axis_StretchShrink(true, true);
            SeismicViewer.Image_Axis_Update();

            lblXAxisScale.Text = String.Concat(Math.Round((SeismicViewer.ImgOriginalSize[0] / (double)picBox1.Image.Width) * 100), " %"); lblXAxisScale.Update();
            lblYAxisScale.Text = String.Concat(Math.Round((SeismicViewer.ImgOriginalSize[1] / (double)picBox1.Image.Height) * 100), " %"); lblYAxisScale.Update();
        }

        private void butColormapEdit_Click(object sender, EventArgs e)
        {
            FrmClrEdit.Show();
        }

        #region trackLowerGain
        private void trackLowerGain_MouseUp(object sender, MouseEventArgs e)
        {
            ImageWriter.gainRange[0] = ImageWriter.gainRange[0] + (float)trackLowerGain.Value / 10.0f;
            trackLowerGain.Value = 0;

            SeismicViewer.ShowSeismic(SeismicFileHandler.GetAllTracesBitmap(SEGYFile4Bmp));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // set gain value to default
            ImageWriter.SetGainRangeToDefault(true, false);
            SeismicViewer.ShowSeismic(SeismicFileHandler.GetAllTracesBitmap(SEGYFile4Bmp));
        }
        #endregion

        #region trackUpperGain
        private void trackUpperGain_MouseUp(object sender, MouseEventArgs e)
        {
            ImageWriter.gainRange[1] = ImageWriter.gainRange[1] + (float)trackUpperGain.Value / 10.0f;
            trackUpperGain.Value = 0;

            SeismicViewer.ShowSeismic(SeismicFileHandler.GetAllTracesBitmap(SEGYFile4Bmp));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // set gain value to default
            ImageWriter.SetGainRangeToDefault(false, true);
            SeismicViewer.ShowSeismic(SeismicFileHandler.GetAllTracesBitmap(SEGYFile4Bmp));
        }
        #endregion

    }
}
