/*---------------------------------------------------------------------------*\
*                         LessThanOK Engine                                  *
*                                                                            *
*          Copyright (C) 2011-2012 by Robert Goetz, Anthony Lobono           *
*                                                                            *
*          authors:  Anthony LoBono (ajlobono@gmail.com)                     *
*                                                                            * 
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
 * CommandEvaluator should be used for evaluating all commands before they   *
 * are exicuted. CommandEvaluator is also responsible for translating        *
 * requests into multiple commands.                                          *
 *                                                                           *
\*---------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LessThanOk.Network.Commands;
using LessThanOk.Network.Commands.Decorators;
using LessThanOk.GameData.GameObjects;
using LessThanOk.GameData.GameObjects.Units;
using Microsoft.Xna.Framework;

namespace LessThanOk.GameData.GameWorld.MoniratorSpace
{
    class CommandEvaluator
    {
        private TileMap _map;

        private PathFinder _explorer;
        /// <summary>
        /// Method for evaluating commands. Calls private functions based on what type of command is being evaluated.
        /// </summary>
        /// <param name="req">Command that is to be evaluated.</param>
        /// <param name="rules">Rulebook for checking dependancies, build time, etc...</param>
        /// <param name="player">Player that is requesting the command being evaluated.</param>
        /// <param name="time">Current game time.</param>
        /// <returns>Queue of translated commands resulting from the evaluations of req.</returns>
        internal Queue<Command> EvaluateCommand(Command req, RuleBook rules, Player player, GameTime time)
        {
            Queue<Command> retval = new Queue<Command>();
            switch (req.CmdType)
            {
                case Command.T_COMMAND.MOVE:
                    retval = evaluateMove(req, rules, player, time);
                    break;
                case Command.T_COMMAND.ADD:
                    retval = evaluateAdd(req, rules, player, time);
                    break;
                case Command.T_COMMAND.REMOVE:
                    retval = evaluateRemove(req, rules, player, time);
                    break;
                case Command.T_COMMAND.SET:
                    retval = evaluateSet(req, rules, player, time);
                    break;
                case Command.T_COMMAND.ERROR:
                    retval = evaluateError(req, rules, player, time);
                    break;
                case Command.T_COMMAND.CANCEL:
                    retval = evaluateMove(req, rules, player, time);
                    break;
                case Command.T_COMMAND.ATTACK:
                    retval = evaluateAttack(req, rules, player, time);
                    break;
                default:
                    break;
            }
            return retval;
        }
        /// <summary>
        /// Method for evaluating the attack command.
        /// </summary>
        /// <param name="req">Attack Command</param>
        /// <param name="rules">Rules for evaluating the command.</param>
        /// <param name="player">Player requesting the attack command.</param>
        /// <param name="time">Current game time.</param>
        /// <returns>Queue of translated commands resulting from the evaluation.</returns>
        private Queue<Command> evaluateAttack(Command req, RuleBook rules, Player player, GameTime time)
        {
            Queue<Command> retval = new Queue<Command>();
            // Decorate the command to get the required functionallity.
            // Probably a better solution. Suggestions welcome.
            Command attack = new AttackDecorator(req);
            // Get the target of the attack.
            ActiveGameObject targ = (ActiveGameObject)GameObjectFactory.The.getGameObject(attack.Target);
            if (targ == null)
            {
                // TODO: flush the unit's queue of commands.  Signifies the attack is over / target is dead.
            }
            // Unit the command is acting on.
            Unit unit = (Unit)GameObjectFactory.The.getGameObject(attack.Actor);
            // Pathfind to the target.
            List<Vector2> waypoints = _explorer.GetPath(unit.getPosition(), targ.getPosition(), _map);
            Engine engine = unit.Engine;
            // Time To Destination.
            // Note for attack commands we only used the first waypoint.
            float ttd = engine.timeToReach(waypoints[0]);
            float curt = time.TotalGameTime.Ticks;
            retval.Enqueue(new MoveDecorator(req.Actor, (UInt16)targ.getPosition().X, (UInt16)targ.getPosition().Y, curt + ttd, new Command()));
            // TODO: push an attack on as well.
            return retval;
        }
        /// <summary>
        /// Method for evaluating Move Commands.
        /// </summary>
        /// <param name="req">Move Command being evaluated.</param>
        /// <param name="rules">RuleBook for evaluating the command.</param>
        /// <param name="player">Player who requested the move.</param>
        /// <param name="time">Current game time.</param>
        /// <returns>Queue of translated commands resulting from the evaluation.</returns>
        private Queue<Command> evaluateMove(Command req, RuleBook rules, Player player, GameTime time)
        {
            Queue<Command> retval = new Queue<Command>();
            // Decorate the command to get the required functionality.
            // Probably a better way to do this...
            Command move = new MoveDecorator(req);
            // Unit the command was acting on.
            ActiveGameObject unit = (ActiveGameObject)GameObjectFactory.The.getGameObject(move.Actor);
            Vector2 origin = unit.getPosition();
            Vector2 dest = new Vector2((long)move.X, (long)move.Y);
            Engine engine = ((Unit)unit).Engine;
            List<Vector2> waypoints = _explorer.GetPath(origin, dest, _map);
            // Check that next waypoint is valid. Depending on implimentaiton of PathFinder, this may not be nessisary.
            if (waypoints.Count > 0 && _map.HasUnitsInPath(origin, waypoints[0]))
            {
                Command deny = new DenyDecorator(req.Actor, time.TotalGameTime.Ticks, new Command());
                retval.Enqueue(deny);
                // TODO: trigger request denied event.
                return retval;
            }
            // Build Move Commands out of waypoints.
            foreach(Vector2 p in waypoints)
            {
                float ttd = engine.timeToReach(dest);
                float curt = time.TotalGameTime.Ticks;
                retval.Enqueue(new MoveDecorator(req.Actor, (UInt16)p.X, (UInt16)p.Y, curt + ttd, new Command()));
            }
            return retval;
        }
        /// <summary>
        /// TODO: Impliment
        /// </summary>
        /// <param name="req"></param>
        /// <param name="rules"></param>
        /// <param name="player"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        private Queue<Command> evaluateError(Command req, RuleBook rules, Player player, GameTime time)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// TODO: Impliment.
        /// </summary>
        /// <param name="req"></param>
        /// <param name="rules"></param>
        /// <param name="player"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        private Queue<Command> evaluateSet(Command req, RuleBook rules, Player player, GameTime time)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// TODO: Impliment
        /// </summary>
        /// <param name="req"></param>
        /// <param name="rules"></param>
        /// <param name="player"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        private Queue<Command> evaluateRemove(Command req, RuleBook rules, Player player, GameTime time)
        {
            throw new NotImplementedException();
        }
       
        // TODO: Get cost of unit 
        // TODO: Get player resources
        // TODO: Get buildtime of the unit
        private Queue<Command> evaluateAdd(Command req, RuleBook rules, Player player, GameTime time)
        {
            // Decorate the command
            Command add = new AddDecorator(req);
           
            // Get the units parent
            UInt16 parent = add.ParentID;
            
            // Get its name to look up in the rule book
            String name = GameObjectFactory.The.getType(add.Type).Name;
            
            // Get the type evaluator from the rule book
            UnitEvaluation dep = rules.getDependancy(name);
            
            // Get the name of the type to add
            name = dep.getType(rules._unitExists);
            
            // Get UInt16 type id from name
            UInt16 type = GameObjectFactory.The.getType(name).ID;
            
            // TODO: Get cost of unit 
            // TODO: Get player resources
            // TODO: Get buildtime of the unit
            

            Queue<Command> retval = new Queue<Command>();
            Command toadd = new AddDecorator(parent, 0, type, time.TotalGameTime, new Command());
            retval.Enqueue(toadd);
            return retval;
        }
        /// <summary>
        /// TODO: Impliment
        /// </summary>
        /// <param name="req"></param>
        /// <param name="rules"></param>
        /// <returns></returns>
        private Queue<Command> evaluateCancel(Command req, RuleBook rules)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Initalize TileMap
        /// </summary>
        /// <param name="map">TileMap</param>
        internal void Initialize(TileMap map)
        {
            _map = map;
        }
    }
}
