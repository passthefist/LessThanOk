using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LessThanOk.Network.Commands;

namespace LessThanOk.Events
{
    public class CommandChangedEventArgs : EventArgs
    {
        private Command newCmd;

        public CommandChangedEventArgs(Command c)
        {
            newCmd = c;
        }

        public Command getCommand()
        {
            return newCmd;
        }
    }
}