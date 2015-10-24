using System.Windows.Forms;

namespace SegyView
{
    public class NoScrollPanel : Panel
    {
        protected override void OnMouseWheel(MouseEventArgs e)
	    {
		    var mouseEvent = (HandledMouseEventArgs)e;
		    mouseEvent.Handled = true;
	    }
    }
   
}
