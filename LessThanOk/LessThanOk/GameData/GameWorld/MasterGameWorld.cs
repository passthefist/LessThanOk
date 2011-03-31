using System;
using System.Collections.Generic;
using LessThanOk.Network.Commands;
using Microsoft.Xna.Framework;
using LessThanOk.GameData.GameObjects.Units;
using LessThanOk.GameData.GameObjects;
using LessThanOk.BufferedCommunication;

namespace LessThanOk.GameData.GameWorld
{
	public class MasterGameWorld : GameWorld
	{	 
//		private List<object> notifications;
//		private List<Path> paths;
		
		private void addUnit(Unit newUnit, GameObject builder)
		{
            AdditionChange add = new AdditionChange(gameTime, newUnit, builder);
            ChangeList.pushAdd(ref add);
			units.Add(newUnit);
		}
		
		private void removeUnit(Unit deadUnit)
		{
            RemovalChange rem = new RemovalChange(gameTime, deadUnit);
            ChangeList.pushRem(ref rem);
			units.Remove(deadUnit);
		}
		
		private void setValue(GameObject targetObject, UInt16 key, UInt32 newValue)
		{
            SetValueChange set = new SetValueChange(gameTime, targetObject, new KeyValuePair<UInt16, UInt32>(key, newValue));
            ChangeList.pushSet(ref set);
            targetObject.setField(key, newValue);
		}
//		private void findPath();
//		private void useAbility();

        public MasterGameWorld()
            : base()
		{
		}

        override public void update(TimeSpan gameTime, List<Command> commands)
        {
            GameObjectFactory fact = GameObjectFactory.The;
            foreach (Command cmd in commands)
            {
                switch (cmd.getCommandType())
                {
                    case Command.T_COMMAND.ADD:
                        Command_Add cAdd = (Command_Add)cmd;
                        Unit newUnit = (Unit)fact.createGameObject(cAdd.getType());
                        ActiveGameObject builder = (ActiveGameObject)fact.getGameObject(cAdd.getBuilder());

                        newUnit._Position = builder._Position;
                        addUnit(newUnit, builder);

                        break;
                    case Command.T_COMMAND.CANCEL:
                        break;
                    case Command.T_COMMAND.REMOVE:
                        break;
                    case Command.T_COMMAND.SET:
                        break;
                    case Command.T_COMMAND.ERROR:
                        break;
                    default:
                        break;
                }
            }

            map.clear();

            foreach (Unit unit in units)
            {
                map.placeUnit(unit);
//				unit.addCommand();
            }
        }
	}
}
