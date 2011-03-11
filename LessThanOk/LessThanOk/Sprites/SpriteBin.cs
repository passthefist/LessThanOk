/*---------------------------------------------------------------------------*\
 *                         LessThanOK Engine                                 *
 *                                                                           *
 *          Copyright (C) 2011-2012 by Robert Goetz, Anthony Lobono          *
 *                                                                           *
 *   authors:  Anthony LoBono (ajlobono@gmail.com)                           *
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
 * SpriteBin is responsible for creating all instances of Sprites.           *
 *                                                                           *                                 
 * See: Sprite.cs                                                            *
 *                                                                           *
\*---------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace LessThanOk.Sprites
{
    public sealed class SpriteBin
    {
        static readonly SpriteBin the = new SpriteBin();
      
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static SpriteBin()
        {
        }

        public static SpriteBin The
        {
            get
            {
                return the;
            }
        }
        private Dictionary<String, Sprite> spriteDictionary;

        public SpriteFont _font{ get; set; }
        /// <summary>
        /// Contructor for SpriteBin.
        /// </summary>
        /// <param name="font">Font to be used for all sprites.  Shouls be a list</param>
        private SpriteBin()
        {
            spriteDictionary = new Dictionary<String, Sprite>();

        }

        /// <summary>
        /// Get a new instantce of a Sprite_Text
        /// </summary>
        /// <param name="content">Text for the Sprite</param>
        /// <returns>New instance of a Sprite_Text</returns>
        public Sprite_Text AddTextSprite(String content, String key)
        {
            Sprite_Text s = new Sprite_Text(content, _font);
            spriteDictionary.Add(key, s);
            return s;
        }
        public Sprite_2D AddSprite_2D(Texture2D texture, Color color, String key)
        {
            Sprite_2D s = new Sprite_2D(texture, color);
            spriteDictionary.Add(key, s);
            return s;
        }
        /// <summary>
        /// Add a Sprite to the sprite list
        /// </summary>
        /// <param name="s">Sprite to be added to list.</param>
        public void Add(Sprite s, String key)
        {
            spriteDictionary.Add(key, s);
        }
        /// <summary>
        /// Clear the sprite list.
        /// </summary>
        public void Clear()
        {
            spriteDictionary.Clear();
        }
        public Sprite getSprite(String key){ return spriteDictionary[key]; }

    }
}
