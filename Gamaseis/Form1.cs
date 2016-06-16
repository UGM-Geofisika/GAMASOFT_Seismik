using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Unplugged.Segy;

namespace Gamaseis
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void plotTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
           var test = new WigglePlot{MdiParent = this};
            test.Show();
        }

        private void addFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = addFileDialog.ShowDialog();
            if (dlg != DialogResult.OK) return;
            var filename = addFileDialog.FileName;
            var sgyReader = new SegyReader();
            var sgyFile = sgyReader.Read(filename);
            
            var sgyItem = new SegyContainer() {FileName = addFileDialog.SafeFileName, Data = sgyFile};
            listBoxFiles.Items.Add(sgyItem);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listBoxFiles.DisplayMember = "FileName";
            listBoxFiles.ValueMember = "Data";
        }
    }
}
