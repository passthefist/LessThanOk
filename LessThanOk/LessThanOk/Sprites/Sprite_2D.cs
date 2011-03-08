using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LessThanOk.Sprites
{
    public class Sprite_2D : Sprite
    {

        private Texture2D texture2D { get; set; }
        private Color color { get; set; }
        private Vector2 origin { get; set; }

        public override Microsoft.Xna.Framework.Vector2 Size()
        {
            throw new NotImplementedException();
        }
    }
}
