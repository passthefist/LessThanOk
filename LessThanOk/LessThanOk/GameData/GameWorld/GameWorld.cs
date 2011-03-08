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
		private TileMap      map;
		
		public void setGameTime(TimeSpan time){}
		public abstract void update(TimeSpan elps, List<Command> commands);
		
		public List<Tile> getTiles(Rectangle bounds)
        {
            return new List<Tile>();
        }
        public List<Unit> getUnits(Rectangle bounds)
        {
            return new List<Unit>() ;
        }
		public GameObject getObjectAt(Vector2 point)
        {
            return null;
        }
		public List<Unit> getAllUnits()
        {
            return units;
        }
	}
}

