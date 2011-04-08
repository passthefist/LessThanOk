using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LessThanOk.Network.Commands
{
    class Command_Cancel: Command
    {
         public Command_Cancel(UInt16 id, TimeSpan timeStamp)
        {
            command = new UInt64[2];
            command[1] = (UInt64)timeStamp.Ticks;
            command[0] = 0x0000000000000000;
            command[0] |= (UInt64)T_COMMAND.ERROR << 56;
            command[0] |= (UInt64)id << 40;
        }

         public UInt16 getID() { return (UInt16)(command[0] >> 40); }

         public override string ToString()
         {
             string[] retval = new string[4];
             retval[0] = "OpCode\t\t: " + (command[0] >> 56).ToString();
             retval[1] = "ID\t\t:" + ((command[0] >> 40) & 0x00000000000000FF).ToString();
             retval[2] = "Empty\t\t:" + (command[0] & 0x000000FFFFFFFFFF).ToString();
             retval[3] = "Ticks\t\t:" + command[1].ToString();

             return retval[0] + "\n" + retval[1] + "\n" + retval[2] + "\n" 
                 + retval[3] + "\n";
         }
    }
}
