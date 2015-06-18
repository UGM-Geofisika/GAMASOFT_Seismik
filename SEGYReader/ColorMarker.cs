using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Text;

namespace SegyView
{
    public class ColorMarker : Panel
    {
        private int IDNum = 0;

        [Category("Color Marker ID"), Description("")]
        public int ID
        {
            get { return IDNum; }
            set { IDNum = value; }
        }
    }
}
