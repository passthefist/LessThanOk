using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LessThanOk.UI.Events.Args
{
    public class ButtonEventArgs:EventArgs
    {
        private Button _element;
        private STATE _state;

        public enum STATE
        {
            UP,
            DOWN
        }

        public STATE State { get { return _state; } }
        public Button Element { get { return _element; } }

        public ButtonEventArgs() { }
        public ButtonEventArgs(Button element)
        {
            _element = element;
            _state = STATE.DOWN;
        }
        public ButtonEventArgs(Button element, STATE state)
        {
            _element = element;
            _state = state;
        }
    }
}
