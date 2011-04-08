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
    public class Sprite_2D : Sprite
    {
        private Texture2D _texture;
        private Rectangle _sourceRect;

        public Texture2D Texture { get { return _texture; } }
        
        internal Sprite_2D(Texture2D texture, Vector2 size)
        {
            _texture = texture;
            Size = size;
            _sourceRect = new Rectangle(0, 0, (int)Size.X, (int)Size.Y);
            Color = Color.White;
            Scale = 1;
            Alpha = 255;
        }
        internal Sprite_2D(Sprite_2D sprite)
        {
            this._sourceRect = sprite._sourceRect;
            this._texture = sprite._texture;
            this.Size = sprite.Size;
            this.Color = sprite.Color;
            this.Scale = sprite.Scale;
            this.Alpha = sprite.Alpha;
            this.Centered = sprite.Centered;
            this.Rotation = sprite.Rotation;
        }
        public override void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, _sourceRect, Color); 
        }
    }
}
