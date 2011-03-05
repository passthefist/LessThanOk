using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LessThanOk.Network
{
   
    public class Command
    {
        protected UInt64[] command;

        public enum T_COMMAND
        {
            MOVE,
            ADD,
            REMOVE,
            SET,
            ERROR,
            CANCEL
        }
        public Command()
        {
            command = new UInt64[2];
            command[0] = 0x0000000000000000;
            command[1] = 0x0000000000000000;
        }
        public Command(UInt64[] n_command)
        {
            if (n_command.Length != 2)
                return false;

            command = new UInt64[2];
            command[1] = n_command[1];
            command[0] = n_command[0];
        }

        public TimeSpan getTimeStamp(){ return new TimeSpan((long)command[1]); }
       
        public T_COMMAND getCommandType() 
        {
            if (((command[0] >> 56) & T_COMMAND.MOVE) == T_COMMAND.MOVE)
                return T_COMMAND.MOVE;
            else if (((command[0] >> 56) & T_COMMAND.ADD) == T_COMMAND.ADD)
                return T_COMMAND.ADD;
            else if (((command[0] >> 56) & T_COMMAND.REMOVE) == T_COMMAND.REMOVE)
                return T_COMMAND.REMOVE;
            else if (((command[0] >> 56) & T_COMMAND.SET) == T_COMMAND.SET)
                return T_COMMAND.SET;
            else if (((command[0] >> 56) & T_COMMAND.ERROR) == T_COMMAND.ERROR)
                return T_COMMAND.ERROR;
            else if (((command[0] >> 56) & T_COMMAND.CANCEL) == T_COMMAND.CANCEL)
                return T_COMMAND.CANCEL;
        }

        public string ToString()
        {
            return command[0].ToString() +" "+ command[1].ToString();
        }
    }
}
