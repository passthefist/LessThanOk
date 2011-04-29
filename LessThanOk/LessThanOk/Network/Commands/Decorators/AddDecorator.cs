/*---------------------------------------------------------------------------*\
*                         LessThanOK Engine                                 *
*                                                                           *
*          Copyright (C) 2011-2012 by Robert Goetz, Anthony Lobono          *
*                                                                           *
*          authors:  Anthony LoBono (ajlobono@gmail.com)                    *
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
 * AddDecorator adds functionaly to the Command Class for Add Command Types. *
 * A better solution should be found.                                        *
 *                                                                           *
\*---------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LessThanOk.Network.Commands.Decorators
{
    class AddDecorator: CommandDecorator
    {
        public AddDecorator(Command cmd): base(cmd){ }
        /// <summary>
        /// Contructor for AddDecorator.
        /// </summary>
        /// <param name="parentID">UnitID of the parent unit.</param>
        /// <param name="newID">ID of the unit to be added.</param>
        /// <param name="type">UnitType for the unit that should be added.</param>
        /// <param name="time">Tiem the add needs to be completed by.</param>
        /// <param name="cmd">Command for the Decorator.</param>
        public AddDecorator(UInt16 parentID, UInt16 newID, UInt16 type, TimeSpan time, Command cmd)
            : base(cmd)
        {
            _command[0] |= ((UInt64)T_COMMAND.ADD << 56);
            _command[0] |= ((UInt64)parentID << 24);
            _command[0] |= ((UInt64)newID << 8);
            _command[0] |= ((UInt64)type << 40);
            _command[1] = (UInt64)time.Ticks;
        }
        /// <summary>
        /// Add functionality.
        /// </summary>
        public override UInt16 ParentID { get { return (UInt16)(_command[0] >> 24); } }
        /// <summary>
        /// Add functionality.
        /// </summary>
        public override UInt16 UnitID { get { return (UInt16)(_command[0] >> 8); } }
        /// <summary>
        /// Add functionality.
        /// </summary>
        public override UInt16 Type { get { return (UInt16)(_command[0] >> 40); } }
    }
}
