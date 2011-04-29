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
 * Monirator is responsible for monitoring and moderating a game.  This      *
 * class can be thought of as the reforie. Contains many classes for         *
 * performing these responsiblities.                                         *
 *                                                                           *
\*---------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using LessThanOk.Network.Commands;
using LessThanOk.GameData;
using LessThanOk.GameData.GameWorld;
using LessThanOk.GameData.GameWorld.Monirator.Events;

namespace LessThanOk.GameData.GameWorld.Monirator
{
    public class Monirator
    {
        public event EventHandler<RequestDeniedEventArgs> RequestDenied; 

        private CommandEvaluator _cmdEval;
        private RuleBook _rulebook;
        private CommandSchedule _schedule;
        private Queue<Command> _ScheduledCommands;
        /// <summary>
        /// Constructor for Monirator
        /// </summary>
        /// <param name="rules">Set of initial rules.</param>
        public Monirator(RuleBook rules)
        {
            _ScheduledCommands = new Queue<Command>();
            _cmdEval = new CommandEvaluator();
            _schedule = new CommandSchedule();
            _rulebook = rules;
        }
        /// <summary>
        /// Method for evaluating commands.
        /// </summary>
        /// <param name="req">Command for Evaluation.</param>
        /// <param name="time">Current game time.</param>
        public void EvaluateCommand(Command req, GameTime time)
        {
            Queue<Command> EvaluationResults = new Queue<Command>();
            
            // TODO: get Player from PlayerList
            // Use Command Evaluator for evaluating the command.
            EvaluationResults = _cmdEval.EvaluateCommand(req, _rulebook, new Player(), time);
            // Check if request was denied.
            if (EvaluationResults.Count == 1)
            {
                if (EvaluationResults.Peek().CmdType == Command.T_COMMAND.ERROR && RequestDenied != null)
                    RequestDenied.Invoke(this, new RequestDeniedEventArgs(0, new Player()));
            }
            // Schedule evaluation results.
            else
            {
                _schedule.Schedule(EvaluationResults);
            }
        }
        /// <summary>
        /// Updates the TileMap refrence.
        /// </summary>
        /// <param name="map">New refrence to TileMap.</param>
        public void SetState(TileMap map) { _cmdEval.Map = map; }
        /// <summary>
        /// Advances the scheduled commands with the current game time.
        /// </summary>
        /// <param name="time">Current game time.</param>
        public void UpdateSchedule(GameTime time) 
        { 
            _schedule.step(time, out _ScheduledCommands);
        }
        /// <summary>
        /// Pull Commands off of the currently exicuting Command queue.
        /// </summary>
        /// <param name="cmd">Command will be passed back through cmd.</param>
        /// <returns>True if there are commands currently exicuting.</returns>
        public bool GetNextScheduledCommand(ref Command cmd)
        {
            if (_ScheduledCommands.Count <= 0)
                return false;
            cmd = _ScheduledCommands.Dequeue();
            return true;
        }

    }
}
