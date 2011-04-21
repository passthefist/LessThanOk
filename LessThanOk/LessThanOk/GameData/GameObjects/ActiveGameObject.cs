/*---------------------------------------------------------------------------*\
 *                         LessThanOK Engine                                 *
 *                                                                           *
 *          Copyright (C) 2011-2012 by Robert Goetz, Anthony Lobono          *
 *                                                                           *
 *   authors:  Robert Goetz (rdgoetz@iastate.edu)                            *
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
 * This class is an active game object, or anything which likely be drawn    *
 * and move around in the game world like a unit or missile. Subclasses      *
 * must implement update().                                                  *
 *                                                                           *
 * See GameObject, GameObjectType, GameObjectFactory                         *
 *                                                                           *
\*---------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Xna.Framework;
using LessThanOk.Sprites;
using LessThanOk.Network.Commands;

namespace LessThanOk.GameData.GameObjects
{
    ///<summary>
    ///This class is an active game object, or anything which likely be drawn
    ///and move around in the game world like a unit or missile.
    ///</summary>

    public abstract class ActiveGameObject : GameObject
    {
        protected Vector2 position;

        /// <summary>
        /// The position of this object
        /// </summary>
        public Vector2 _Position
        {
            get { return position; }
            set
            {
                position = value;
            }
        }

        protected Sprite_2D image;

        /// <summary>
        /// The image for this object
        /// </summary>
        public Sprite_2D Sprite
        {
            get { return image; }
            set { image = value; }
        }

        //---------------------Reflexive Maps----------------------
        static ActiveGameObject()
        {
            AgnosticObject.initFieldMaps(typeof(ActiveGameObject));
        }

        //-----------------Instance Functions----------------------

        public ActiveGameObject()
            : base()
        {
            position = new Vector2();
        }

        //Must be implemented by any subclasses
        public abstract void update(GameTime elps);
    }
}