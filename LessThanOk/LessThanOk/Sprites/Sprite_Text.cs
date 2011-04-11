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
        private Vector2 _size;

        public override float Scale { get; set; }
        public override Vector2 Position { get; set; }
        public override float Rotation { get; set; }
        public override Color Color { get; set; }
        public override float Alpha { get; set; }
        public override bool Centered { get; set; }
        public SpriteFont Font { get { return _font; } }
        public override Vector2 Size { get { return _size; } }
        public override bool Hover { get; set; }
        public string Text { get; set; }

        internal Sprite_Text(string text, SpriteFont font)
        {
            _font = font;
            Text = text;
            _size = _font.MeasureString(Text);
            Color = Color.White;
            Scale = 1;
            Alpha = 255;
        }
        internal Sprite_Text(Sprite_Text sprite)
        {
            this.Text = sprite.Text;
            this._font = sprite.Font;
            this.Color = sprite.Color;
            this.Scale = sprite.Scale;
            this.Alpha = sprite.Alpha;
            this.Centered = sprite.Centered;
            this.Rotation = sprite.Rotation;
        }
        public override void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, Text, Position, Color, Rotation,
                Vector2.Zero, Scale, SpriteEffects.None, 0);
        }
    }
}
