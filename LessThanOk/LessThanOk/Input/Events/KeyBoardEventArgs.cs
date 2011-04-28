using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace LessThanOk.Input.Events
{
    public class KeyBoardEventArgs:EventArgs
    {
        private Keys _key;

        public Keys Key { get { return _key; } }

        public KeyBoardEventArgs(Keys key)
        {
            _key = key;
        }
    }
}
