using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using LessThanOk.GameData.GameObjects;
using LessThanOk.GameData.GameObjects.Units;
using LessThanOk.GameData.GameObjects.Tiles;
using LessThanOk.Network;
using LessThanOk.Network.Commands;
using LessThanOk.Network.Commands.Decorators;

namespace LessThanOk.GameData.GameWorld.GameSimulator
{
    public class GameSimulator
    {   
		//objects in world
        protected List<Unit> units;

		//commands to be executed
        protected Queue<Command> instantCmds;
		protected Queue<Command> blockingCmds;

		//spatial organizing structures
        //protected QuadTree<ActiveGameObject> gameSpace;
        protected TileMap map;
		
		//Changes to be done
		protected List<AdditionChange> addChanges;
		protected List<RemovalChange> removeChanges;
        //protected List<IdleChange> idleChanges;
        //protected List<MoveChange> moveChanges;
        //protected List<AttackChange> attackChanges;
		
		//History of set values
        protected Dictionary<KeyValuePair<UInt16,UInt16>, UInt32> setChanges;
		
		//Set value for health
		protected Dictionary<Unit,ushort> battleChanges;

        protected long gameTime;
		
		protected event EventHandler unitAdded;
        protected event EventHandler unitBuilt;
        protected event EventHandler unitRemoved;
        protected event EventHandler simulationStepped;

        public GameSimulator(TileMap tiles)
		{
            throw new NotImplementedException();
		}

		public void dispatchCommands(Queue<Command> commandSet)
        {
            throw new NotImplementedException();
        }
		
        public void step(GameTime elps)
        {
            throw new NotImplementedException();
        }
		/*
		public List<AdditionChange> collectAddChanges()
        {
            return addChanges;
        }

        public List<MoveChange> collectMoveChanges()
        {
            return moveChanges;
        }

        public List<SetValueChange> collectSetValues()
        {
            List<SetValueChange> sc = new List<SetValueChange>(setChanges.Count);
            foreach (KeyValuePair<KeyValuePair<UInt16, UInt16>, UInt32> kv in setChanges)
            {
                sc.Add(new SetValueChange(gameTime, kv.Key.Key, kv.Key.Value, kv.Value));
            }
            return sc;
        }

        public List<AttackChange> collectAttackChanges()
        {
            return attackChanges;
        }

        public List<RemovalChange> collectRemovalChanges()
        {
            return removeChanges;
        }

        public List<IdleChange> collectIdleChanges()
        {
            return idleChanges;
        }
        */
		public bool isPointInMap(Vector2 point)
		{
            return true;
		}
		
		public Tile getTileAtPoint(Vector2 point)
		{
            throw new NotImplementedException();
		}

		public Unit getUnitAtPoint(Vector2 point)
		{
            throw new NotImplementedException();
		}
		
		public List<Unit> getUnitsInRect(Rectangle rect)
		{
            throw new NotImplementedException();
		}
		
		public List<Tile> getTilesInRect(Rectangle rect)
		{
			return map.getTilesInRect(rect);
		}
		
		private void doInstantCommands(GameTime elps)
		{
            throw new NotImplementedException();
		}
		
		private void updateUnits(GameTime elps)
		{
            throw new NotImplementedException();
		}
		
		protected virtual void doBattle(GameTime elps)
		{
            throw new NotImplementedException();
		}

        protected virtual void pruneDeadUnits()
        {
        }

        protected virtual void handleAddUnitCommand(Command addCommand)
        {
            throw new NotImplementedException();
        }

        protected virtual void addObjectToWorld(ActiveGameObject toAdd, ActiveGameObject adder)
        {
            throw new NotImplementedException();
        }

        protected virtual ActiveGameObject aquireTarget(Vector2 point)
        {
            return null;
        }

        protected virtual void handleMoveCommand(Command moveCommand)
        {
            throw new NotImplementedException();
        }

        protected virtual void unitAttackObject(Unit u, ActiveGameObject target)
        {
            throw new NotImplementedException();
        }

        protected virtual void removeUnit(Unit deadUnit)
		{
            throw new NotImplementedException();
		}

        protected virtual void makeUnitIdle(Unit u)
        {
            throw new NotImplementedException();
        }

        protected virtual void setValue(GameObject targetObject, UInt16 key, UInt32 newValue)
		{
            throw new NotImplementedException();
		}
    }
}
