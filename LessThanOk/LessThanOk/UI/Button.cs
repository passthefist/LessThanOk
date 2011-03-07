using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LessThanOk.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LessThanOk.UI
{
    class Button: Element
    {
        private Sprite textSprite;
 
        public Button(Sprite sprite)
        {
            textSprite = sprite;
        }
        public override void select()
        {
            Console.WriteLine("Button Selected");
        }
        public override void release()
        {

        }
        public override void hover()
        {
            Console.WriteLine("Button Hover");
        }

        public override void unHover()
        {

        }
        public override void decrease()
        {

        }
        public override void increase()
        {

        }
        public override Boolean isOver(int x, int y)
        {
            if (x >= origin.X && x <= textSprite.Size().X)
                if (y >= origin.Y && y <= textSprite.Size().Y)
                    return true;
            return false;
        }
        public override void draw(SpriteBatch spriteBatch) 
        {
            textSprite.Draw(spriteBatch);
           // Console.WriteLine("Drawing Button");
        }
        public void update(GameTime gameTime) { }
    }
}
