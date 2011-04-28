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
 * A unit. Units have armor, weapons, and engines that allow them to move.   *
 * Units also have a set of commands to execute, and some basic AI           *
 *                                                                           *
 * See GameObject, GameObjectType, GameObjectFactory, pretty much everything.*
 *                                                                           *
\*---------------------------------------------------------------------------*/


using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using LessThanOk.Network.Commands;
using LessThanOk.GameData.GameObjects;
using LessThanOk.Sprites;
using Microsoft.Xna.Framework.Graphics;
using LessThanOk.Events;
using LessThanOk.Network.Commands.Decorators;

[assembly: InternalsVisibleTo("UnitType")]

namespace LessThanOk.GameData.GameObjects.Units
{
    /// <summary>
    /// A unit. Units have armor, weapons, and engines that allow them to move.
    /// </summary>
    public class   Unit: ActiveGameObject
    {
        public event EventHandler<CommandChangedEventArgs> CommandStarted;
        public event EventHandler<CommandChangedEventArgs> CommandFinished;
        public event EventHandler unitKilled;
        public event EventHandler unitHealthChange;
        public event EventHandler unitWeaponFired;
        public event EventHandler unitUsedAbility;

        public enum UnitState
        {
            IDLE,
            MOVE,
            BUILD,
            CAST
        }

        UnitState state;

        /// <summary>
        /// The position of this object
        /// </summary>
        public override Vector2 getPosition()
        {
            return engine.getPosition();
        }
        
        protected override void setNewPosition(Vector2 pos)
        {
            engine.setStartPosition(pos);
        }

        private Vector2 velocity;

        private UInt32 hp;

        private Engine engine;

        public Engine Engine
        {
            get { return engine; }
        }

        private ActiveGameObject target;

        public ActiveGameObject Target
        {
            get { return target; }
        }

        private Weapon weapon;

        public Weapon MainWeapon
        {
            get { return weapon; }
        }

        private bool aggressive;
        private bool pursue;

        private Command activeCommand; 

        static Unit()
        {
            AgnosticObject.initFieldMaps(typeof(Unit));
        }

        protected Unit() : base() { init(); }

        internal Unit(UnitType t)
            : base()
        {
            Type = t;
        }

        /// <summary>
        /// Copy ctor
        /// </summary>
        /// <param name="u">
        /// A <see cref="Unit"/>
        /// </param>
        public Unit(Unit u)
            : base()
        {
            this.Type = u.Type;
            init();
        }

        private void init()
        {
            velocity = new Vector2();
            target = null;
            activeCommand = null;
            aggressive = false;
            pursue = false;

            engine = (Engine)((UnitType)Type).Engine.create();
        }

        public void moveTo(Vector2 position)
        {
            engine.setTargetPosition(position);
            state = UnitState.MOVE;
        }

        public void idle()
        {
            target = null;
            state = UnitState.IDLE;
        }

        public bool isAggressive()
        {
            return aggressive;
        }

        public bool isPursuing()
        {
            return pursue;
        }

        public void setTarget(ActiveGameObject targ)
        {
            target = targ;
            aggressive = true;
        }

        public void forceFinishAction()
        {
            switch (state)
            {
                case UnitState.MOVE:
                    setPosition(engine.getFinalPosition());
                    break;
                case UnitState.BUILD:
                case UnitState.CAST:
                case UnitState.IDLE:
                    break;
            }

            state = UnitState.IDLE;
        }

        //public void assist(ActiveGameObject targ)

        /// <summary>
        /// Update the unit.
        /// </summary>
        /// <param name="elps">
        /// A <see cref="GameTime"/>
        /// </param>
        override public void update(GameTime elps)
        {
            switch (state)
            {
                case UnitState.MOVE:
                    engine.update(elps);
                    break;
                case UnitState.BUILD:
                    //do build animation
                case UnitState.CAST:
                    //do cast animation
                case UnitState.IDLE:
                    //do idle animation
                    break;
            }
        }

        public void draw(SpriteBatch batch)
        {
            batch.Draw(Sprite.Texture, getPosition(), Color.White);
        }

        //	public Weapon fireWeapon()
    }
}