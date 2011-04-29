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
 *  This Class represents the map of the world.                              *
\*---------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using LessThanOk.GameData.GameObjects;
using LessThanOk.GameData.GameObjects.Tiles;
using LessThanOk.GameData.GameObjects.Units;
using System.Windows.Forms;

namespace LessThanOk.GameData.GameWorld
{
    public class TileMap
    {
        Tile[,] tileMap;

        private uint width;
        private uint height;

        private byte tileSize;

        public const uint MAXIMUM_MAP_SIZE = 16384;


        /// <summary>
        /// Construct a TileMap with default parameters
        /// </summary>
        public TileMap()
        {
            width = 800 / 20;
            height = 400 / 20;
            tileSize = 20;

            tileMap = new Tile[width, height];

            for (int i = 0; i < height * width; i++)
            {
                if ((i % width) % 2 == 0)
                    tileMap[i % width, i / width] = (Tile)GameObjectFactory.The.createGameObject("grassTile");
                else
                    tileMap[i % width, i / width] = (Tile)GameObjectFactory.The.createGameObject("yellowTile");
                tileMap[i % width, i / width].setPosition(new Vector2(tileSize * (i % width), tileSize * (i / width)));
            }
        }

        /// <summary>
        /// Construct a TileMap with a set size
        /// </summary>
        /// <param name="width">The width the map</param>
        /// <param name="height">The height of the map</param>
        /// <param name="tileSize">the size of a tile</param>
        public TileMap(uint width, uint height, byte tileSize)
        {
            if (width * height > MAXIMUM_MAP_SIZE)
            {
                throw new Exception("You Suck At Life.");
            }

            this.width = width;
            this.height = height;
            this.tileSize = tileSize;

            tileMap = new Tile[width, height];

            for (int i = 0; i < height * width; i++)
            {
                if ((i % width) % 2 == 0)
                    tileMap[i % width, i / width] = (Tile)GameObjectFactory.The.createGameObject("grassTile");
                else
                    tileMap[i % width, i / width] = (Tile)GameObjectFactory.The.createGameObject("yellowTile");
                tileMap[i % width, i / width].setPosition(new Vector2(tileSize * (i % width), tileSize * (i / width)));
            }
        }

        /// <summary>
        /// Checks if a point is outside the map
        /// </summary>
        /// <param name="point">The position to check</param>
        /// <returns>True if the position is on the map</returns>
        public bool isPointInMap(Vector2 point)
        {
            return !(point.X < 0 || point.Y < 0 || point.X >= tileSize*width || point.Y >= tileSize*height);
        }

        /// <summary>
        /// Get all the tiles in a given rectangle. Used for things
        /// like rendering the visible tiles only.
        /// </summary>
        /// <param name="rect">The rectangle to use.</param>
        /// <returns>A List of tiles in the rectangle</returns>
        public List<Tile> getTilesInRect(Rectangle rect)
        {
            //Constrain Rectangle to be on the map
            if (rect.X < 0)
            {
                rect.Width += rect.X;
                rect.X = 0;
            }
            if (rect.Y < 0)
            {
                rect.Height += rect.Y;
                rect.Y = 0;
            }
            if (rect.X + rect.Width > tileSize*width)
            {
                rect.Width = (int)(tileSize* width) - rect.X;
            }
            if (rect.Y + rect.Height > tileSize * height)
            {
                rect.Y = (int)(tileSize* height)- rect.Y;
            }

            //Get all them tiles.

            List<Tile> tiles = new List<Tile>(rect.Width*rect.Height);

            for (int i = rect.Left; i < rect.Right; i += tileSize)
            {
                for(int j = rect.Top; j < rect.Bottom; j+= tileSize)
                {
                    tiles.Add(getTileAtPoint(i, j));
                }
            }

            return tiles;
        }

        /// <summary>
        /// Gets a tile at a given x and y coordinate
        /// </summary>
        /// <param name="i">The x position to check</param>
        /// <param name="j">The y position to check</param>
        /// <returns>The Tile at that position</returns>
        public Tile getTileAtPoint(int i, int j)
        {
            uint x = mapX((float)i);
            uint y = mapY((float)j);

            return tileMap[x, y];
        }

        /// <summary>
        /// Gets a tile at a given position
        /// </summary>
        /// <param name="point">The position to check</param>
        /// <returns>The Tile at that position</returns>
        public Tile getTileAtPoint(Vector2 point)
        {
            uint x = mapX(point.X);
            uint y = mapY(point.Y);

            return tileMap[x, y];
        }

        /// <summary>
        /// Gets the closest empty tile to the given position, not further
        /// than the given distance. OTherwise null.
        /// </summary>
        /// <param name="position">The position to use</param>
        /// <param name="maxDistance">The maximum distance away</param>
        /// <returns>The Tile found or null if all tiles are taken.</returns>
        public Tile getClosestEmptyTile(Vector2 position,float maxDistance)
        {
            return getTileAtPoint(position);
        }

        /// <summary>
        /// Checks if the given path is un Obstructed
        /// </summary>
        /// <param name="start">The start position</param>
        /// <param name="end">The end position</param>
        /// <returns>if the path between the points has anything on it</returns>
        public bool hasUnitsInPath(Vector2 start, Vector2 end)
        {
            return false;
        }

        public List<Unit> getUnitsOnLine(Vector2 start, Vector2 end, int maxNumberOfUnits)
        {
            List<Unit> units = new List<Unit>();
            return units;
        }

        private uint mapX(float xPos)
        {
            return (uint)(xPos / tileSize);
        }

        private uint mapY(float yPos)
        {
            return (uint)(yPos / tileSize);
        }
    }
}
