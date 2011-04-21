/*---------------------------------------------------------------------------*\
*                         LessThanOK Engine                                 *
*                                                                           *
*          Copyright (C) 2011-2012 by Robert Goetz, Anthony Lobono          *
*                                                                           *
*   authors:  Anthony LoBono (ajlobono@gmail.com)                           *
*                                                                           *
\*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*\
 *                                License                                    *
 *                                                                           *
 * This library is free software; you can redistribute it and/or modify it   *
 * under the terms of the MIT Liscense.                                      *
 *                                                                           *
 * This library is distributed in the hope that it will be useful, but       *
 * WITHOUT ANY WARRANTY; without even the implied warranty of                *
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.                      *
 *                                                                           *
 * You should have received a copy of the MIT Liscense with this library, if *
 * not, visit http://www.opensource.org/licenses/mit-license.php.            *
 *                                                                           *
\*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*\
 *                            Class Overview                                 *
 *                                                                           *
 * Command_Add.cs is the command for adding objects to the game.  Each add   *
 * command has an ID for the builder, an ID for the object to be added, and  *
 * an ID for the object type.  In the case that the command is a request the *
 * ID of object to be added should be ignored.                               *
 *                                                                           *
 * See: Command.cs                                                           *
 *                                                                           *
\*---------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LessThanOk.Network.Commands
{
    public class Command_Move : Command
    {
        /// <summary>
        /// Constructor for Command_Add.
        /// </summary>
        /// <param name="builderID">
        /// Unsigned int identifying the Unit that 
        /// should move</param>
        /// <param name="builtID">
        /// Unsigned int identifying the GameObject to be
        /// moved to.</param>
        /// <param name="timeStamp">
        /// TimeSpan identifying the number of ticks the command needs to be
        /// exicuted by. Only valid if command has been granted.</param>
        public Command_Move(UInt16 unit, UInt16 x, UInt16 y, TimeSpan timeStamp)
        {
            command = new UInt64[2];
            command[1] = (UInt64)timeStamp.Ticks;
            command[0] = 0x0000000000000000;
            command[0] |= (UInt64)unit << 32;
            command[0] |= (UInt64)x << 16;
            command[0] |= (UInt64)y;
            command[0] |= (UInt64)T_COMMAND.MOVE << 56;
        }
        public Command_Move(UInt64[] ndata)
        {
            if (ndata.Length != 2)
                throw new Exception();
            command[0] = ndata[0];
            command[1] = ndata[1];
        }
        public UInt16 getTarget() { return (UInt16)(command[0] >> 32); }

        public UInt16 getX() { return (UInt16)(command[0] >> 16); }

        public UInt16 getY() { return (UInt16)(command[0]); }

        public override string ToString()
        {
            string[] retval = new string[5];
            retval[0] = "OpCode\t\t: " + (command[0] >> 56).ToString();
            retval[1] = "target\t:" + ((command[0] >> 24) & 0x00000000000FFFFF).ToString();
            retval[2] = "unit\t:" + ((command[0] >> 8) & 0x000000000000FFFF).ToString();
            retval[3] = "Empty\t:" + (command[0] & 0x00000000000000FF).ToString(); 
            retval[4] = "Ticks\t\t:" + command[1].ToString();

            return retval[0] + "\n" + retval[1] + "\n" + retval[2] + "\n"
                + retval[3] + "\n" + retval[4] + "\n";
        }
    }
}
