using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LessThanOk.Network
{
    class Command_Set : Command
    {
        public Command_Set(UInt16 id, UInt16 key, UInt32 value,
            TimeSpan timeStamp)
        {
            command = new UInt64[2];
            command[1] = timeStamp;
            command[0] = 0x0000000000000000;
            command[0] |= T_COMMAND.SET << 56;
            command[0] |= id << 40;
            command[0] |= key << 32;
            command[0] |= value;
        }

        public UInt16 getID() { return command[0] >> 40; }

        public byte getKey() { return command[0] >> 32; }

        public UInt32 getValue() { return command[0]; }

        public string ToString()
        {
            string[] retval = new string[5];
            retval[0] = "OpCode\t\t: " + (command[0] >> 56).ToString;
            retval[1] = "ID\t\t:" + ((command[0] >> 40) & 0x000000000000FFFF).ToString;
            retval[2] = "Key\t\t:" + ((command[0] >> 32) & 0x00000000000FFFFF).ToString;
            retval[3] = "Value\t:" + (command[0] & 0x00000000FFFFFFFF).ToString;
            retval[4] = "Ticks\t\t:" + command[1].ToString;

            return retval[0] + "\n" + retval[1] + "\n" + retval[2] + "\n" 
                + retval[3] + "\n" + retval[4] + "\n";
        }
    }
}
