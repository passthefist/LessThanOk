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
 * DenyDecorator adds functionality to Commands of type Deny. A better       *
 * solution should be found.                                                 *
 *                                                                           *
\*---------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LessThanOk.Network.Commands.Decorators
{
    class DenyDecorator:CommandDecorator
    {
        public DenyDecorator(Command cmd): base(cmd){ }
        /// <summary>
        /// Consturctor for the DenyDecorator
        /// </summary>
        /// <param name="ID">UnitID of the unit who's command was denied.</param>
        /// <param name="time">Time the request was denied.</param>
        /// <param name="cmd">Command for the decorator.</param>
        public DenyDecorator(UInt16 ID, long time, Command cmd)
            : base(cmd)
        {
            _command[0] |= ((UInt64)T_COMMAND.ERROR << 56);
            _command[0] |= ((UInt64)ID);
            _command[1] = (UInt64)time;
        }
        /// <summary>
        /// Add functionality.
        /// </summary>
        public override UInt16 UnitID { get { return (UInt16)(_command[0]); } }
    }
}
