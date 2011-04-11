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
    public class Command_Add : Command
    {
        /// <summary>
        /// Constructor for Command_Add.
        /// </summary>
        /// <param name="builderID">
        /// Unsigned int identifying the GameObject that 
        /// requested the add command</param>
        /// <param name="builtID">
        /// Unsigned int identifying the GameObject to be
        /// added.  Only Valid if the command has been granted.</param>
        /// <param name="type">
        /// Unsigned int identifying the GameObjectType to be added</param>
        /// <param name="timeStamp">
        /// TimeSpan identifying the number of ticks the command needs to be
        /// exicuted by. Only valid if command has been granted.</param>
        public Command_Add(UInt16 builderID, UInt16 builtID, UInt16 type,
            TimeSpan timeStamp)
        {
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

        public override string ToString()
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
