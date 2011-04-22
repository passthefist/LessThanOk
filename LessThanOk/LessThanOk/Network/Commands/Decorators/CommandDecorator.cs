using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LessThanOk.Network.Commands
{
    public abstract class CommandDecorator: Command
    {
        protected Command decoratedCommand;
        public CommandDecorator() { }
        public CommandDecorator(Command cmd):base(cmd.Cmd)
        {
            this.decoratedCommand = cmd;
        }
    }
}
