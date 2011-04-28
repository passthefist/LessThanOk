using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LessThanOk.Sprites;

namespace LessThanOk.UI.Frames.UIElements
{
    class AddButton : Button
    {
        public UInt16 Parent { get { return _parent; } }
        public UInt16 Type { get { return _type; } }

        private UInt16 _type;
        private UInt16 _parent;

        public AddButton(String name, Sprite image, int x, int y, UInt16 parent, UInt16 type)
            : base("add", image, x, y)
        {
            _type = type;
            _parent = parent;
        }
    }
}
