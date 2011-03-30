using System;
using System.Collections.Generic;
using LessThanOk.Network.Commands;
using LessThanOk.GameData.GameObjects;
using LessThanOk.GameData.GameObjects.Units;
using LessThanOk.GameData.GameObjects.Tiles;
using Microsoft.Xna.Framework;

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
        }
		
		public void setGameTime(TimeSpan time){}
		public abstract void update(TimeSpan elps, List<Command> commands);

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
