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
        private Sprite_Text textSprite;
        public Vector2 Size{get{return textSprite.Size();}}
        public Element.SelectedAction Action { get; set; }
        public Button(Sprite_Text sprite, Boolean n_visible, Element.SelectedAction n_action)
        {
            textSprite = sprite;
            this.Action = n_action;
            this.visible = n_visible;
        }
        public void setPosition(Vector2 pos)
        {
            origin = pos;
        }

        public override void select()
        {
            Action();
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
            if (x >= origin.X && x <= (textSprite.Size().X + origin.X))
                if (y >= origin.Y && y <= (textSprite.Size().Y + origin.Y))
                    return true;
            return false;
        }
        public override void draw(SpriteBatch spriteBatch) 
        {
            spriteBatch.DrawString(textSprite.Font, textSprite.Text, origin,
                new Color(textSprite.Color, textSprite.Alpha), textSprite.Rotation,
                Vector2.Zero, textSprite.Scale, SpriteEffects.None, 0);
        }
        public void update(GameTime gameTime) { }
    }
}
