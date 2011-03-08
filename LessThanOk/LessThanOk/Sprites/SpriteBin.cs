using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace LessThanOk.Sprites
{
    class SpriteBin
    {
        List<Sprite> sprites;

        private SpriteFont _font;
        public SpriteBin(SpriteFont font)
        {
            sprites = new List<Sprite>();
            _font = font;
        }

        public Sprite_Text AddTextSprite(string content)
        {
            Sprite_Text s = new Sprite_Text(content, _font);
            sprites.Add(s);
            return s;
        }

        public void Add(Sprite s)
        {
            sprites.Add(s);
        }

        public void Clear()
        {
            sprites.Clear();
        }

    }
}
