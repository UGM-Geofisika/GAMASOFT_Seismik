﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ILNumerics;
using ILNumerics.Drawing;
using ILNumerics.Drawing.Plotting;

namespace Gamaseis
{
    public partial class GatherPlot : Form, IPLottingForm
    {
        private readonly PlotType _type;
        public IList<ShotGather> Shots { get; }
        public GatherPlot(IList<ShotGather> shots, PlotType type)
        {
            InitializeComponent();
            Shots = shots;
            _type = type;
        }
        public void Plot()
        {
            var cube = PlotFactory.BuildDefaultCube();
            cube.BuildSeismicPlot(Shots[0],_type);
            cube.SetYAxisOnTop();
            ilPanel1.Scene.Add(cube);
        }
        public void Update(IEnumerable<ShotGather> shots)
        {
            throw new NotImplementedException();
        }
    }
}