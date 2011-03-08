using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

[assembly: InternalsVisibleTo("SpriteBin")] 

namespace LessThanOk.Sprites
{
    class Sprite_Text : Sprite
    {
        public SpriteFont Font { get { return mFont; } set { mFont = value; } }
        public string Text { get { return mText; } set { mText = value; } }
        private SpriteFont mFont;
        private string mText;

        internal Sprite_Text(string text, SpriteFont font)
        {
            mFont = font;
            mText = text;
            Color = Color.White;
            Scale = 1;
            Alpha = 255;
        }

        public override Vector2 Size() { return mFont.MeasureString(Text); }
    }
}
