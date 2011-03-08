using System;
using System.Collections.Generic;
using LessThanOk.Network.Commands;
using Microsoft.Xna.Framework;
using LessThanOk.GameData.GameObjects.Units;
using LessThanOk.GameData.GameObjects;

namespace LessThanOk.GameData.GameWorld
{
    public class ClientGameWorld : GameWorld
    {

        public ClientGameWorld() : base ()
        {
        }

        override public void update(TimeSpan gameTime, List<Command> commands)
        {
            foreach (Command cmd in commands)
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
                        break;
                    case Command.T_COMMAND.ERROR:
                        break;
                    default:
                        break;
                }
            }

            foreach (Unit unit in units)
            {
                //				unit.addCommand();
            }
        }
    }
}
