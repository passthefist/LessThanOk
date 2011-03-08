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
 * This class defines how an object moves in the game world.                 *
 *                                                                           *
 * See GameObject, GameObjectType, GameObjectFactory, EngineType, Unit       *
 *                                                                           *
\*---------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using LessThanOk.GameData.GameObjects;

[assembly: InternalsVisibleTo("EngineType")]

namespace LessThanOk.GameData.GameObjects.Units
{
    /// <summary>
    /// This class defines how an object moves in the game world.
    /// </summary>
    public class Engine : GameObject
    {
        private EngineType type;

        public EngineType Type
        {
            get { return type; }
            private set { type = value; }
        }

        static Engine()
        {
            initFieldMaps();
        }

        private static void initFieldMaps()
        {
            PropertyInfo[] properties = typeof(Engine).GetProperties();

            ushort id = 0;
            foreach (PropertyInfo property in properties)
            {
                idToPropMap[id] = property;
                fieldNameToIDMap[property.Name] = id;
                id++;
            }
        }

        protected Engine() : base() { }

        /// <summary>
        /// Copy ctor
        /// </summary>
        /// <param name="e">
        /// A <see cref="Engine"/>
        /// </param>
        public Engine(Engine e)
        {
            this.type = e.type;
        }

        internal Engine(EngineType t)
        {
            type = t;
        }
    }
}