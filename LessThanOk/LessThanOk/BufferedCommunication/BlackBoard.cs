using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LessThanOk.GameData.GameWorld;

namespace LessThanOk.BufferedCommunication
{
    class BlackBoard
    {
        static readonly BlackBoard the = new BlackBoard();
        static BlackBoard() 
        {
            _map = new TileMap();
        }
        public static BlackBoard The { get { return the; } }

        private static TileMap _map;
       
        public static bool updateTileMap(TileMap map)
        {
            _map = new TileMap(map);
            return true;
        }
        public static bool getTileMap(out TileMap map)
        {
            if (_map == null)
            {
                map = null;
                return false;
            }
            map = _map;
            return true;
        }
    }
}
