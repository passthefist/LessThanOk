using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace LessThanOk.Sprites
{
    abstract class Sprite
    {
        public Vector2 Position { get; set; }
        public float Scale { get; set; }
        public float Rotation { get; set; }
        public Color Color { get; set; }
        public float Alpha { get; set; }
        public bool Centered { get; set; }

        public abstract Vector2 Size();
        public abstract void Draw(SpriteBatch sb);
    }
}
