using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LessThanOk.Network.Commands
{
    class Command_Add : Command
    {
        public Command_Add(UInt16 builderID, UInt16 builtID, UInt16 type,
            TimeSpan timeStamp)
        {
            granted = false;
            command = new UInt64[2];
            command[1] = (UInt64)timeStamp.Ticks;
            command[0] = 0x0000000000000000;
            command[0] |= (UInt64)builderID << 8;
            command[0] |= (UInt64)builtID << 24;
            command[0] |= (UInt64)type << 40;
            command[0] |= (UInt64)T_COMMAND.ADD << 56;
        }
        public UInt16 getBuilt() { return (UInt16)(command[0] >> 24); }

        public UInt16 getBuilder() { return (UInt16)(command[0] >> 8); }

        public UInt16 getType() { return (UInt16)(command[0] >> 40); }

        public string ToString()
        {
            string[] retval = new string[6];
            retval[0] = "OpCode\t\t: " + (command[0] >> 56).ToString();
            retval[1] = "Type\t\t:" + ((command[0] >> 40) & 0x000000000000FFFF).ToString();
            retval[2] = "Built\t:" + ((command[0] >> 24) & 0x00000000000FFFFF).ToString();
            retval[3] = "Builder\t:" + ((command[0] >> 8) & 0x000000000000FFFF).ToString();
            retval[4] = "Empty\t:" + (command[0] & 0x00000000000000FF).ToString(); 
            retval[5] = "Ticks\t\t:" + command[1].ToString();

            return retval[0] + "\n" + retval[1] + "\n" + retval[2] + "\n"
                + retval[3] + "\n" + retval[4] + "\n" + retval[5] + "\new";
        }
    }
}
