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
        protected List<IdleChange> idleChanges;
        protected List<MoveChange> moveChanges;
        protected List<AttackChange> attackChanges;
		
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
			units = new List<Unit>();

			//commands to be executed
			instantCmds = new Queue<Command>();
			blockingCmds =  new Queue<Command>();
			
			//spatial organizing structures
			//gameSpace = new QuadTree<ActiveGameObject>();
			map = tiles;
		
			//Changes to be done
			addChanges = new List<AdditionChange>();
			removeChanges = new List<RemovalChange>();
		
			//History of set values steppced
            setChanges = new Dictionary<KeyValuePair<UInt16, UInt16>, UInt32>();
		
			//Set value for health, efficient damage calc.
			battleChanges = new Dictionary<Unit,ushort>();
		}

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

            addChanges.Clear();
            removeChanges.Clear();
            setChanges.Clear();
            battleChanges.Clear();
        }
		
        public void step(GameTime elps)
        {
            gameTime = elps.TotalGameTime.Ticks;

            doInstantCommands(elps);
			
			updateUnits(elps);
			
			doBattle(elps);

            pruneDeadUnits();
        }
		
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

		public bool isPointInMap(Vector2 point)
		{
            return map.isPointInMap(point);
		}
		
		public Tile getTileAtPoint(Vector2 point)
		{
			return map.getTileAtPoint(point);
		}

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
		
		public List<Tile> getTilesInRect(Rectangle rect)
		{
			return map.getTilesInRect(rect);
		}
		
		private void doInstantCommands(GameTime elps)
		{
            Command cmd;
            while (instantCmds.Count > 0)
            {
				cmd = instantCmds.Dequeue();
                switch (cmd.CmdType)
                {
                    case Command.T_COMMAND.ADD:
                        handleAddUnitCommand(cmd);

                        break;
                    case Command.T_COMMAND.CANCEL:
                        //Command_Cancel cCan = (Command_Cancel)cmd;
                        //Unit u = (Unit)fact.getGameObject(cCan.getID());
                        //u.clearCommands();
                        break;
                    case Command.T_COMMAND.REMOVE:
                        break;
                    case Command.T_COMMAND.SET:
                        break;
                    case Command.T_COMMAND.ERROR:
                        break;
                    case Command.T_COMMAND.MOVE:
                        /* Command_Move cMove = (Command_Mov)cmd;
                         *
                         * Unit u = (Unit)fact.getGameObject(cMove.getID());
                         * u.addCommand(cMove);
                         */
                        break;
                    default:
                        break;
                }
            }
		}
		
		private void updateUnits(GameTime elps)
		{
			Unit u;
            Command cmd;
			while(blockingCmds.Count > 0)
			{
				cmd = instantCmds.Dequeue();
				u = (Unit)GameObjectFactory.The.getGameObject(cmd.UnitID);
                switch (cmd.CmdType)
                {
                    case Command.T_COMMAND.MOVE:
                        break;
                    case Command.T_COMMAND.ATTACK:
                        break;
                    case Command.T_COMMAND.ADD:
                        break;
                }
			}
			
            foreach(Unit unit in units)
            {
                unit.update(elps);
            }
		}
		
		protected virtual void doBattle(GameTime elps)
		{
			foreach(Unit u in units)
			{
                if (u.isAggressive())
                {
                    if (u.Target == null)
                    {
                        u.setTarget(aquireTarget(u.getPosition()));
                        if (u.Target == null)
                        {
                            continue;
                        }
                    }
                    if (u.Target.Health > 0)
                    {
                        if (u.MainWeapon.canFireOn(u.Target))
                        {
                        }
                    }
                    else
                    {
                        u.idle();
                    }
                }
			}
		}

        protected virtual void pruneDeadUnits()
        {
        }

        protected virtual void handleAddUnitCommand(Command addCommand)
        {
            AddDecorator cmd = new AddDecorator(addCommand);
            Unit u = (Unit)GameObjectFactory.The.createGameObject(cmd.Type);
            Unit parent = (Unit)GameObjectFactory.The.getGameObject(cmd.ParentID);

            u.setPosition(parent.getPosition());
            //addCommand.get intended owner

            addChanges.Add(new AdditionChange(addCommand.TimeStamp, u, parent, parent.Owner));
        }

        protected virtual void addObjectToWorld(ActiveGameObject toAdd, ActiveGameObject adder)
        {

        }

        protected virtual ActiveGameObject aquireTarget(Vector2 point)
        {
            return null;
        }

        protected virtual void handleMoveCommand(Command moveCommand)
        {
            MoveDecorator mov = new MoveDecorator(moveCommand);
            Vector2 position = new Vector2((float)mov.X, (float)mov.Y);

            Unit u = (Unit)GameObjectFactory.The.getGameObject(mov.UnitID);

            u.forceFinishAction();
            u.moveTo(position);

            moveChanges.Add(new MoveChange(moveCommand.TimeStamp, u, position));
        }

        protected virtual void unitAttackObject(Unit u, ActiveGameObject target)
        {
            u.setTarget(target);
            attackChanges.Add(new AttackChange(gameTime, u, target));
        }

        protected virtual void removeUnit(Unit deadUnit)
		{
			//gameSpace.Remove(deadUnit);
			units.Remove(deadUnit);
            removeChanges.Add(new RemovalChange(gameTime, deadUnit));
		}

        protected virtual void makeUnitIdle(Unit u)
        {
            u.forceFinishAction();
            u.idle();
            idleChanges.Add(new IdleChange(gameTime, u));
        }

        protected virtual void setValue(GameObject targetObject, UInt16 key, UInt32 newValue)
		{
            targetObject.setField(key, newValue);

            KeyValuePair<UInt16, UInt16> entry = new KeyValuePair<ushort, ushort>(targetObject.ID, key);
            uint value;

            if (setChanges.TryGetValue(entry,out value))
            {
                setChanges.Remove(entry);
                setChanges.Add(entry, newValue);
            }
            else
            {
                setChanges.Add(entry, newValue);
            }
		}
    }
}
