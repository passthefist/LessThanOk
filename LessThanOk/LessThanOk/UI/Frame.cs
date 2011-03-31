/*---------------------------------------------------------------------------*\
 *                         LessThanOK Engine                                 *
 *                                                                           *
 *          Copyright (C) 2011-2012 by Robert Goetz, Anthony Lobono          *
 *                                                                           *
 *   authors:  Anthony LoBono (ajlobono@gmail.com)                           *
 *                                                                           *
\*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*\
 *                                License                                    *
 *                                                                           *
 * This library is free software; you can redistribute it and/or modify it   *
 * under the terms of the MIT Liscense.                                      *
 *                                                                           *
 * This library is distributed in the hope that it will be useful, but       *
 * WITHOUT ANY WARRANTY; without even the implied warranty of                *
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.                      *
 *                                                                           *
 * You should have received a copy of the MIT Liscense with this library, if *
 * not, visit http://www.opensource.org/licenses/mit-license.php.            *
 *                                                                           *
\*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*\
 *                            Class Overview                                 *
 *                                                                           *
 * This class is responsible containing Elements and other Frames            *
 *                                                                           *
 * See: Element.cs UIManager.cs                                              *
 *                                                                           *
\*---------------------------------------------------------------------------*/

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

        /// <summary>
        /// Default constructor.  Should never be used.
        /// </summary>
        protected Frame() { }
        /// <summary>
        /// Frames main contructor.  Creates a new instance of Frame.
        /// </summary>
        /// <param name="n_origin">Top left corner's global possition</param>
        /// <param name="n_size">Width and hight of the Frame</param>
        /// <param name="n_background">Background Image for the Frame.</param>
        public Frame(Vector2 n_origin, Vector2 n_size, Sprite_2D n_background)
        {
            frames = new List<Frame>();
            elements = new List<Element>();
            origin = n_origin;
            size = n_size;
            background = n_background;
        }
        /// <summary>
        /// Returns a list of all Elements contained in the Frame.
        /// </summary>
        /// <returns>List of all Elements contained in the Frame.</returns>
        public List<Element> getElements() { return elements; }
        /// <summary>
        /// Returns a List of all sub Frames in Frame.
        /// </summary>
        /// <returns>List of all sub Frames.</returns>
        public List<Frame> getFrames() { return frames; }
        /// <summary>
        /// Itterates through all visible frames and draws each element in the frame.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch used for drawing.</param>
        public virtual void draw(SpriteBatch spriteBatch)
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
        /// <summary>
        /// Itterates thought all visible Frames and updates the Elements in each
        /// Frame.
        /// </summary>
        /// <param name="gameTime">Current GameTime.</param>
        public virtual void update(GameTime gameTime)
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
        /// <summary>
        /// Add a sub Frame.  Sub Frames may not overlap and other frames in the
        /// current Frame.  This check has not been implimented yet.
        /// </summary>
        /// <param name="n_frame">New Frame to be added.</param>
        /// <param name="n_origin">
        /// Possition relitive to the current Frames top left Corner
        /// </param>
        public void addFrame(Frame n_frame, Vector2 n_origin)
        {
            n_frame.parent = this;
            ///Set subframe's new global possition 
            n_frame.origin = this.origin + n_origin;
            frames.Add(n_frame);
        }
        /// <summary>
        /// Add an Element to current Frame.
        /// </summary>
        /// <param name="n_origin">
        /// Element's possition relitive to the current frame's top left corner. 
        /// </param>
        /// <param name="element"></param>
        public void addElement(Vector2 n_origin, Element element)
        {
            element.origin = n_origin + this.origin;
            elements.Add(element);
        }
        /// <summary>
        /// Check if the giving coordinates are inside the frame.
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <returns>True if x and y are in the frame. False otherwise.</returns>
        public Boolean isInFrame(int x, int y)
        {
            if (x > origin.X && x < (origin.X + size.X))
                if (y > origin.Y && y < (origin.Y + size.Y))
                    return true;
            return false;
        }
        /// <summary>
        /// Get the Element undernieth the mouse. 
        /// </summary>
        /// <param name="mouseState">Current mouse state</param>
        /// <returns>Element underneith the mouse. Null if no Elemnt is under the mouse.</returns>
        public virtual Element findElement(MouseState mouseState)
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
                            if (curFrame is Frame_Game)
                                frameFound = false;
                            else
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
        /// <summary>
        /// Clear the current Frame.
        /// </summary>
        public void clear()
        {
            elements.Clear();
            frames.Clear();
        }
    }
}
