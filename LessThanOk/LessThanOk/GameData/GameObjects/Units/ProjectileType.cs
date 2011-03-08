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
 * The type of a projectile.                                                 *
 *                                                                           *
 * See GameObject, GameObjectType, GameObjectFactory                         *
 *                                                                           *
\*---------------------------------------------------------------------------*/


using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Xna.Framework;

using LessThanOk.GameData.GameObjects;namespace LessThanOk.GameData.GameObjects.Units
{
    /// <summary>
    /// The type of a projectile.
    /// </summary>
    public class ProjectileType : GameObjectType
    {
        private bool homing;

        /// <summary>
        /// If this projectile is homing.
        /// </summary>
        public bool Homing
        {
            get
            {
                return homing;
            }
            set
            {
                homing = value;
            }
        }

        private float accel;
        /// <summary>
        /// The acceleration.
        /// </summary>
        public float Accel
        {
            get
            {
                return accel;
            }
            set
            {
                accel = value;
            }
        }

        private float startSpeed;
        /// <summary>
        /// firing speed.
        /// </summary>
        public float StartSpeed
        {
            get
            {
                return startSpeed;
            }
            set
            {
                startSpeed = value;
            }
        }

        private int timeout;

        /// <summary>
        /// The time before the projectile self detonates.
        /// </summary>
        public int Timeout
        {
            get
            {
                return timeout;
            }
            set
            {
                timeout = value;
            }
        }

        private float arc;

        /// <summary>
        /// The maximimum height the projectile will travel.
        /// </summary>
        public float Arc
        {
            get
            {
                return arc;
            }
            set
            {
                arc = value;
            }
        }

        static ProjectileType()
        {
            initFieldMaps();
        }

        private static void initFieldMaps()
        {
            PropertyInfo[] properties = typeof(ProjectileType).GetProperties();

            ushort id = 0;
            foreach (PropertyInfo property in properties)
            {
                idToPropMap[id] = property;
                fieldNameToIDMap[property.Name] = id;
                id++;
            }
        }

        /// <summary>
        /// Make a projectile type.
        /// </summary>
        /// <param name="h">
        /// Homing? <see cref="System.Boolean"/>
        /// </param>
        /// <param name="a">
        /// Accel <see cref="System.Single"/>
        /// </param>
        /// <param name="s">
        /// Inital speed. <see cref="System.Single"/>
        /// </param>
        /// <param name="c">
        /// Arc height. <see cref="System.Single"/>
        /// </param>
        public ProjectileType(bool h, float a, float s, float c)
        {
            homing = h;
            accel = a;
            startSpeed = s;
            arc = c;

            protoType = new Projectile(this);
        }

        /// <summary>
        /// Create a projectile of this type.
        /// </summary>
        /// <returns>
        /// A new projectile. <see cref="GameObject"/>
        /// </returns>
        override public GameObject create()
        {
            return new Projectile((Projectile)protoType);
        }
    }
}