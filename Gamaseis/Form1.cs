using System;
using System.Collections.Generic;
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
            // get the selected listbox items
            var segy = listBoxFiles.SelectedItem as SegyContainer;
            if (segy == null)
            {
                Debug.WriteLine("Null detected, returning..");
                return;
            }
            // grab the requested shots
            var myShots = new ShotGather();
            var myTraces = segy.Data.Traces.Where(trace => trace.Header.Fldr == 1).ToList();
            myShots.InitializeTraces(myTraces);

            var gathers = new List<ShotGather> {myShots};
            var wigglePlot = new WigglePlot(gathers) {};
            wigglePlot.Plot();
            wigglePlot.Show();
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
