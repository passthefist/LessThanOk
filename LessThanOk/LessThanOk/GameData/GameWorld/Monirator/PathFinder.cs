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
 * PathFinder's repsonsibility is to find a set of wayponts to get from one  *
 * point on the TileMap the another.                                         *
 *                                                                           *
\*---------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Net;

namespace LessThanOk.GameData.GameWorld.Monirator
{
    class PathFinder
    {
        /// <summary>
        /// Get a set of waypoints given an origin, destination, and a TileMap.
        /// </summary>
        /// <param name="origin">Starting point.</param>
        /// <param name="dest">Final destination.</param>
        /// <param name="map">TileMap to path find through.</param>
        /// <returns>Set of waypoints.</returns>
        internal List<Vector2> GetPath(Vector2 origin, Vector2 dest, TileMap map)
        {
            List<Vector2> retval = new List<Vector2>();
            // TODO: return path based on player.
            return retval;
        }
    }
}
