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
 * Sprite_Text is a subclass of Sprit.  It is used for drawing a text to the *
 * application window.                                                       *
 *                                                                           *
 * See: Sprite.cs SpriteBin.cs                                               *
 *                                                                           *
\*---------------------------------------------------------------------------*/

using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

[assembly: InternalsVisibleTo("SpriteBin")] 

namespace LessThanOk.Sprites
{
    class Sprite_Text : Sprite
    {
        public SpriteFont Font { get { return mFont; } set { mFont = value; } }
        public string Text { get { return mText; } set { mText = value; } }
        private SpriteFont mFont;
        private string mText;
        /// <summary>
        /// Should never be called except by a SpriteBin
        /// </summary>
        /// <param name="text">Text for the Sprite</param>
        /// <param name="font">Font of the Text</param>
        internal Sprite_Text(string text, SpriteFont font)
        {
            mFont = font;
            mText = text;
            Color = Color.White;
            Scale = 1;
            Alpha = 255;
        }
        /// <summary>
        /// Get the size of the Sprite
        /// </summary>
        /// <returns>The size of the Sprite represented as a Vector2</returns>
        public override Vector2 Size() { return mFont.MeasureString(Text); }
    }
}
