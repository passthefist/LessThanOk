using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

[assembly: InternalsVisibleTo("SpriteBin")]

namespace LessThanOk.Sprites
{
    public class Sprite_Text : Sprite
    {
        private SpriteFont _font;
        private string _text;

        public SpriteFont Font { get { return _font; } }
        new public Vector2 Size { get { return _font.MeasureString(_text); } }
        public string Text { get { return _text; } }

        internal Sprite_Text(string text, SpriteFont font)
        {
            _font = font;
            _text = text;
            Color = Color.White;
            Scale = 1;
            Alpha = 255;
        }
        internal Sprite_Text(Sprite_Text sprite)
        {
            this._text = sprite.Text;
            this._font = sprite.Font;
            this.Color = sprite.Color;
            this.Scale = sprite.Scale;
            this.Alpha = sprite.Alpha;
            this.Centered = sprite.Centered;
            this.Rotation = sprite.Rotation;
        }
        public override void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, _text, Position, Color, Rotation,
                Vector2.Zero, Scale, SpriteEffects.None, 0);
        }
    }
}
