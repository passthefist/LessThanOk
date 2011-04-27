using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LessThanOk.Sprites;
using LessThanOk.UI.Events;
using LessThanOk.UI;
using LessThanOk.UI.Events.Args;

namespace LessThanOk.UI
{
    public class Button
    {
        public LessThanOk.UI.WindowDefinitions.BUTTON Name { get { return _name; } }
        public Sprite Image { get { return _image; } }
        public int X { get { return _posx; } }
        public int Y { get { return _posy; } }
        public int Width { get { return Image.Width; } }
        public int Height { get { return Image.Height; } }
  
        protected LessThanOk.UI.WindowDefinitions.BUTTON _name;
        protected Sprite _image;
        protected int _posx;
        protected int _posy;
        protected Boolean _visible;
        protected Boolean _mouseOver;
        protected event EventHandler<ButtonEventArgs> _trigger;

        public Button() { }

        public Button(LessThanOk.UI.WindowDefinitions.BUTTON name, Sprite image, int x, int y)
        {
            _name = name;
            _image = image;
            _posx = x;
            _posy = y;
        }

        public virtual void update(GameTime gameTime)
        {
            int x = Mouse.GetState().X;
            int y = Mouse.GetState().Y;
            if (x > _posx && x < (_posx + Width))
            {
                if (y > _posy && y < (_posy + Height))
                    _mouseOver = true;
            }
            else
                _mouseOver = false;

        }
        public virtual void draw(SpriteBatch spriteBatch)
        {
            if (_mouseOver)
                Image.Color = Color.BlueViolet;
            else
                Image.Color = Color.White;
            
            Image.Draw(spriteBatch, _posx, _posy); 
        }
        public virtual void AddListeners(List<EventHandler<ButtonEventArgs>> handlers)
        {
            foreach(EventHandler<ButtonEventArgs> handle in handlers)
            {
                _trigger += handle;
            }
        }
        public virtual void AddListener(EventHandler<ButtonEventArgs> handle)
        {
           _trigger += handle;
        }
        public virtual void Click()
        {
            if(_trigger != null)
                _trigger.Invoke(this, new ButtonEventArgs(this));
        }
        protected virtual void  Click(ButtonEventArgs args) 
        {
            if (_trigger != null)
                _trigger.Invoke(this, args);
        }
    }
}
