/*---------------------------------------------------------------------------*\
*                         LessThanOK Engine                                 *
*                                                                           *
*          Copyright (C) 2011-2012 by Robert Goetz, Anthony Lobono          *
*                                                                           *
*          authors:  Anthony LoBono (ajlobono@gmail.com)                    *
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
 * CommandSchedule holds the queued up scheduled of commands for all units.  *
 * Access to all queues will be need for user interface aspects. When a      *
 * Command's timestamp expires the command will be dequeued and playced in   *
 * a Queue. See Step method.                                                 *
 *                                                                           *
\*---------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using LessThanOk.Network.Commands;
using LessThanOk.Network.Commands.Decorators;

namespace LessThanOk.GameData.GameWorld.MoniratorSpace
{
    class CommandSchedule
    {
        private Dictionary<UInt16, Queue<Command>> _unitQueues;

        public CommandSchedule()
        {
            _unitQueues = new Dictionary<ushort, Queue<Command>>();
        }
        /// <summary>
        /// Enqueue commands for units
        /// </summary>
        /// <param name="EvaluationResults">Commands to be scheduled.</param>
        internal void Schedule(Queue<Command> EvaluationResults)
        {
            UInt16 key;
            Queue<Command> actorsQueue;
            foreach (Command cmd in EvaluationResults)
            {
                key = cmd.Actor;
                // Check if unit's queue already exists.
                if (_unitQueues.ContainsKey(key))
                {
                    if (_unitQueues.TryGetValue(key, out actorsQueue))
                    {
                        actorsQueue.Enqueue(cmd);
                    }
                }
                // Create a Queue for the unit.
                else
                {
                    actorsQueue = new Queue<Command>();
                    actorsQueue.Enqueue(cmd);
                    _unitQueues.Add(key, actorsQueue);
                }
            }
        }
        /// <summary>
        /// Step the schedule
        /// </summary>
        /// <param name="time">Current game time.</param>
        /// <param name="ScheduledCommands">Queue that all currently exicuting commands will be played in.</param>
        internal void step(GameTime time, out Queue<Command> ScheduledCommands)
        {
            ScheduledCommands = new Queue<Command>();
         
            foreach (Queue<Command> q in _unitQueues.Values)
            {
                foreach (Command cmd in q)
                {
                    // If Command is expired dequeue it.
                    if (cmd.TimeStamp > time.TotalGameTime.Ticks)
                        q.Dequeue();
                    // Peek at the next command to be exicuted.    
                    ScheduledCommands.Enqueue(q.Peek());
                }
            }
        }
    }
}
