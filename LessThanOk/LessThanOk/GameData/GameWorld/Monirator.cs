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
 * Monirator is responsible for processing and validating player requests.   *
 * It is also responsible for processing world changes and constructing      *
 * command to be sent out to all clients notifying them of the changes. This *
 * is how we are able to sync the host game to all the client games.         *
 *                                                                           *   
 * See: Commands.cs Command_Add.cs Command_Cancel.cs Command_Note.cs         *
 *      Command_Set.cs                                                       *
 *                                                                           *
\*---------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using LessThanOk.Network.Commands;
using LessThanOk.GameData;
using LessThanOk.GameData.GameWorld;

namespace LessThanOk.GameData.GameWorld
{
    public sealed class Monirator
    {
        static readonly Monirator the = new Monirator();
      
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Monirator(){}

        public static Monirator The { get { return the; } }

        private Queue<Command> grants;
        private Queue<Command> requests;
        /// <summary>
        /// Default constructor that sets the queue lengths to 100
        /// </summary>
        public Monirator()
        {
            grants = new Queue<Command>(100);
            requests = new Queue<Command>(100);
        }
        /// <summary>
        /// Tests if a command is valid.
        /// </summary>
        /// <param name="req"></param>
        /// <param name="board"></param>
        /// <returns></returns>
        public Boolean validate(Command req, TileMap board)
        {
            // TODO: Validation Logic
            return true;
        }
    }
}
