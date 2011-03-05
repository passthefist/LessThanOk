using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LessThanOk.Network
{
    class Command_Error: Command
    {
        public Command_Error(UInt16 errorID, TimeSpan timeStamp)
        {
            command = new UInt64[2];
            command[1] = timeStamp;
            command[0] = 0x0000000000000000;
            command[0] |= T_COMMAND.ERROR << 56;
            command[0] |= errorID << 40;
        }

        public UInt16 getError() { return command[0] >> 40; }

        public string ToString()
        {
             string[] retval = new string[4];
             retval[0] = "OpCode\t\t: " + (command[0] >> 56).ToString;
             retval[1] = "ID\t\t:" + ((command[0] >> 40) & 0x00000000000000FF).ToString;
             retval[2] = "Empty\t\t:" + (command[0] & 0x000000FFFFFFFFFF).ToString;
             retval[3] = "Ticks\t\t:" + command[1].ToString;

             return retval[0] + "\n" + retval[1] + "\n" + retval[2] + "\n" 
                 + retval[3] + "\n";
        }
    }
}
