using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LessThanOk.Sprites;
using LessThanOk.UI.Events;

namespace LessThanOk.UI
{
    public class UIElement
    {
        public String Name { get { return _name; } }
        public Sprite Image { get { return _image; } }
        public int X { get { return _posx; } }
        public int Y { get { return _posy; } }
        public virtual Vector2 Size { get { return _size; } }
        public Boolean Hover { get; set; }

        protected String _name;
        protected Sprite _image;
        protected int _posx;
        protected int _posy;
        protected Boolean _visible;
        protected Boolean _mouseOver;
        protected Vector2 _size;

        public UIElement() { }

        public UIElement(String name, Sprite image, int x, int y)
        {
            _name = name;
            _image = image;
            _posx = x;
            _posy = y;
            _image.Position = new Vector2(x, y);
            _size = _image.Size;
        }

        public virtual void update(GameTime gameTime)
        {
            
        }
        public virtual void draw(SpriteBatch spriteBatch)
        {
            _image.draw(spriteBatch);
        }
    }
}
