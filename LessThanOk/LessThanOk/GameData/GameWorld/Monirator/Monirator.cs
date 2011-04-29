/*---------------------------------------------------------------------------*\
 *                         LessThanOK Engine                                 *
 *                                                                           *
 *          Copyright (C) 2011-2012 by Robert Goetz, Anthony Lobono          *
 *                                                                           *
 *   authors:  Anthony LoBono (ajlobono@gmail.com)                           *
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
 * Monirator is responsible for processing and validating player requests.   *
 * It is also responsible for processing world changes and constructing      *
 * command to be sent out to all clients notifying them of the changes. This *
 * is how we are able to sync the host game to all the client games.         *
 *                                                                           *   
 * See: Commands.cs Command_Add.cs Command_Cancel.cs Command_Note.cs         *
 *      Command_Set.cs                                                       *
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

        public Monirator(RuleBook rules)
        {
            _ScheduledCommands = new Queue<Command>();
            _cmdEval = new CommandEvaluator();
            _schedule = new CommandSchedule();
            _rulebook = rules;
        }
        
        public void EvaluateCommand(Command req, GameTime time)
        {
            Queue<Command> EvaluationResults = new Queue<Command>();
            
            // TODO: get Player from PlayerList

            EvaluationResults = _cmdEval.EvaluateCommand(req, _rulebook, new Player(), time);
            if (EvaluationResults.Count == 1)
            {
                if (EvaluationResults.Peek().CmdType == Command.T_COMMAND.ERROR && RequestDenied != null)
                    RequestDenied.Invoke(this, new RequestDeniedEventArgs());
            }
            else
            {
                _schedule.Schedule(EvaluationResults);
            }
        }

        public void SetState(TileMap map) { _cmdEval.Map = map; }

        public void UpdateSchedule(GameTime time) 
        { 
            _schedule.step(time, out _ScheduledCommands);
        }

        public bool GetNextScheduledCommand(ref Command cmd)
        {
            if (_ScheduledCommands.Count <= 0)
                return false;
            cmd = _ScheduledCommands.Dequeue();
            return true;
        }

    }
}
