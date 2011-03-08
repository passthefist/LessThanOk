
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
 * This class is a template for all user interface elements.  Every UI       *
 * element contains a Sprite, a Vecter2 origin(global possition), and an     *
 * identifying String name.  Elements are responsible for drawing the Sprite *
 * they contain.                                                             *
 *                                                                           *
 * See: Sprite.cs                                                            *
 *                                                                           *
\*---------------------------------------------------------------------------*/

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
        public Vector2 origin { set; get; }
        public String name { get; set; }
        public virtual void select(){}
        public virtual void release(){}
        public virtual void hover(){}
        public virtual void unHover(){}
        public virtual void decrease(){}
        public virtual void increase() {}
        public virtual Boolean isOver(int x, int y) { return false; }
        public virtual void draw(SpriteBatch spriteBatch) { }
        public virtual void update(GameTime gameTime){}

    }
}
