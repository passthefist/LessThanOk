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
        public abstract float Rotation { get; set; }
        public abstract Color Color { get; set;  }
        public abstract float Alpha { get; set;  }
        public abstract bool Centered { get; set; }
        public abstract int Width { get; set; }
        public abstract int Height { get; set; }
        public virtual void Draw(SpriteBatch batch, int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
