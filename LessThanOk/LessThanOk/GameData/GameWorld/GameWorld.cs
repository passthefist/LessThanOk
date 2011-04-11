using System;
using System.Collections.Generic;
using LessThanOk.Network.Commands;
using LessThanOk.GameData.GameObjects;
using LessThanOk.GameData.GameObjects.Units;
using LessThanOk.GameData.GameObjects.Tiles;
using Microsoft.Xna.Framework;
using LessThanOk.BufferedCommunication;

namespace LessThanOk.GameData.GameWorld
{
	public abstract class GameWorld
	{
		protected GameObjectFactory fact;
		
		protected List<Unit>   units;
//		protected List<Command>cmdBuffer;
		protected TimeSpan     gameTime;
		protected TileMap      map;

        public GameWorld()
        {
            units = new List<Unit>(12);
            gameTime = new TimeSpan();
            map = new TileMap();
            fact = GameObjectFactory.The;
            update(new TimeSpan());
        }
		
		public void setGameTime(TimeSpan time){}
		public abstract void update(TimeSpan elps);

        protected void ConstructTileMap()
        {
            BlackBoard.updateTileMap(map);
        }

        public TileMap getTileMap()
        {
            return map;
        }

		public List<Unit> getAllUnits()
        {
            return units;
        }
	}
}
