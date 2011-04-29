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
using Microsoft.Xna.Framework.Graphics;
using LessThanOk.Sprites;
using LessThanOk.UI.Events;
using LessThanOk.Input.Events;
using Microsoft.Xna.Framework;
using LessThanOk.Input;
using Microsoft.Xna.Framework.Input;

namespace LessThanOk.UI.Frames.UIElements
{
    class ToggleButton : UIElement
    {
        public ToggleEventArgs.STATE State { get { return _state; } }
        public event EventHandler<ToggleEventArgs> ToggleEvent;
        public Sprite Image { get { return _image; } }

        public override int Height { get { return _image.Height; } }
        public override int Width { get { return _image.Width; } }

        protected Sprite _image;
        private ToggleEventArgs.STATE _state;
        private Color _colorS1;
        private Color _colorS2;

        public ToggleButton(String name, Sprite image, int x, int y, Color c1, Color c2)
        {
            _colorS1 = c1;
            _colorS2 = c2;
            _image = image;
            _posx = x;
            _posy = y;
            _name = name;
            InputManager.LeftMouseUpEvent += new EventHandler<MouseEventArgs>(LeftMouseUpEventHandler);
        }
        
        public override void draw(SpriteBatch batch)
        {
            if (_state == ToggleEventArgs.STATE.DOWN)
                Image.Color = _colorS2;
            if (_state == ToggleEventArgs.STATE.UP)
                Image.Color = _colorS1;
            Image.Draw(batch, _posx, _posy);
        }

        public override void update(GameTime gameTime)
        {
            int x = Mouse.GetState().X;
            int y = Mouse.GetState().Y;
            if (x > _posx && x < (_posx + Width))
            {
                if (y > _posy && y < (_posy + Height))
                {
                    _mouseOver = true;
                }
            }
            else
                _mouseOver = false;

        }

        public void LeftMouseUpEventHandler(object sender, MouseEventArgs args)
        {
            if(_mouseOver && ToggleEvent!= null) 
            {
                if (_state == ToggleEventArgs.STATE.DOWN)
                {
                    _state = ToggleEventArgs.STATE.UP;
                    ToggleEvent.Invoke(this, new ToggleEventArgs(this, _state));
                }
                else
                {
                    _state = ToggleEventArgs.STATE.DOWN;
                    ToggleEvent.Invoke(this, new ToggleEventArgs(this, _state));
                }
            }

        }
 
    }
}
