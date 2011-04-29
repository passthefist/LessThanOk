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
using LessThanOk.GameData.GameObjects;
using LessThanOk.GameData.GameWorld;
using LessThanOk.GameData.GameObjects.Tiles;
using LessThanOk.GameData.GameObjects.Units;
using LessThanOk.UI.Frames.UIElements;
using LessThanOk.UI.Events;

namespace LessThanOk.UI
{
    public class Frame_Game : Frame
    {
        private TileMap _map;
        private Rectangle _view;

        public event EventHandler QuitEvent;
        public event EventHandler AddUnitEvent;

        public Frame_Game(int width, int height)
        {
            _width = width;
            _height = height;
            _view = new Rectangle(0, 0, 800, 400);
            _elements = new List<UIElement>();
        }
        public override void draw(SpriteBatch spriteBatch)
        {
            foreach (UIElement e in _elements)
                e.draw(spriteBatch);
        }
        public override void update(GameTime gameTime)
        {
            base.update(gameTime);
        }
        public override void addElement(UIElement element)
        {
            if (element is Button)
            {
                Button b = (Button)element;
                if (b.Name == "add")
                {
                    b.ButtonClickedEvent += new EventHandler<ButtonEventArgs>(TriggerAddEvent);
                }
                else if (b.Name == "quit")
                {
                    b.ButtonClickedEvent += new EventHandler<ButtonEventArgs>(TriggerQuitEvent);
                }
            }
            base.addElement(element);
        }

        void TriggerQuitEvent(object sender, ButtonEventArgs e)
        {
            if (QuitEvent != null)
                QuitEvent.Invoke(this, EventArgs.Empty);
        }

        void TriggerAddEvent(object sender, ButtonEventArgs e)
        {
            if(AddUnitEvent != null)
                AddUnitEvent.Invoke(sender, EventArgs.Empty);
        }
    }
}
