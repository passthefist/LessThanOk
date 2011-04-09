using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LessThanOk.Sprites;

namespace LessThanOk.UI
{
    public class UIElement
    {
        public String Name { get { return _name; } }
        public Sprite Image { get { return _image; } }
        public Vector2 Position { get { return _position; } }
        public RightClick RightClickEvent { get { return _rightClick; } }
        public LeftClick LeftClickEvent { get { return _leftClick; } }
        public virtual Vector2 Size { get { return _size; } }

        protected String _name;
        protected Sprite _image;
        protected Vector2 _position;
        protected Boolean _visible;
        protected Boolean _mouseOver;
        protected RightClick _rightClick;
        protected LeftClick _leftClick;
        protected Vector2 _size;

        public UIElement() { }

        public UIElement(String name, Sprite image, Vector2 position)
        {
            _name = name;
            _image = image;
            _position = position;
            _rightClick = UIManager.RightClickEvent;
            _leftClick = UIManager.LeftClickEvent;
            _image.Position = _position;
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
