using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework;
using LessThanOk.Network;
using LessThanOk.Sprites;

namespace LessThanOk.UI
{
    class LobbyList : UIElement
    {
        public LobbyList(String name, int x, int y, Sprite_Text image)
        {
            _name = name;
            _posx = x;
            _posy = y;
            _image = image;
            _size = _image.Size;
        }

        public override void update(GameTime gameTime)
        {
            String text = "";
            Sprite_Text temp = (Sprite_Text)_image;
            /*
            foreach (Gamer g in session.AllGamers)
            {
                text += g.Gamertag;
                text += "\n";
            }
            temp.Text = text;
            _image = temp;
             */
        }
        public override void draw(SpriteBatch spriteBatch)
        {
            _image.draw(spriteBatch);
        }
    }
}
