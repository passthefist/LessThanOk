using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LessThanOk.Network.Commands;
using LessThanOk.Network.Commands.Decorators;
using LessThanOk.GameData.GameObjects;
using LessThanOk.GameData.GameObjects.Units;
using Microsoft.Xna.Framework;

namespace LessThanOk.GameData.GameWorld.Monirator
{
    class CommandEvaluator
    {
        internal TileMap Map { get; set; }

        private PathFinder _explorer;

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

        private Queue<Command> evaluateAttack(Command req, RuleBook rules, Player player, GameTime time)
        {
            Queue<Command> retval = new Queue<Command>();

            Command attack = new AttackDecorator(req);
            ActiveGameObject targ = (ActiveGameObject)GameObjectFactory.The.getGameObject(attack.Target);
            if (targ == null)
            {
                // flush req.Actor's Queue
            }
            Unit unit = (Unit)GameObjectFactory.The.getGameObject(attack.Actor);
            List<Vector2> waypoints = _explorer.GetPath(unit.getPosition(), targ.getPosition(), Map);
            Engine engine = unit.Engine;
            float ttd = engine.timeToReach(waypoints[0]);
            float curt = time.TotalGameTime.Ticks;
            retval.Enqueue(new MoveDecorator(req.Actor, (UInt16)targ.getPosition().X, (UInt16)targ.getPosition().Y, curt + ttd, new Command()));
            return retval;
        }
        //Done
        private Queue<Command> evaluateMove(Command req, RuleBook rules, Player player, GameTime time)
        {
            Queue<Command> retval = new Queue<Command>();

            Command move = new MoveDecorator(req);
            ActiveGameObject unit = (ActiveGameObject)GameObjectFactory.The.getGameObject(move.Actor);
            Vector2 origin = unit.getPosition();
            Vector2 dest = new Vector2((long)move.X, (long)move.Y);
        
            if(Map.hasUnitsInPath(origin, dest))
            {
                Command deny = new DenyDecorator(req.Actor, time.TotalGameTime.Ticks, new Command());
                retval.Enqueue(deny);
                return retval;
            }
            Engine engine = ((Unit)unit).Engine;
            List<Vector2> waypoints = _explorer.GetPath(origin, dest, Map);
            foreach(Vector2 p in waypoints)
            {
                float ttd = engine.timeToReach(dest);
                float curt = time.TotalGameTime.Ticks;
                retval.Enqueue(new MoveDecorator(req.Actor, (UInt16)p.X, (UInt16)p.Y, curt + ttd, new Command()));
            }
            return retval;
        }

        private Queue<Command> evaluateError(Command req, RuleBook rules, Player player, GameTime time)
        {
            throw new NotImplementedException();
        }

        private Queue<Command> evaluateSet(Command req, RuleBook rules, Player player, GameTime time)
        {
            throw new NotImplementedException();
        }

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

        private Queue<Command> evaluateCancel(Command req, RuleBook rules)
        {
            throw new NotImplementedException();
        }
    }
}
