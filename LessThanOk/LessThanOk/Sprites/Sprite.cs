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
        public abstract float Scale { get; set; }
        public abstract Vector2 Position { get; set; }
        public abstract Vector2 Size { get; }
        public abstract float Rotation { get; set; }
        public abstract Color Color { get; set;  }
        public abstract float Alpha { get; set;  }
        public abstract bool Centered { get; set; }
        public abstract void draw(SpriteBatch spriteBatch);
    }
}
