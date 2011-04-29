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
 * MoveDecorator adds functionality to Commands of type Move.  A Better      *
 * Solution should be found.                                                 *
 *                                                                           *
\*---------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LessThanOk.Network.Commands.Decorators
{
    class MoveDecorator : CommandDecorator
    {
        public MoveDecorator(Command cmd) : base(cmd) { }
        /// <summary>
        /// Constructor for MoveDecorator
        /// </summary>
        /// <param name="unit">UnitID for the Unit that is moving</param>
        /// <param name="x">Possition to move to.</param>
        /// <param name="y">Possition to move to.</param>
        /// <param name="time">Time the unit needs to be at the possition.</param>
        /// <param name="cmd">Command to be Decorated.</param>
        public MoveDecorator(UInt16 unit, UInt16 x, UInt16 y, float time, Command cmd)
            : base(cmd)
        {
            _command[0] |= ((UInt64)T_COMMAND.MOVE << 56);
            _command[0] |= ((UInt64)unit << 32);
            _command[0] |= ((UInt64)x << 16);
            _command[0] |= ((UInt64)y);
            _command[1] = (UInt64)time;
        }
        /// <summary>
        /// Add functionality.
        /// </summary>
        public override UInt16 UnitID { get { return (UInt16)(_command[0] >> 32); } }
        /// <summary>
        /// Add functionality.
        /// </summary>
        public override UInt16 X { get { return (UInt16)(_command[0] >> 16); } }
        /// <summary>
        /// Add functionality.
        /// </summary>
        public override UInt16 Y { get { return (UInt16)(_command[0]); } }
    }
}
