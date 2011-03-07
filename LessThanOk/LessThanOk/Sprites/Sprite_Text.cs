using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LessThanOk.Sprites
{
    class Sprite_Text : Sprite
    {
        public SpriteFont Font
        {
            get
            {
                return mFont;
            }
            set
            {
                mFont = value;
                if (Text != null)
                {
                    mOrigin = mFont.MeasureString(Text) / 2;
                }
            }
        }


        public string Text
        {
            get
            {
                return mText;
            }
            set
            {
                mText = value;
                if (mFont != null)
                {
                    mOrigin = mFont.MeasureString(Text) / 2;
                }
            }
        }


        public Sprite_Text(string text, SpriteFont font)
        {
            mFont = font;
            Text = text;
            Position = Vector2.Zero;
            Color = Color.White;
            Scale = 1;
            Alpha = 255;
        }

        public override Vector2 Size()
        {
            return mFont.MeasureString(Text);
        }

        private SpriteFont mFont;
        private string mText;
        private Vector2 mOrigin;

        public override void Draw(SpriteBatch sb)
        {
            sb.DrawString(Font, mText, Position, new Color(Color, Alpha), Rotation,
                Vector2.Zero, Scale, SpriteEffects.None, 0);
        }
    }
}
