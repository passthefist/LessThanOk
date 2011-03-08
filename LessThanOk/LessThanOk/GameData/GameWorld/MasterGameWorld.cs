using System;
using System.Collections.Generic;
using LessThanOk.Network.Commands;
using Microsoft.Xna.Framework;
using LessThanOk.GameData.GameObjects.Units;
using LessThanOk.GameData.GameObjects;

namespace LessThanOk.GameData.GameWorld
{
	public class MasterGameWorld : GameWorld
	{	
		private List<Unit> unitsAdded;
		public  List<Unit> UnitsAdded {get;private set;}
		
		private List<Unit> unitsRemoved;
		public  List<Unit> UnitsRemoved {get;private set;}
		
		private List<KeyValuePair<byte, UInt32>> valuesSet;
		public  List<KeyValuePair<byte, UInt32>> ValuesSet {get;private set;} 
//		private List<object> notifications;
//		private List<Path> paths;

		private void clearChanges()
		{
			unitsAdded.Clear();
			unitsRemoved.Clear();
			valuesSet.Clear();
		}
		
		private void addUnit(Unit newUnit)
		{
			unitsAdded.Add(newUnit);
			units.Add(newUnit);
		}
		
		private void removeUnit(Unit deadUnit)
		{
			unitsRemoved.Add(deadUnit);
			units.Remove(deadUnit);
		}
		
		private void setValue(GameObject targetObject, byte key, UInt32 newValue)
		{
			targetObject.setField(key,newValue);
			valuesSet.Add(new KeyValuePair<byte,UInt32>(key,newValue));
		}
//		private void findPath();
//		private void useAbility();

        public MasterGameWorld()
            : base()
		{
			unitsAdded = new List<Unit>(5);
			unitsRemoved = new List<Unit>(5);
			valuesSet = new List<KeyValuePair<byte, UInt32>>(15);
		}
		
		override public void update(TimeSpan gameTime, List<Command> commands)
		{	
			foreach(Command cmd in commands)
			{
				switch (cmd.getCommandType()) {
					case Command.T_COMMAND.ADD:
						
						Command_Add cAdd = (Command_Add)cmd;
						Unit newUnit = (Unit) fact.createGameObject(cAdd.getBuilt());
						GameObject builder = fact.getGameObject(cAdd.getBuilder());
						
						if(builder.GetType() == typeof(Unit))
						{
							newUnit._Position = ((Unit) builder)._Position;
						}
						
						addUnit(newUnit);
				
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
			
			foreach(Unit unit in units)
			{
//				unit.addCommand();
			}
		}
	}
}
