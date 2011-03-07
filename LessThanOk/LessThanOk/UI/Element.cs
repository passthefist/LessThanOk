using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LessThanOk.Sprites;

namespace LessThanOk.UI
{
    public class Element
    {
        public Boolean visible { set; get; }
        public Vector2 origin { get; set; }

        public virtual void select()
        {

        }
        public virtual void release()
        {

        }
        public virtual void hover()
        {
        }

        public virtual void unHover()
        {

        }
        public virtual void decrease()
        {

        }
        public virtual void increase()
        {

        }
        public virtual Boolean isOver(int x, int y)
        {
            return false;
        }
        public virtual void draw(SpriteBatch spriteBatch) { }
        public virtual void update(GameTime gameTime){}

    }
}
