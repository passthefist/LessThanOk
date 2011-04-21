using System;
using System.Collections.Generic;
using LessThanOk.Network.Commands;
using Microsoft.Xna.Framework;
using LessThanOk.GameData.GameObjects.Units;
using LessThanOk.GameData.GameObjects;
using LessThanOk.BufferedCommunication;
using LessThanOk.Events;

namespace LessThanOk.GameData.GameWorld
{
	public class MasterGameWorld : GameWorld
	{	 
//		private List<object> notifications;
//		private List<Path> paths

		private void addUnit(Unit newUnit, ActiveGameObject builder)
		{
            AdditionChange add = new AdditionChange(gameTime.TotalGameTime, newUnit, builder);
            ChangeList.pushAdd(ref add);

            newUnit.CommandStarted += new EventHandler<CommandChangedEventArgs>(this.onUnitCommandStarted);
            newUnit.CommandFinished += new EventHandler<CommandChangedEventArgs>(this.onUnitCommandFinished);

			units.Add(newUnit);
		}

        private void moveUnitTo(Unit u, Vector2 position)
        {
            float finishTimeInMillis = u.Engine.timeToReach(position)*1000;

            TimeSpan finish = new TimeSpan(0, 0, 0, 0, (int)finishTimeInMillis);
            finish += gameTime.TotalGameTime;

            Console.WriteLine(finish);

            Command_Move m = new Command_Move(u.ID, (ushort)position.X, (ushort)position.Y, finish);
            u.addCommand(m);
        }
		
		private void removeUnit(Unit deadUnit)
		{
            RemovalChange rem = new RemovalChange(gameTime.TotalGameTime, deadUnit);
            ChangeList.pushRem(ref rem);
			units.Remove(deadUnit);
		}
		
		private void setValue(GameObject targetObject, UInt16 key, UInt32 newValue)
		{
            SetValueChange set = new SetValueChange(gameTime.TotalGameTime, targetObject, new KeyValuePair<UInt16, UInt32>(key, newValue));
            ChangeList.pushSet(ref set);
            targetObject.setField(key, newValue);
		}
//		private void findPath();
//		private void useAbility();

        public MasterGameWorld()
            : base()
		{
		}

        override public void update(GameTime elps)
        {
            gameTime = elps;

            Command cmd;
            while((cmd = GlobalRequestQueue.The.poll()) != null)
            {
                switch (cmd.getCommandType())
                {
                    case Command.T_COMMAND.ADD:
                        Command_Add cAdd = (Command_Add)cmd;
                        //consult monirator
                        Unit newUnit = (Unit)fact.createGameObject(cAdd.getType());
                        ActiveGameObject builder = (ActiveGameObject)fact.getGameObject(cAdd.getBuilder());

                        newUnit._Position = builder._Position;

                        addUnit(newUnit, builder);

                        Vector2 point = new Vector2((float)LessThanMath.random(100, 500), (float)LessThanMath.random(100, 300));

                        moveUnitTo(newUnit, point);

                        break;
                    case Command.T_COMMAND.CANCEL:
                        Command_Cancel cCan = (Command_Cancel)cmd;

                        Unit u = (Unit)fact.getGameObject(cCan.getID());
                        u.clearCommands();
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

            foreach(Unit u in units)
            {
                u.update(elps);
            }

            UpdateUnits();
            ConstructTileMap();

            List<Unit> targets = new List<Unit>();
            Dictionary<KeyValuePair<Unit,UInt16>,UInt32> setUnits = new Dictionary<KeyValuePair<Unit,ushort>,uint>();
            UInt16 setField;

            foreach (Unit unit in units)
            {
                if (unit.Target == null)
                {
                    targets = map.getUnitsInCirc(new Point((int)unit._Position.X, (int)unit._Position.Y), 250);
                    if(targets.Count > 0)
                    {
                        unit.Target = targets[0];
                    }
                }

                setField = unit.Target.getFieldID("health");
                
                (unit.Target as Unit).health--;

                if ((unit.Target as Unit).health < 0)
                {
                    setUnits.Remove(new KeyValuePair<Unit, ushort>(unit, setField));
                    //removeUnit(unit.Target);
                }
                else
                {
                    setUnits.Add(new KeyValuePair<Unit, ushort>(unit, setField), (unit.Target as Unit).health);
                }
            }

            foreach (KeyValuePair<KeyValuePair<Unit, ushort>, UInt32> kv in setUnits)
            {
                setValue(kv.Key.Key, kv.Key.Value, kv.Value);
            }
        }

        private void onUnitCommandStarted(object sender, CommandChangedEventArgs e)
        {
            Console.WriteLine("START TIME:" + gameTime.TotalGameTime);
        }

        private void onUnitCommandFinished(object sender, CommandChangedEventArgs e)
        {
            Console.WriteLine("FINISH TIME:" + gameTime.TotalGameTime);
        }
	}
}
