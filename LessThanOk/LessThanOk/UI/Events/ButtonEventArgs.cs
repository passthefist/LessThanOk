using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LessThanOk.UI.Frames.UIElements;

namespace LessThanOk.UI.Events
{
    public class ButtonEventArgs:EventArgs
    {
        private Button _element;

        public Button Element { get { return _element; } }

        public ButtonEventArgs() { }

        public ButtonEventArgs(Button element)
        {
            _element = element;
        }
    }
}
