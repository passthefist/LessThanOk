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
 * The type of a Unit.                                                       *
 *                                                                           *
 * See GameObject, GameObjectType, GameObjectFactory                         *
 *                                                                           *
\*---------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Xna.Framework;
using LessThanOk.Sprites;
using LessThanOk.GameData.GameObjects;

namespace LessThanOk.GameData.GameObjects.Units
{
    /// <summary>
    /// The type of unit.
    /// </summary>
    public class UnitType : GameObjectType
    {
        private List<WeaponType> weapons;
        private ArmorType armor;
        private EngineType engine;

        public EngineType Engine
        {
            get { return engine; }
        }

        private Sprite image;
        private int maxHp;

        static UnitType()
        {
            AgnosticObject.initFieldMaps(typeof(UnitType));
        }

        public UnitType(string wep, string a, string e, Sprite s)
        {
            List<WeaponType> weps = new List<WeaponType>(1);
            weps.Add((WeaponType)GameObjectFactory.The.getType(wep));
            init(weps, (ArmorType) GameObjectFactory.The.getType(a), (EngineType) GameObjectFactory.The.getType(e), s);
        }

        public UnitType(WeaponType wep, ArmorType a, EngineType e, Sprite s)
        {
            List<WeaponType> weps = new List<WeaponType>(1);
            weps.Add(wep);
            init(weps, a, e, s);
        }

        /// <summary>
        /// Create a new unit type
        /// </summary>
        /// <param name="weps">
        /// The weapons <see cref="List<WeaponType>"/>
        /// </param>
        /// <param name="a">
        /// The armor <see cref="ArmorType"/>
        /// </param>
        /// <param name="e">
        /// the engine <see cref="EngineType"/>
        /// </param>
        public UnitType(List<WeaponType> weps, ArmorType a, EngineType e, Sprite s)
        {
            init(weps,a,e,s);
        }

        public Sprite getImage()
        {
            return image;
        }

        private void init(List<WeaponType> weps, ArmorType a, EngineType e, Sprite s)
        {
            weapons = weps;
            armor = a;
            engine = e;
            image = s;
            protoType = new Unit(this);
        }

        /// <summary>
        /// create a new unit.
        /// </summary>
        /// <returns>
        /// A <see cref="GameObject"/>
        /// </returns>
        override public GameObject create()
        {
            return new Unit((Unit)protoType);
        }
    }
}