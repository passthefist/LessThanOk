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

        private int width;
        private int height;
        private Vector2 origin;
        private List<Frame> frames;
        private List<Element> elements;
        private Sprite_2D background;

        public Frame(Vector2 n_origin, int n_width, int n_height,
                        Sprite_2D n_background, Boolean n_visible)
        {
            frames = new List<Frame>();
            elements = new List<Element>();
            origin = n_origin;
            width = n_width;
            height = n_height;
            background = n_background;
            visible = n_visible;
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

        public void addFrame(Frame n_frame)
        {
            n_frame.parent = this;
            frames.Add(n_frame);
        }

        public void addElement(Vector2 n_origin, Element element)
        {
            element.origin = n_origin + this.origin;
            elements.Add(element);
        }

        public Boolean isInFrame(int x, int y)
        {
            if (x > origin.X && x < (origin.X + width))
                if (y > origin.Y && y < (origin.Y + height))
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
