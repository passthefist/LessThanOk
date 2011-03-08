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
 * This class is a factory for arbitrarily creating GameObjects.             *
 * GameObjectTypes are added to the factory and associated with a name.      *
 * The factory can then create a GameObject of this type from the given name.*
 *                                                                           *
 * Changing any of the members of a GameObjectType will immediately change   *
 * the respective fields in any existing GameObjects of the type, or any     *
 * GameObject that has a member which is a GameObject of the modified type.  *
 *                                                                           *
 * This class is a singleton, and the factory is accessed with               *
 *      GameObjectFactory.The;                                               *
 *                                                                           *
 * All types must be built bottom up.                                        *
 *                                                                           *
 * See GameObject, GameObjectType                                            *
 *                                                                           *
\*---------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace LessThanOk.GameData.GameObjects
{

    /// <summary>
    /// This class is a factory for arbitrarily creating GameObjects from
    /// a name or type ID.
    /// </summary>

    public sealed class GameObjectFactory
    {
        static readonly GameObjectFactory the = new GameObjectFactory();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static GameObjectFactory()
        {
        }

        public static GameObjectFactory The
        {
            get
            {
                return the;
            }
        }
        private UInt16 numTypes;
        private UInt16 nextID;
        private Dictionary<UInt16, GameObject> createdObjects;

        private Dictionary<UInt16, GameObjectType> idToTypeMap;
        private Dictionary<string, UInt16> stringToIdMap;

        private GameObjectFactory()
        {
            numTypes = 0;
            nextID = 1;
            createdObjects = new Dictionary<ushort, GameObject>();

            idToTypeMap = new Dictionary<UInt16, GameObjectType>();
            stringToIdMap = new Dictionary<string, UInt16>();
        }

        public void freeID(UInt16 id)
        {
            createdObjects.Remove(id);
            nextID = id;
        }

        private void findNextID()
        {
            nextID++;
            if (nextID == 0)
            {
                throw new Exception();
            }

            while (createdObjects.ContainsKey(nextID))
            {
                nextID++;
            }
        }

        /// <summary>
        /// add a type to the factory
        /// </summary>
        /// <param name="typeName">
        /// A <see cref="System.String"/>
        /// </param>
        /// <param name="type">
        /// A <see cref="GameObjectType"/>
        /// </param>
        /// <returns>
        /// A <see cref="System.Boolean"/>
        /// </returns>
        public bool addType(string typeName, GameObjectType type)
        {
            if (numTypes == UInt16.MaxValue)
            {
                Console.WriteLine("FACTORY CANNOT HAVE MORE TYPES");
                return false;
            }
            else
            {
                idToTypeMap[numTypes] = type;
                stringToIdMap[typeName] = numTypes;
                type.Name = typeName;
                type.ID = numTypes;

                numTypes++;
            }
            return true;
        }

        /// <summary>
        /// create an object given a type id
        /// </summary>
        /// <param name="id">
        /// A <see cref="UInt32"/>
        /// </param>
        /// <returns>
        /// A <see cref="GameObject"/>
        /// </returns>
        public GameObject createGameObject(UInt16 id)
        {
            GameObject retVal = idToTypeMap[id].create();
            createdObjects[id] = retVal;
            retVal.ID = nextID;
            findNextID();
            return retVal;
        }

        /// <summary>
        /// create an object given a type name
        /// </summary>
        /// <param name="typeName">
        /// A <see cref="System.String"/>
        /// </param>
        /// <returns>
        /// A <see cref="GameObject"/>
        /// </returns>
        public GameObject createGameObject(string typeName)
        {
            return createGameObject(stringToIdMap[typeName]);
        }

        /// <summary>
        /// get a type given the id
        /// </summary>
        /// <param name="id">
        /// A <see cref="UInt32"/>
        /// </param>
        /// <returns>
        /// A <see cref="GameObjectType"/>
        /// </returns>
        public GameObjectType getType(UInt16 id)
        {
            return idToTypeMap[id];
        }

        /// <summary>
        /// get a type given the type name
        /// </summary>
        /// <param name="typeName">
        /// A <see cref="System.String"/>
        /// </param>
        /// <returns>
        /// A <see cref="GameObjectType"/>
        /// </returns>
        public GameObjectType getType(string typeName)
        {
            return getType(stringToIdMap[typeName]);
        }

        /// <summary>
        ///replace an already existing type with a new type.
        ///NOTE: This will not "update" any existing GameObjects
        ///      that are of this type or have any members of
        ///      this type. Any objects created from the name or
        ///      id will reflect the change after calling replaceType()
        /// </summary>
        /// <param name="id">
        /// A <see cref="UInt32"/>
        /// </param>
        /// <param name="newType">
        /// A <see cref="GameObjectType"/>
        /// </param>
        public void replaceType(UInt16 id, GameObjectType newType)
        {
            newType.Name = idToTypeMap[id].Name;
            newType.ID = idToTypeMap[id].ID;
            idToTypeMap[id] = newType;
        }

        /// <summary>
        ///replace an already existing type with a new type.
        ///NOTE: This will not "update" any existing GameObjects
        ///      that are of this type or have any members of
        ///      this type. Any objects created from the name or
        ///      id will reflect the change after calling replaceType()
        /// </summary>
        /// <param name="typeName">
        /// A <see cref="System.String"/>
        /// </param>
        /// <param name="newType">
        /// A <see cref="GameObjectType"/>
        /// </param>
        public void replaceType(string typeName, GameObjectType newType)
        {
            replaceType(stringToIdMap[typeName], newType);
        }

        public GameObject getGameObject(UInt16 id)
        {
            return createdObjects[id];
        }
    }
}