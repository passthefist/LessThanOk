using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LessThanOk.Network
{
    class Command_Add : Command
    {
        public Boolean Command_ADD(UInt16 builderID, UInt16 builtID, UInt16 type,
            TimeSpan timeStamp)
        {
            command = new UInt64[2];
            command[1] = timeStamp;
            command[0] = 0x0000000000000000;
            command[0] |= builderID << 8;
            command[0] |= builtID << 24;
            command[0] |= type << 40;
            command[0] |= T_COMMAND.ADD << 56;
        }
        public UInt16 getBuilt() { return command[0] >> 24; }
        
        public UInt16 getBuilder() { return command[0] >> 8; }

        public UInt16 getType() { return command[0] >> 40; }

        public string ToString()
        {
            string[] retval = new string[6];
            retval[0] = "OpCode\t\t: " + (command[0] >> 56).ToString;
            retval[1] = "Type\t\t:" + ((command[0] >> 40) & 0x000000000000FFFF).ToString;
            retval[2] = "Built\t:" + ((command[0] >> 24) & 0x00000000000FFFFF).ToString;
            retval[3] = "Builder\t:" + ((command[0] >> 8) & 0x000000000000FFFF).ToString;
            retval[4] = "Empty\t:" + (command[0] & 0x00000000000000FF).ToString; 
            retval[5] = "Ticks\t\t:" + command[1].ToString;

            return retval[0] + "\n" + retval[1] + "\n" + retval[2] + "\n"
                + retval[3] + "\n" + retval[4] + "\n" + retval[5] + "\new";
        }
    }
}
