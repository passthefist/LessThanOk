using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LessThanOk.UI
{
    public class Frame
    {
        public int Width { get { return _width; } }
        public int Height { get { return _height; } }
        public Dictionary<Vector2, UIElement> Elements { get { return _elements; } }

        private int _width;
        private int _height;
        private Dictionary<Vector2, UIElement> _elements;

        public Frame(int width, int height)
        {
            _width = width;
            _height = height;
            _elements = new Dictionary<Vector2, UIElement>();
        }
        public void addElement(UIElement element)
        {
            _elements.Add(element.Position, element);
        }
        public void draw(SpriteBatch spriteBatch)
        {
            foreach (UIElement e in _elements.Values)
                e.draw(spriteBatch);
        }
        public void update(GameTime gameTime)
        {
            foreach (UIElement e in _elements.Values)
                e.update(gameTime);
        }
        public UIElement getElementAt(Vector2 mousePos)
        {
            float x = mousePos.X;
            float y = mousePos.Y;
            foreach (UIElement e in _elements.Values)
            {
                if (x >= e.Position.X && x <= (e.Position.X + e.Image.Size.X))
                {
                    if (y >= e.Position.Y && y <= (e.Position.Y + e.Image.Size.Y))
                        return e;
                }
            }
            return null;
        }
    }
}
