using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using LessThanOk.Sprites;

namespace LessThanOk.UI
{
    class Frame_Game: Frame
    {
        public Frame_Game(Vector2 n_origin, Vector2 n_size,
                        Sprite_2D n_background)
        {
            elements = null;
            frames = null;
            background = n_background;
            origin = n_origin;
            size = n_size;

        }
        public void addFrame(Frame element)
        {
            throw new NotImplementedException();
        }
        public void addElement(Vector2 n_origin, Element element)
        {
            throw new NotImplementedException();
        }
    }
}
