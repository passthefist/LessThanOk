using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using LessThanOk.GameData.GameObjects;
using LessThanOk.GameData.GameObjects.Tiles;
using LessThanOk.GameData.GameObjects.Units;

namespace LessThanOk.GameData.GameWorld
{
    public class TileMap
    {
        Tile[,] tileMap;
//        QuadNode [][] quadMap;
        private uint width;
        public uint Width { get; private set; }
        private uint height;
        public uint Height { get; private set; }

        private byte tileSize;

        public const uint MAXIMUM_MAP_SIZE = 16384;

        public TileMap()
        {
            width = 800 / 20;
            height = 600 / 20;
            tileSize = 20;
        }

        public GameObject getObjectAtPoint(Vector2 point)
        {
            uint x;
            uint y;
            y = (uint)point.Y;
            x = (uint)point.X;
            y = y / height;
            x = x % width;

            Tile tile = tileMap[x, y];
            if (tile.HasUnits)
            {
                return tile.InternalUnits[0];
            }
            else
            {
                return tile;
            }
        }

        public TileMap(uint width, uint height, byte tileSize)
        {
            if (width * height > 16384)
            {
                throw new Exception("You Suck At Life.");
            }

            this.width = width;
            this.height = height;
            this.tileSize = tileSize;

            tileMap = new Tile[width,height];

            for(int i = 0; i < height * width; i++)
            {
                tileMap[i%width,i/height]._Position = new Vector2(i%width,i/height);
            }
        }

        public List<Tile> getTilesInRect(Rectangle rect)
        {
            List<Tile> tiles = new List<Tile>(rect.Width*rect.Height);

            for (int i = rect.Left; i < rect.Right; i += tileSize)
            {
                for(int j = rect.Top; j < rect.Bottom; j+= tileSize)
                {
                    tiles.Add(tileMap[i % width,j / height]);
                }
            }

            return tiles;
        }

        public List<Unit> getUnitsInRect(Rectangle rect)
        {
            List<Tile> tiles = getTilesInRect(rect);
            List<Unit> units = new List<Unit>(rect.Width * rect.Height);

            foreach (Tile t in tiles)
            {
                if(t.HasUnits)
                {
                    foreach (Unit u in t.InternalUnits)
                    {
                        units.Add(u);
                    }
                }
            }

            return units;
        }

        public void rebuild()
        {
            foreach (Tile t in tileMap)
            {
                t.clear();
            }
        }

        public void placeUnit(Unit u)
        {
            uint x = (uint)u._Position.X;
            uint y = (uint)u._Position.Y;

            tileMap[x % width, y / height].addUnit(u);
        }
    }
}
