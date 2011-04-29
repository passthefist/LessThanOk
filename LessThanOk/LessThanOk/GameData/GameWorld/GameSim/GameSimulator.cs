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
using LessThanOk.GameData.GameWorld.Events;

namespace LessThanOk.GameData.GameWorld.GameSim
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

        protected long gameTime;
		
		public event EventHandler unitAdded;
        public event EventHandler unitBuilt;
        public event EventHandler unitRemoved;
        public event EventHandler simulationStepped;

        public GameSimulator()
		{
			units = new List<Unit>();

			//commands to be executed
			instantCmds = new Queue<Command>();
			blockingCmds =  new Queue<Command>();
		}
        public void Initialize(TileMap tilemap)
        {
            //spatial organizing structures
            map = tilemap;
        }
        /// <summary>
        /// Distpatches the commands in preparation for stepping the simulation
        /// </summary>
        /// <param name="commandSet">The set of commands to execute</param>
		public void dispatchCommands(Queue<Command> commandSet)
        {
            foreach (Command c in commandSet)
            {
                if (c.isBlocking())
                {
					blockingCmds.Enqueue(c);
                }
                else
                {
                    instantCmds.Enqueue(c);
                }
            }
        }
		
        /// <summary>
        /// Step the simulation by the time.
        /// </summary>
        /// <param name="elps">The elapsed time since the last update</param>
        public void step(GameTime elps)
        {
            gameTime = elps.TotalGameTime.Ticks;

            preUpdate(elps);

            doInstantCommands(elps);
			
			updateUnits(elps);

            postUpdate(elps);
        }

        /// <summary>
        /// Check if a point is on the map
        /// </summary>
        /// <param name="point">The point to check</param>
        /// <returns>True if the point is on the map</returns>
		public bool isPointInMap(Vector2 point)
		{
            return map.isPointInMap(point);
		}
		
        /// <summary>
        /// Gets a tile at a point
        /// </summary>
        /// <param name="point">The position to use</param>
        /// <returns>the tile at that position</returns>
		public Tile getTileAtPoint(Vector2 point)
		{
			return map.getTileAtPoint(point);
		}

        /// <summary>
        /// Get a unit at a point. Used for things like
        /// clicking on a unit or verifying position
        /// </summary>
        /// <param name="point">The position to use</param>
        /// <returns>The Unit at that point or null</returns>
		public Unit getUnitAtPoint(Vector2 point)
		{
			foreach(Unit u in units)
			{
				if(Vector2.Distance(point, u.getPosition()) < (u.Sprite.Width))
				{
					return u;
				}
			}
			
			return null;
		}
		
        /// <summary>
        /// Get all the units in a rectangle. Used for things
        /// like rendering the visible units only.
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
		public List<Unit> getUnitsInRect(Rectangle rect)
		{
			Vector2 pos;
			List<Unit> found = new List<Unit>(rect.Width);
			foreach(Unit u in units)
			{
				pos = u.getPosition();
				if(pos.X >= rect.X && pos.Y >= rect.Y && pos.X + u.Sprite.Width < rect.X+rect.Width && pos.Y + 2*u.Sprite.Height < rect.Y + rect.Height)
				{
					found.Add(u);
				}
			}
			
			return found;
		}
		
        /// <summary>
        /// Get all the tiles in a given rectangle. Used for things
        /// like rendering the visible tiles only.
        /// </summary>
        /// <param name="rect">The rectangle to use.</param>
        /// <returns>A List of tiles in the rectangle</returns>
		public List<Tile> getTilesInRect(Rectangle rect)
		{
			return map.getTilesInRect(rect);
		}

        /// <summary>
        /// Get the simulator's tile Map.
        /// </summary>
        /// <returns>a TileMap that represents the current state of the map</returns>
        public TileMap getTileMap()
        {
            return map;
        }

        protected virtual void preUpdate(GameTime elps)
        {
           //initialize simulation step data, likely extentions
        }

        private void doInstantCommands(GameTime elps)
        {
             Command cmd;
            while (instantCmds.Count > 0)
            {
                cmd = instantCmds.Dequeue(); ;
                switch (cmd.CmdType)
                {
                    case Command.T_COMMAND.ADD:
                        handleAddUnitCommand(cmd);
                        break;
                    case Command.T_COMMAND.REMOVE:
                        handleRemoveCommand(cmd);
                        break;
                }
            }
        }

		/// <summary>
		/// Update all units. This funtion will execute all commands in
        /// the blocking queue
		/// </summary>
		/// <param name="elps"></param>
        protected void updateUnits(GameTime elps)
        {
            Unit u;
            Command cmd;
            while (blockingCmds.Count > 0)
            {
                cmd = blockingCmds.Dequeue();
                switch (cmd.CmdType)
                {
                    case Command.T_COMMAND.MOVE:
                        handleMoveCommand(cmd);
                        break;
                    case Command.T_COMMAND.ATTACK:
                        handleAttackCommand(cmd);
                        break;
                    //case Command.T_COMMAND.BUILD:
                    //   break;
                }
            }

            foreach (Unit unit in units)
            {
                unit.update(elps);
            }
        }

        protected virtual void postUpdate(GameTime elps)
        {
            //raiseSimulationStepped
        }

        protected virtual void handleAttackCommand(Command cmd)
        {

        }

        /// <summary>
        /// Handle a unit added command. This function should read
        /// the data out of the command and either:
        /// 1. Use it to create a new game object from the factory or
        /// 2. Use it to ressurect a game object from the factory
        /// </summary>
        /// <param name="addCommand"></param>
        protected void handleAddUnitCommand(Command cmd)
        {
            AddDecorator addCommand = new AddDecorator(cmd);
            Unit newUnit = createNewUnit(addCommand.UnitID,addCommand.Type, addCommand.ParentID);
            Unit adder = (Unit)GameObjectFactory.The.getGameObject(addCommand.ParentID);
            newUnit.setPosition(adder.getPosition());
            units.Add(newUnit);
        }

        protected virtual Unit createNewUnit(ushort toAddID, ushort type, ushort adderID)
        {
            return (Unit)GameObjectFactory.The.resurrectGameObject(toAddID, type);
        }

        private void handleRemoveCommand(Command cmd)
        {
            //No remove decorator yet
            //removeObjectFromWorld(ActiveGameObject toRemove)
        }

        protected virtual void removeObjectFromWorld(Unit toRemove)
        {
            units.Remove(toRemove);
        }

        protected virtual void handleMoveCommand(Command moveCommand)
        {
            MoveDecorator mov = new MoveDecorator(moveCommand);
            Vector2 position = new Vector2((float)mov.X, (float)mov.Y);

            Unit u = (Unit)GameObjectFactory.The.getGameObject(mov.UnitID);

            u.forceFinishAction();
            u.moveTo(position);
        }

        protected virtual void makeUnitIdle(Unit u)
        {
            u.forceFinishAction();
            u.idle();
        }
    }
}
