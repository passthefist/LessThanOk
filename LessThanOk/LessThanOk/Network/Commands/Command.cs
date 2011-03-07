using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/******************** COMMAND *******************************
* This class is the super class for all commands.           *
************************************************************/
namespace LessThanOk.Network.Commands
{
   
    public class Command
    {
        protected UInt64[] command;
        protected Boolean granted;
        public enum T_COMMAND
        {
            MOVE = 1,
            ADD,
            REMOVE,
            SET,
            ERROR,
            CANCEL
        }
        /// <summary>
        /// Constructs an empty command
        /// </summary>
        public Command()
        {
            granted = false;
            command = new UInt64[2];
            command[0] = 0x0000000000000000;
            command[1] = 0x0000000000000000;
        }
        /// <summary>
        /// Constructs a command from raw data.
        /// </summary>
        /// <param name="n_command">An array of data for the raw packet</param>
        public Command(UInt64[] n_command)
        {
            if (n_command.Length != 2)
                return;
            granted = false;
            command = new UInt64[2];
            command[1] = n_command[1];
            command[0] = n_command[0];
        }
        /// <summary>
        /// Returns the time stamp the command needs to be finished by.
        /// </summary>
        /// <returns>Ticks the command needs to be finished by</returns>
        public long getTimeStamp(){ return (long)command[1]; }
        /// <summary>
        /// Returns the type of command 
        /// </summary>
        /// <returns>Command Type</returns>
        public T_COMMAND getCommandType() 
        {
            if ((command[0] >> 56) == (UInt64)T_COMMAND.MOVE)
                return T_COMMAND.MOVE;
            else if ((command[0] >> 56) == (UInt64)T_COMMAND.ADD)
                return T_COMMAND.ADD;
            else if ((command[0] >> 56) == (UInt64)T_COMMAND.REMOVE)
                return T_COMMAND.REMOVE;
            else if ((command[0] >> 56) == (UInt64)T_COMMAND.SET)
                return T_COMMAND.SET;
            else if ((command[0] >> 56) == (UInt64)T_COMMAND.ERROR)
                return T_COMMAND.ERROR;
            else if ((command[0] >> 56) == (UInt64)T_COMMAND.CANCEL)
                return T_COMMAND.CANCEL;
            else
                return 0;
        }
        /// <summary>
        /// Sets the granted flag to true.
        /// </summary>
        public void grant() { granted = true; }
        /// <summary>
        /// Check whether the command has been granted.
        /// </summary>
        /// <returns>True if command was granted, false otherwise.</returns>
        public Boolean isGranted() { return granted; }
        /// <summary>
        /// Returns the string representing the command.  Usefull for debugging.
        /// </summary>
        /// <returns>Formatted String representing the command</returns>
        public String ToString()
        {
            return command[0].ToString() +" "+ command[1].ToString();
        }
        /// <summary>
        /// Returns the raw hex value as a string.
        /// </summary>
        /// <returns>String of the raw hex value.</returns>
        public String raw() { return command[0] + " " + command[1] + "\n"; }

    }
}
