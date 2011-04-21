using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LessThanOk.UI.Events.Args
{
    public class ButtonEventArgs:EventArgs
    {
        private UIElement _element;
        public UIElement Element { get { return _element; } }

        public ButtonEventArgs(UIElement element)
        {
            _element = element;
        }
    }
}
