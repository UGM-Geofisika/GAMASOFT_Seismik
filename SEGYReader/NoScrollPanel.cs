using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms; 

namespace SegyView
{
    internal class NoScrollPanel : Panel
    {
        protected override void OnMouseWheel(MouseEventArgs e)
	    {
		    HandledMouseEventArgs mouseEvent = (HandledMouseEventArgs)e;
		    mouseEvent.Handled = true;
	    }
    }
   
}
