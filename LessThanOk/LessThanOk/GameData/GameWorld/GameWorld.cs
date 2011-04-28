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
        protected GameTime gameTime;
		protected TileMap      map;

        public GameWorld()
        {
            units = new List<Unit>(12);
            gameTime = new GameTime();
            map = new TileMap();
            fact = GameObjectFactory.The;
        }
		
		public void setGameTime(TimeSpan time){}
		public abstract void update(GameTime elps);

        protected void UpdateUnits()
        {
            foreach(Unit u in units)
            {
                u.update(gameTime);
            }
        }

        protected void ConstructTileMap()
        {
            map.clear();

            foreach (Unit unit in units)
            {
                map.placeUnit(unit);
                //				unit.addCommand();
            }

            //BlackBoard.updateTileMap(map);
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
