using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LessThanOk.Sprites;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LessThanOk.UI.Frames.UIElements
{
    public class UIElement
    {
        public virtual int X { get { return _posx; } }
        public virtual int Y { get { return _posy; } }
        public virtual int Width { get { return 0; } }
        public virtual int Height { get { return 0; } }
        public String Name { get { return _name; } }

        protected String _name;
        protected int _posx;
        protected int _posy;
        protected Boolean _visible;
        protected Boolean _mouseOver;

        public UIElement() { }
        public virtual void draw(SpriteBatch batch) { }
        public virtual void update(GameTime time) { }
    }
}
