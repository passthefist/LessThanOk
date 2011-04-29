/*---------------------------------------------------------------------------*\
*                         LessThanOK Engine                                  *
*                                                                            *
*          Copyright (C) 2011-2012 by Robert Goetz, Anthony Lobono           *
*                                                                            *
*          authors:  Anthony LoBono (ajlobono@gmail.com)                     *
*                                                                            *
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
 * EventArgs for the Request Denied Event. Contains the UnitID for the unit  *
 * whom the command was intended for, and the player who owns the Unit.      *
 * Event is thrown by CommandValidator and should be caught by               *
 * NetworkManager and writen to the network.                                 *
 *                                                                           *
\*---------------------------------------------------------------------------*/
         
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LessThanOk.GameData.GameWorld.Monirator.Events
{
    public class RequestDeniedEventArgs : EventArgs
    {
        public UInt16 UnitID { get { return _unitID; } }
        public Player Player { get { return _player; } }
        
        private UInt16 _unitID;
        private Player _player;

        /// <summary>
        /// Contstructor for RequestDeniedEventArgs.
        /// </summary>
        /// <param name="unitID">Unit whom the command was intended for.</param>
        /// <param name="player">Player who owns the unit.</param>
        public RequestDeniedEventArgs(UInt16 unitID, Player player)
        {
            _unitID = unitID;
            _player = player;
        }
    }
}
