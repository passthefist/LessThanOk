﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LessThanOk.Network.Commands;

namespace LessThanOk.GameData.GameWorld.Monirator
{
    class CommandEvaluator
    {
        internal TileMap Map { get; set; }

        private PathFinder _explorer;

        internal Queue<Command> EvaluateCommand(Command req, RuleBook rules)
        {
            Queue<Command> retval = new Queue<Command>();
            switch (req.CmdType)
            {
                case Command.T_COMMAND.MOVE:
                    retval = evaluateMove(req, rules);
                    break;
                case Command.T_COMMAND.ADD:
                    retval = evaluateAdd(req, rules);
                    break;
                case Command.T_COMMAND.REMOVE:
                    retval = evaluateRemove(req, rules);
                    break;
                case Command.T_COMMAND.SET:
                    retval = evaluateSet(req, rules);
                    break;
                case Command.T_COMMAND.ERROR:
                    retval = evaluateError(req, rules);
                    break;
                case Command.T_COMMAND.CANCEL:
                    retval = evaluateMove(req, rules);
                    break;
                default:
                    break;
            }
        }

        private Queue<Command> evaluateMove(Command req, RuleBook rules)
        {
            throw new NotImplementedException();
        }

        private Queue<Command> evaluateError(Command req, RuleBook rules)
        {
            throw new NotImplementedException();
        }

        private Queue<Command> evaluateSet(Command req, RuleBook rules)
        {
            throw new NotImplementedException();
        }

        private Queue<Command> evaluateRemove(Command req, RuleBook rules)
        {
            throw new NotImplementedException();
        }

        private Queue<Command> evaluateAdd(Command req, RuleBook rules)
        {
            throw new NotImplementedException();
        }

        private Queue<Command> evaluateMove(Command req, RuleBook rules)
        {
            throw new NotImplementedException();
        }
    }
}
