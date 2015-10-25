using System.ComponentModel;
using System.Windows.Forms;

namespace Gamaseis
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
