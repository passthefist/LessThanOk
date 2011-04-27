using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using LessThanOk.Sprites;
using LessThanOk.UI.Events.Args;

namespace LessThanOk.UI.Frames.UIElements
{
    class ToggleButton : Button
    {
        public enum STATE
        {
            UP,
            DOWN
        }
        public STATE State { get { return _state; } }
        private STATE _state;
        private Color _colorS1;
        private Color _colorS2;

        public ToggleButton(LessThanOk.UI.WindowDefinitions.BUTTON name, Sprite image, int x, int y, Color c1, Color c2)
            : base(name, image, x, y)
        {
            _colorS1 = c1;
            _colorS2 = c2;
        }
        
        public override void draw(SpriteBatch batch)
        {
            if (_state == STATE.DOWN)
                Image.Color = _colorS2;
            if (_state == STATE.UP)
                Image.Color = _colorS1;
            Image.Draw(batch, _posx, _posy);
        }

        public override void Click()
        {
            if (_state == STATE.DOWN)
            {
                _state = STATE.UP;
                base.Click(new ButtonEventArgs(this, ButtonEventArgs.STATE.UP));
            }
            else
            {
                _state = STATE.DOWN;
                base.Click();
            }

        }
 
    }
}
