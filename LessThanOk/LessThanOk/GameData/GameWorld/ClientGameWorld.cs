using System;
using System.Collections.Generic;
using LessThanOk.Network.Commands;
using Microsoft.Xna.Framework;
using LessThanOk.GameData.GameObjects.Units;
using LessThanOk.GameData.GameObjects;
using LessThanOk.GameData.GameWorld;
using LessThanOk.BufferedCommunication;

namespace LessThanOk.GameData.GameWorld
{
    public class ClientGameWorld : GameWorld
    {
        public List<Command> Commands;

        public ClientGameWorld() : base ()
        {
        }

        override public void update(TimeSpan elps)
        {
            Commands.AddRange(RequestQueue_Client.The.Requests);
            foreach (Command cmd in Commands)
            {
                switch (cmd.getCommandType())
                {
                    case Command.T_COMMAND.ADD:

                        Command_Add cAdd = (Command_Add)cmd;
                        Unit newUnit = (Unit)fact.resurrectGameObject(cAdd.getType(),cAdd.getBuilt());
                        GameObject builder = fact.getGameObject(cAdd.getBuilder());

                        if (builder.GetType() == typeof(Unit))
                        {
                            newUnit._Position = ((Unit)builder)._Position;
                        }

                        units.Add(newUnit);

                        break;
                    case Command.T_COMMAND.CANCEL:
                        break;
                    case Command.T_COMMAND.REMOVE:
                        break;
                    case Command.T_COMMAND.SET:
                        Command_Set cSet = (Command_Set)cmd;
                        fact.getGameObject(cSet.getID()).setField(cSet.getKey(),cSet.getValue());
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
