/*---------------------------------------------------------------------------*\
*                         LessThanOK Engine                                  *
*                                                                            *
*          Copyright (C) 2011-2012 by Robert Goetz, Anthony Lobono           *
*                                                                            *
*          authors:  Anthony LoBono (ajlobono@gmail.com)                     *
*                                                                            *
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
 *                                                                           *
\*---------------------------------------------------------------------------*/
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
