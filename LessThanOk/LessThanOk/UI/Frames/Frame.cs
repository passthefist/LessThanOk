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
        public List<UIElement> Elements { get { return _elements; } }

        protected int _width;
        protected int _height;
        protected List<UIElement> _elements;

        public Frame() { }
        public Frame(int width, int height)
        {
            _width = width;
            _height = height;
            _elements = new List<UIElement>();
        }
        public virtual void addElement(UIElement element)
        {
            _elements.Add(element);
        }
        public virtual void draw(SpriteBatch spriteBatch)
        {
            foreach (UIElement e in _elements)
                e.draw(spriteBatch);
        }
        public virtual void update(GameTime gameTime)
        {
            foreach (UIElement e in _elements)
                e.update(gameTime);
        }

    }
}
