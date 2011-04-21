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
            height = 400 / 20;
            tileSize = 20;

            tileMap = new Tile[width, height];

            for (int i = 0; i < height*width; i++)
            {
                if((i%width)%2 == 0)
                    tileMap[i % width, i / width] = (Tile)GameObjectFactory.The.createGameObject("grassTile");
                else
                    tileMap[i % width, i / width] = (Tile)GameObjectFactory.The.createGameObject("yellowTile");
                tileMap[i % width, i / width]._Position = new Vector2(tileSize*(i%width), tileSize*(i/width));
            }
        }
        public TileMap(TileMap nmap)
        {
            this.height = nmap.height;
            this.width = nmap.width;
            this.tileSize = nmap.tileSize;
            this.tileMap = new Tile[nmap.width, nmap.height];
            for (int i = 0; i < nmap.width; i++)
                for (int j = 0; j < nmap.height; j++)
                    this.tileMap[i, j] = new Tile(nmap.tileMap[i, j]);
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
                tileMap[i % width, i / width] = (Tile)GameObjectFactory.The.createGameObject("grASSTile");
                tileMap[i%width, i/width]._Position = new Vector2(tileSize*(i%width),tileSize*(i/width));
            }

        }

        public List<Tile> getTilesInRect(Rectangle rect)
        {
            List<Tile> tiles = new List<Tile>(rect.Width*rect.Height);

            for (int i = rect.Left; i < rect.Right; i += tileSize)
            {
                for(int j = rect.Top; j < rect.Bottom; j+= tileSize)
                {
                    if(i/tileSize >= 0 && j/tileSize >= 0)
                        if(i/tileSize < width && j/tileSize < height)
                            tiles.Add(tileMap[i/tileSize ,j/tileSize]);
                }
            }

            return tiles;
        }

        public ActiveGameObject getObjectAtPoint(Vector2 point)
        {
            //TODO: structure should support 
            uint x;
            uint y;
            y = (uint)point.Y;
            x = (uint)point.X;

            y = y / tileSize;
            x = x / tileSize;

            if (y < 0 || y >= height || x < 0 || x >= width)
                return null;

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
        
        public List<Unit> getUnitsInCirc(Point center, int radius)
        {
            List<Tile> tiles = getTilesInRect(new Rectangle(center.X - radius, center.Y - radius,2*radius,2*radius));
            List<Unit> units = new List<Unit>(radius/4);

            foreach (Tile t in tiles)
            {
                if (t.HasUnits)
                {
                    foreach (Unit u in t.InternalUnits)
                    {
                        if (LessThanMath.approxDist((int)u._Position.X, (int)u._Position.Y, center.X, center.Y) < radius)
                        {
                            units.Add(u);
                        }
                    }
                }
            }

            return units;
        }

        public void clear()
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

            tileMap[x / tileSize, y / tileSize].addUnit(u);
        }
    }
}
