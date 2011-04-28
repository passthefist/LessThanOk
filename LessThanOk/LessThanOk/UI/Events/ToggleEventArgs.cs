using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LessThanOk.UI.Frames.UIElements;

namespace LessThanOk.UI.Events
{
    class ToggleEventArgs : EventArgs
    {

        public STATE State { get { return _state; } }
        public ToggleButton Element { get { return _element; } }

        private ToggleButton _element;
        private STATE _state;

        public enum STATE
        {
            UP,
            DOWN
        }

        public ToggleEventArgs(ToggleButton element, STATE state)
        {
            _element = element;
            _state = state;
        }
    }
}
