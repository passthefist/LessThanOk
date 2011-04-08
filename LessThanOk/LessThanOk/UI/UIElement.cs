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
        public Sprite image { get { return _image; } }
        public Vector2 Position { get { return _position; } }
        public RightClick RightClickEvent { get { return _rightClick; } }
        public LeftClick LeftClickEvent { get { return _leftClick; } }

        private String _name;
        private Sprite _image;
        private Vector2 _position;
        private Boolean _visible;
        private Boolean _mouseOver;
        private RightClick _rightClick;
        private LeftClick _leftClick;

        public UIElement(String name, Sprite image, Vector2 position)
        {
            _name = name;
            _image = image;
            _position = position;
            _rightClick = UIManager.RightClickEvent;
            _leftClick = UIManager.LeftClickEvent;
            _image.Position = _position;
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
