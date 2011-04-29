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
 * AttackDecorator adds functionality to a Command of Type Attack. A better  *
 * solution should be found.                                                 *
 *                                                                           *
\*---------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LessThanOk.Network.Commands.Decorators
{
    class AttackDecorator: CommandDecorator
    {
        public AttackDecorator(Command cmd) : base(cmd) { }
        /// <summary>
        /// Constructor for the AttackDecorator.
        /// </summary>
        /// <param name="unit">UnitID of the attacking unit.</param>
        /// <param name="target">UnitID of the target.</param>
        /// <param name="time">Time the Attack needs to be finished.</param>
        /// <param name="cmd">Command for the decorator.</param>
        public AttackDecorator(UInt16 unit, UInt16 target, TimeSpan time, Command cmd)
            : base(cmd)
        {
            _command[0] |= ((UInt64)T_COMMAND.ATTACK << 56);
            _command[0] |= ((UInt64)unit << 16);
            _command[0] |= ((UInt64)target);
            _command[1] = (UInt64)time.Ticks;
        }
        /// <summary>
        /// Add functionality.
        /// </summary>
        public override UInt16 UnitID { get { return (UInt16)(_command[0] >> 16); } }
        /// <summary>
        /// Add functionality.
        /// </summary>
        public override UInt16 Target { get { return (UInt16)(_command[0]); } }
    }
}
