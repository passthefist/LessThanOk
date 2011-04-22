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
    public class Unit : ActiveGameObject
    {
        public event EventHandler<CommandChangedEventArgs> CommandStarted;
        public event EventHandler<CommandChangedEventArgs> CommandFinished;
        public event EventHandler unitKilled;
        public event EventHandler unitHealthChange;
        public event EventHandler unitWeaponFired;
        public event EventHandler unitUsedAbility;

        /// <summary>
        /// The position of this object
        /// </summary>
        public Vector2 _Position
        {
            get { return engine.getPosition(); }
            set
            {
                engine.setStartPosition(value);
            }
        }

        private UnitType type;

        /// <summary>
        /// The type of this unit
        /// </summary>
        public UnitType Type
        {
            get { return type; }
            private set { type = value; }
        }

        private Vector2 velocity;

        private UInt32 hp;

        private Engine engine;

        public Engine Engine
        {
            get { return engine; }
        }

        /// <summary>
        /// How much health this unti has.
        /// </summary>
        public UInt32 health
        {
            get { return hp; }
            set { hp = value; }
        }

        private ActiveGameObject target;

        public ActiveGameObject Target
        {
            get { return target; }
            set { target = value; }
        }

        private bool aggressive;
        private bool pursue;

        private Command activeCommand;
        private Queue<Command> commands;//Needed? 

        static Unit()
        {
            AgnosticObject.initFieldMaps(typeof(Unit));
        }

        protected Unit() : base() { init(); }

        internal Unit(UnitType t)
            : base()
        {
            type = t;
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
            this.type = u.type;
            init();
        }

        private void init()
        {
            velocity = new Vector2();
            target = null;
            activeCommand = null;
            commands = new Queue<Command>();
            aggressive = false;
            pursue = false;

            engine = (Engine)type.Engine.create();
        }

        /// <summary>
        /// Update the unit.
        /// </summary>
        /// <param name="elps">
        /// A <see cref="GameTime"/>
        /// </param>
        override public void update(GameTime elps)
        {
            float del = (float)elps.ElapsedGameTime.TotalSeconds;

            if (activeCommand == null && commands.Count > 0)
            {
                activeCommand = commands.Dequeue();

                CommandStarted.Invoke(this, new CommandChangedEventArgs(activeCommand));

                switch (activeCommand.CmdType)
                {
                    case Command.T_COMMAND.MOVE:
                        Command cMov = new MoveDecorator(activeCommand);
                        ushort x = cMov.X;
                        ushort y = cMov.Y;

                        Vector2 finalPosition = new Vector2((float)x, (float)y);
                        engine.setTargetPosition(finalPosition);
                        break;
                }
            }

            if (activeCommand != null)
            {
                switch (activeCommand.CmdType)
                {
                    case Command.T_COMMAND.MOVE:
                        engine.update(elps);
                        break;
                    //case Command.T_COMMAND.USEABILITY:
                    //    break;
                    default:
                        break;
                }

                if (activeCommand.TimeStamp <= elps.TotalGameTime.Ticks)
                {
                    switch (activeCommand.CmdType)
                    {
                        case Command.T_COMMAND.MOVE:
                            break;
                    }

                    CommandFinished.Invoke(this, new CommandChangedEventArgs(activeCommand));
                    activeCommand = null;
                }
            }
        }

        public void draw(SpriteBatch batch)
        {
            batch.Draw(((Sprite_2D)type.getImage()).Texture, _Position, Color.White);
        }

        public void clearCommands()
        {
            commands.Clear();
            activeCommand = null;
        }

        public void addCommand(Command newCommand)
        {
            if (newCommand.CmdType == Command.T_COMMAND.CANCEL)
            {
                commands.Clear();
            }
            else
            {
                commands.Enqueue(newCommand);
            }
        }

        //	public WeaponFire fireWeapon()
    }
}