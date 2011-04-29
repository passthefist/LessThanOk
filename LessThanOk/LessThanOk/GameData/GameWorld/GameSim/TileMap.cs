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

        public bool isPointInMap(Vector2 point)
        {
            return !(point.X < 0 || point.Y < 0 || point.X >= tileSize*width || point.Y >= tileSize*height);
        }

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

        public Tile getTileAtPoint(int i, int j)
        {
            uint x = mapX((float)i);
            uint y = mapY((float)j);

            return tileMap[x, y];
        }

        public Tile getTileAtPoint(Vector2 point)
        {
            uint x = mapX(point.X);
            uint y = mapY(point.Y);

            return tileMap[x, y];
        }

        public Tile getClosestEmptyTile(Vector2 position,float maxDistance)
        {
            Tile start = getTileAtPoint(position);
            if (!start.Walkable)
            {
                //start = getTileAtPoint(position + Vector2
            }
            throw new NotImplementedException();
        }

        public List<Unit> getUnitsOnLine(Vector2 start, Vector2 end, int maxNumberOfUnits)
        {
            throw new NotImplementedException();
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
