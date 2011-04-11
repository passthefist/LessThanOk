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
        static BlackBoard() { }
        public static BlackBoard The { get { return the; } }

        private static TileMap _map;
       
        public static bool updateTileMap(ref TileMap map)
        {
            _map = map;
            map = null;
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
