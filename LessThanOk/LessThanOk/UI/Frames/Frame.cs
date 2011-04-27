using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LessThanOk.UI.Frames.UIElements;

namespace LessThanOk.UI
{
    public class Frame
    {
        public int Width { get { return _width; } }
        public int Height { get { return _height; } }
        public Dictionary<LessThanOk.UI.WindowDefinitions.BUTTON, Button> Elements { get { return _elements; } }

        protected int _width;
        protected int _height;
        protected Dictionary<LessThanOk.UI.WindowDefinitions.BUTTON, Button> _elements;

        public Frame() { }
        public Frame(int width, int height)
        {
            _width = width;
            _height = height;
            _elements = new Dictionary<LessThanOk.UI.WindowDefinitions.BUTTON, Button>();
        }
        public void addElement(Button element)
        {
            _elements.Add(element.Name, element);
        }
        public virtual void draw(SpriteBatch spriteBatch)
        {
            foreach (Button e in _elements.Values)
                e.draw(spriteBatch);
        }
        public virtual void update(GameTime gameTime)
        {
            foreach (Button e in _elements.Values)
                e.update(gameTime);
        }
        
        //Depricated
        public Button getElementAt(Vector2 mousePos)
        {
            float x = mousePos.X;
            float y = mousePos.Y;
            foreach (Button e in _elements.Values)
            {
                if (x >= e.X && x <= (e.X + e.Width))
                {
                    if (y >= e.Y && y <= (e.Y + e.Height))
                        return e;
                }
            }
            return null;
        }

        internal Button getElementAt(int x, int y)
        {
            foreach (Button b in _elements.Values)
            {
                if (x >= b.X && x <= (b.X + b.Image.Width))
                {
                    if (y >= b.Y && y <= (b.Y + b.Height))
                        return b;
                }
            }
            return null;
        }

    }
}
