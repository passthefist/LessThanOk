using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace LessThanOk.Input.Events
{
    public class MouseEventArgs : EventArgs
    {
        private MouseState _mouseState;
        public MouseState MouseState { get { return _mouseState; } }

        public MouseEventArgs(MouseState state)
        {
            _mouseState = state;
        }
    }
}
