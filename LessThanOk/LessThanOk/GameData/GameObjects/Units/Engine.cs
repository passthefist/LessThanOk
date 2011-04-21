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
            AgnosticObject.initFieldMaps(typeof(Engine));
        }

        private Vector2 velocity;
        private Vector2 accel;
        private Vector2 position;
        private Vector2 target;

        private enum State
        {
            IDLE,
            INTERPOLATE
        };

        State state;

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
            init();
        }

        internal Engine(EngineType t)
        {
            type = t;
        }

        private void init()
        {
            velocity = new Vector2();
            position = new Vector2();
            target = new Vector2();
            accel = new Vector2();
            state = State.IDLE;
        }

        public Vector2 getPosition()
        {
            return position;
        }

        public void setStartPosition(Vector2 p)
        {
            position = p;
        }

        public void setTargetPosition(Vector2 t)
        {
            target = t;
            velocity = target - position;
            velocity.Normalize();
            velocity = velocity * type.MaxSpeed;
            state = State.INTERPOLATE;
        }

        public void update(GameTime elps)
        {
            switch (state)
            {
                case State.IDLE:
                    break;
                case State.INTERPOLATE:
                    position += velocity * (float)elps.ElapsedGameTime.TotalSeconds;
                    break;
            }
        }

        public float distanceToFinish()
        {
            return distToReach(target);
        }

        public float timeToFinish()
        {
            return timeToReach(target);
        }

        public float distToReach(Vector2 pos)
        {
            Vector2 diff = position - pos;
            return diff.Length();
        }

        public float timeToReach(Vector2 pos)
        {
            float dist = distToReach(pos);
            return dist / type.MaxSpeed;
        }
    }
}