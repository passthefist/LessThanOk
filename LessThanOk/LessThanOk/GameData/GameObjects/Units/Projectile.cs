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
 * A projectile is the motion and graphic data of a weapon. It defines the   *
 * path and image/animation for showing a weapon being fired in the game.    *
 *                                                                           *
 * See GameObject, GameObjectType, GameObjectFactory, ProjectileType, Weapon *
 *                                                                           *
\*---------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using LessThanOk.GameData.GameObjects;

[assembly: InternalsVisibleTo("ProjectileType")]

namespace LessThanOk.GameData.GameObjects.Units
{
    //should be abstract eventually.

    /// <summary>
    /// The motion and graphic data of a weapon.
    /// </summary>
    public class Projectile : ActiveGameObject
    {
        private ProjectileType type;

        /// <summary>
        /// The type of projectile.
        /// </summary>
        public ProjectileType Type
        {
            get { return type; }
            private set { type = value; }
        }

        private Vector3 direc;

        /// <summary>
        /// The direction this projectile is traveling
        /// </summary>
        public Vector3 _Direction
        {
            get { return direc; }
            private set { direc = value; }
        }

        private float speed;

        /// <summary>
        /// The speed of this projectile.
        /// </summary>
        public float _Speed
        {
            get { return speed; }
            private set { speed = value; }
        }

        static Projectile()
        {
            initFieldMaps();
        }

        private static void initFieldMaps()
        {
            PropertyInfo[] properties = typeof(Projectile).GetProperties();

            ushort id = 0;
            foreach (PropertyInfo property in properties)
            {
                idToPropMap[id] = property;
                fieldNameToIDMap[property.Name] = id;
                id++;
            }
        }

        protected Projectile() { init(); }

        internal Projectile(ProjectileType t)
        {
            init();
            type = t;
        }


        /// <summary>
        /// Copy ctor
        /// </summary>
        /// <param name="p">
        /// A <see cref="Projectile"/>
        /// </param>
        public Projectile(Projectile p)
        {
            init();
            type = p.Type;
        }

        private void init()
        {
            speed = 0;
            direc = new Vector3();
        }

        /// <summary>
        /// Fire this projectile.
        /// </summary>
        /// <param name="d">
        /// A <see cref="Vector3"/>
        /// </param>
        /// <param name="p">
        /// A <see cref="Vector3"/>
        /// </param>
        public void fire(Vector3 d, Vector3 p)
        {
            direc = d;
            position = p;
        }

        //should be abstract?

        /// <summary>
        /// Update the position and direction of this projectile.
        /// </summary>
        /// <param name="elps">
        /// A <see cref="GameTime"/>
        /// </param>
        override public void update(GameTime elps)
        {
        }
    }
}