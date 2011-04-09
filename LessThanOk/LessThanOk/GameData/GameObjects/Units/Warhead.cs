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
 * Warheads are the damaging component of a weapon. They can have different  *
 * types, but that ultimately means nothing other than armor weaknesses.     *
 *                                                                           *
 * See GameObject, GameObjectType, GameObjectFactory                         *
 *                                                                           *
\*---------------------------------------------------------------------------*/


using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using LessThanOk.GameData.GameObjects;

[assembly: InternalsVisibleTo("WarheadType")]

namespace LessThanOk.GameData.GameObjects.Units
{

    public class Warhead : GameObject
    {
        private WarheadType type;

        /// <summary>
        /// The type of warhead.
        /// </summary>
        public WarheadType Type
        {
            get { return type; }
            private set { type = value; }
        }

        static Warhead()
        {
            AgnosticObject.initFieldMaps(typeof(Warhead));
        }

        protected Warhead()
            : base()
        {
            init();
        }

        internal Warhead(WarheadType p)
            : base()
        {
            init();
            type = p;
        }

        /// <summary>
        /// Copy ctor
        /// </summary>
        /// <param name="w">
        /// A <see cref="Warhead"/>
        /// </param>
        public Warhead(Warhead w)
            : base()
        {
            init();
            this.type = w.type;
        }

        private void init()
        {
            //damageMod = 0;
        }
    }
}