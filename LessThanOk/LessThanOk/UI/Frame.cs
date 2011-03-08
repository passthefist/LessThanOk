using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using LessThanOk.Sprites;


namespace LessThanOk.UI
{
    class Frame
    {
        public Boolean visible { get; set; }
        public Frame parent { get; set; }

        protected Vector2 size;
        protected Vector2 origin { get; set; }
        protected List<Frame> frames;
        protected List<Element> elements;
        protected Sprite_2D background;

        protected Frame() { }
        public Frame(Vector2 n_origin, Vector2 n_size, Sprite_2D n_background)
        {
            frames = new List<Frame>();
            elements = new List<Element>();
            origin = n_origin;
            size = n_size;
            background = n_background;
        }

        public List<Element> getElements() { return elements;  }
        public List<Frame> getFrames() { return frames; }
        public void draw(SpriteBatch spriteBatch)
        {
            foreach(Frame f in frames)
            {
                if (f.visible)
                    f.draw(spriteBatch);
            }
            foreach(Element e in elements)
            {
                if (e.visible)
                {
                    e.draw(spriteBatch);
                }
            }
        }

        public void update(GameTime gameTime)
        {
            foreach (Frame f in frames)
            {
                if (f.visible)
                    f.update(gameTime);
            }
            foreach (Element e in elements)
            {
                if (e.visible)
                {
                    e.update(gameTime);
                }
            }
        }

        public void addFrame(Frame n_frame, Vector2 n_origin)
        {
            n_frame.parent = this;
            n_frame.origin = this.origin + n_origin;
            frames.Add(n_frame);
        }

        public void addElement(Vector2 n_origin, Element element)
        {
            element.origin = n_origin + this.origin;
            elements.Add(element);
        }

        public Boolean isInFrame(int x, int y)
        {
            if (x > origin.X && x < (origin.X + size.X))
                if (y > origin.Y && y < (origin.Y + size.Y))
                    return true;
            return false;
        }

        public Element findElement(MouseState mouseState)
        {
            Element retval = null;
            List<Frame> children;
            Frame curFrame = this;
            Boolean frameFound = false;
            while (retval == null)
            {
                foreach (Element e in curFrame.getElements())
                    if (e.isOver(mouseState.X, mouseState.Y) && e.visible)
                        return e;

                children = curFrame.getFrames();
                if (children != null)
                {
                    frameFound = false;
                    foreach (Frame f in children)
                    {
                        if (f.visible && f.isInFrame(mouseState.X, mouseState.Y) &&
                            !frameFound)
                        {
                            curFrame = f;
                            frameFound = true;
                        }
                    }
                    if (!frameFound)
                        return null;
                }
                else
                    return null;
            }
            return null;
        }
    }
}
