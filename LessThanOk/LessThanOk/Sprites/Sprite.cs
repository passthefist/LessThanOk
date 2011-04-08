using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LessThanOk.Sprites
{
    public abstract class Sprite
    {
        public float Scale { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public float Rotation { get; set; }
        public Color Color { get; set; }
        public float Alpha { get; set; }
        public bool Centered { get; set; }
        public abstract void draw(SpriteBatch spriteBatch);
    }
}
