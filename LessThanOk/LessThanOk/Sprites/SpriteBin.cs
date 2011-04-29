/*---------------------------------------------------------------------------*\
*                         LessThanOK Engine                                 *
*                                                                           *
*          Copyright (C) 2011-2012 by Robert Goetz, Anthony Lobono          *
*                                                                           *
*          authors:  Anthony LoBono (ajlobono@gmail.com)                    *
*                                                                           *
\*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*\
 *                                License                                    *
 *                                                                           *
 * This library is free software; you can redistribute it and/or modify it   *
 * under the terms of the MIT Liscense.                                      *
 *                                                                           *
 * This library is distributed in the hope that it will be useful, but       *
 * WITHOUT ANY WARRANTY; without even the implied warranty of                *
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.                      *
 *                                                                           *
 * You should have received a copy of the MIT Liscense with this library, if *
 * not, visit http://www.opensource.org/licenses/mit-license.php.            *
 *                                                                           *
\*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*\
 *                            Class Overview                                 *
 *                                                                           *
 *                                                                           *
\*---------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace LessThanOk.Sprites
{
    public sealed class SpriteBin
    {
        private Dictionary<String, Sprite> _sprites;

        static readonly SpriteBin the = new SpriteBin();
        static SpriteBin() { }
        public static SpriteBin The { get { return the; } }

        private SpriteBin()
        {
            _sprites = new Dictionary<String, Sprite>();

        }
        public Sprite_Text AddTextSprite(SpriteFont font, String content, String key)
        {
            Sprite_Text s = new Sprite_Text(content, font);
            _sprites.Add(key, s);
            return s;
        }
        public Sprite_2D Add2DSprite(Texture2D texture, Rectangle source, String key)
        {
            Sprite_2D s = new Sprite_2D(texture, source);
            _sprites.Add(key, s);
            return s;
        }
        public void Add(Sprite s, String key)
        {
            _sprites.Add(key, s);
        }
        public void Clear()
        {
            _sprites.Clear();
        }
        public Sprite getSprite(String key) 
        {
            Sprite retval;
            if (!(_sprites.TryGetValue(key, out retval)))
                return null;
  
            if (retval is Sprite_2D)
                return new Sprite_2D((Sprite_2D)retval);
            else if (retval is Sprite_Text)
                return new Sprite_Text((Sprite_Text)retval);
            else
                return null;
        }

    }
}
