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
 * This class is armor, to make your units strong!                           *
 *                                                                           *
 * See GameObject, GameObjectType, GameObjectFactory, ArmorType,             *
 *    Unit, UnitType.                                                        *
 *                                                                           *
\*---------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using LessThanOk.GameData.GameObjects;

[assembly: InternalsVisibleTo("ArmorType")]

namespace LessThanOk.GameData.GameObjects.Units
{
    /// <summary>
    /// Armor for a unit
    /// </summary>
    public class Armor : GameObject
    {
        private int strength;

        static Armor()
        {
            AgnosticObject.initFieldMaps(typeof(Armor));
        }

        protected Armor()
            : base()
        {
        }

        internal Armor(ArmorType t)
            : base()
        {
            Type = t;
            strength = t.Strength;
        }

        /// <summary>
        /// Copy ctor
        /// </summary>
        /// <param name="a">
        /// A <see cref="Armor"/>
        /// </param>
        public Armor(Armor a)
        {
            this.Type = (ArmorType)a.Type;
            strength = ((ArmorType)a.Type).Strength;
        }

        /// <summary>
        /// Apply damage to this armor. The armor takes some damage
        /// and also mitigates the damage done.
        /// </summary>
        /// <param name="w">
        /// A <see cref="Warhead"/>
        /// </param>
        /// <returns>
        /// The amount of damage not absorbed by the armor. <see cref="System.Byte"/>
        /// </returns>
        public byte applyDamage(Warhead w)
        {
            return ((WarheadType)w.Type).Damage;
        }
    }
}
