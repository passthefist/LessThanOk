﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LessThanOk.Sprites;
using LessThanOk.UI.Events;
using LessThanOk.UI;
using LessThanOk.Input.Events;
using LessThanOk.Input;

namespace LessThanOk.UI.Frames.UIElements
{
    public class Button : UIElement
    {
        public Sprite Image { get { return _image; } }
        public override int Height { get { return _image.Height; } }
        public override int Width { get { return _image.Width; } }

        protected Sprite _image;
        public event EventHandler<ButtonEventArgs> ButtonClickedEvent;

        public Button() { }

        public Button(String name, Sprite image, int x, int y)
        {
            _image = image;
            _posx = x;
            _posy = y;
            _name = name;
            InputManager.LeftMouseUpEvent += new EventHandler<MouseEventArgs>(LeftMouseUpEventHandler);
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
        public override void draw(SpriteBatch spriteBatch)
        {
            if (_mouseOver)
                Image.Color = Color.BlueViolet;
            else
                Image.Color = Color.White;
            
            Image.Draw(spriteBatch, _posx, _posy); 
        }
        
        protected void LeftMouseUpEventHandler(object sender, MouseEventArgs args)
        {
            if(_mouseOver && ButtonClickedEvent != null)
                ButtonClickedEvent.Invoke(this, new ButtonEventArgs(this));
        }
        protected void InvokeClick(object sender, ButtonEventArgs args)
        {
            if (ButtonClickedEvent != null)
                ButtonClickedEvent.Invoke(sender, args);
        }
    }
}
