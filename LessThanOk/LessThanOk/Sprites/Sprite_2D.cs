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
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

[assembly: InternalsVisibleTo("SpriteBin")]

namespace LessThanOk.Sprites
{
    public class Sprite_2D: Sprite
    {
        public override float Scale { get; set; }
        public override float Rotation { get; set; }
        public override Color Color { get; set; }
        public override float Alpha { get; set; }
        public override bool Centered { get; set; }
        public override int Height { get { return Texture.Height; } set { throw new NotImplementedException(); } }
        public override int Width { get { return Texture.Width; } set { throw new NotImplementedException(); } }
        public Texture2D Texture { get; set; }
        public Rectangle Source { get; set; }
        
        internal Sprite_2D(Texture2D texture, Rectangle source)
        {
            Texture = texture;
            Color = Color.White;
            Scale = 1;
            Alpha = 255;
            Source = source;
        }
        internal Sprite_2D(Sprite_2D sprite)
        {
            this.Source = new Rectangle(sprite.Source.X, sprite.Source.Y, sprite.Source.Width, sprite.Source.Height) ;
            this.Texture = sprite.Texture;
            this.Color = sprite.Color;
            this.Scale = sprite.Scale;
            this.Alpha = sprite.Alpha;
            this.Centered = sprite.Centered;
            this.Rotation = sprite.Rotation;
        }
        public override void Draw(SpriteBatch batch, int x, int y)
        {
            batch.Draw(Texture, new Vector2((float)x, (float)y), Source, Color);
        }
    }
}
