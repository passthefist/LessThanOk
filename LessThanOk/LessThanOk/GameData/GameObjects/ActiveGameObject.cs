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
using LessThanOk.GameData.GameWorld;

namespace LessThanOk.GameData.GameObjects
{
    ///<summary>
    ///This class is an active game object, or anything which likely be drawn
    ///and move around in the game world like a unit or missile.
    ///</summary>

    public abstract class ActiveGameObject : GameObject,IQuadObject
    {
        //implement IQuadObject
        private Rectangle bounds;
        public Rectangle Bounds() { return bounds; }
        public event EventHandler BoundsChanged;

        protected void RaiseBoundsChanged()
        {
            EventHandler handler = BoundsChanged;
            if (handler != null)
                handler(this, new EventArgs());
        }

        public abstract Vector2 getPosition();

        public void setPosition(Vector2 pos)
        {
            setNewPosition(pos);
            RaiseBoundsChanged();
        }

        protected abstract void setNewPosition(Vector2 pos);
		
		protected ushort health;
		
		public ushort Health
		{
			get {return health;}
			set {health = value;}
		}
		
		protected Player owner;
		
		public Player Owner
		{
			get {return owner;}
			set {owner = value;}
		}
		
		//extensionList

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
        }

        //Must be implemented by any subclasses
        public abstract void update(GameTime elps);
    }
}