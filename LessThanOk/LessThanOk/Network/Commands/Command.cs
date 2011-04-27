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
 * Command.cs is the super class for all Command Types.                      *
 *                                                                           *
\*---------------------------------------------------------------------------*/
         
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LessThanOk.Network.Commands
{
   
    public class Command
    {
        private UInt64[] _command;
        public Command()
        {
            _command = new UInt64[2];
            _command[0] = 0x0000000000000000;
            _command[1] = 0x0000000000000000;
        }
        public Command(UInt64[] command)
        {
            if (command.Length != 2)
                return;
            _command = new UInt64[2];
            _command[1] = command[1];
            _command[0] = command[0];
        }

        #region Shared By All Commands
        public virtual UInt64[] Cmd { get { return _command; } }
        public virtual long TimeStamp { get { return (long)_command[1]; } }
        public virtual T_COMMAND CmdType
        {
            get
            {
                if ((_command[0] >> 56) == (UInt64)T_COMMAND.MOVE)
                    return T_COMMAND.MOVE;
                else if ((_command[0] >> 56) == (UInt64)T_COMMAND.ADD)
                    return T_COMMAND.ADD;
                else if ((_command[0] >> 56) == (UInt64)T_COMMAND.REMOVE)
                    return T_COMMAND.REMOVE;
                else if ((_command[0] >> 56) == (UInt64)T_COMMAND.SET)
                    return T_COMMAND.SET;
                else if ((_command[0] >> 56) == (UInt64)T_COMMAND.ERROR)
                    return T_COMMAND.ERROR;
                else if ((_command[0] >> 56) == (UInt64)T_COMMAND.CANCEL)
                    return T_COMMAND.CANCEL;
                else
                    return 0;
            }
        }
        public UInt16 Actor
        {
            get
            {
                switch (this.CmdType)
                {
                    case T_COMMAND.MOVE:
                        return (UInt16)(_command[0] >> 48);
                        break;
                    case T_COMMAND.ADD:
                        return (UInt16)(_command[0] >> 24);
                        break;
                }
            }
        }
        public enum T_COMMAND
        {
            MOVE = 1,
            ADD,
            REMOVE,
            SET,
            ERROR,
            ATTACK,
            CANCEL
        }
        #endregion

        #region Functionality to be add by the decorator
        public virtual UInt16 ParentID { get { throw new NotImplementedException(); } }
        public virtual UInt16 UnitID { get { throw new NotImplementedException(); } }
        public virtual UInt16 Type { get { throw new NotImplementedException(); } }
        public virtual UInt16 Target { get { throw new NotImplementedException(); } }
        public virtual UInt16 X { get { throw new NotImplementedException(); } }
        public virtual UInt16 Y { get { throw new NotImplementedException(); } }

        #endregion
    }
}
