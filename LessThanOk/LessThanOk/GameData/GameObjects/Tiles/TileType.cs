/*---------------------------------------------------------------------------*\
 *                         LessThanOK Engine                                 *
 *                                                                           *
 *          Copyright (C) 2011-2012 by Robert Goetz, Anthony Lobono          *
 *                                                                           *
 *   authors:  Robert Goetz (rdgoetz@iastate.edu)                            *
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
 * The type of Tile.                                                         *
 *                                                                           *
 * See GameObject, GameObjectType, GameObjectFactory, Tile, TileMap          *
 *                                                                           *
\*---------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Xna.Framework;
using LessThanOk.Sprites;
using LessThanOk.GameData.GameObjects;

namespace LessThanOk.GameData.GameObjects.Tiles
{
    /// <summary>
    /// The type of tile.
    /// </summary>
    public class TileType : GameObjectType
    {

        private Sprite image;

        static TileType()
        {
            AgnosticObject.initFieldMaps(typeof(TileType));
        }

        /// <summary>
        /// Create a tile type.
        /// </summary>
        /// <param name="maxspeed">
        /// Maximum Speed <see cref="System.Single"/>
        /// </param>
        /// <param name="acceleration">
        /// acceleration rate <see cref="System.Single"/>
        /// </param>
        /// <param name="deceleration">
        /// breaking rate <see cref="System.Single"/>
        /// </param>
        public TileType(Sprite tileImage)
        {
            image = tileImage;

            protoType = new Tile(this);
        }

        /// <summary>
        /// Create an engine of this type.
        /// </summary>
        /// <returns>
        /// A <see cref="GameObject"/>
        /// </returns>
        override public GameObject create()
        {
            return new Tile((Tile)protoType);
        }
        public Sprite getImage() { return image; }
    }
}