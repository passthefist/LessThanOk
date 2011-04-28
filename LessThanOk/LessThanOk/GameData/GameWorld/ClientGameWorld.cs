using System;
using System.Collections.Generic;
using LessThanOk.Network.Commands;
using Microsoft.Xna.Framework;
using LessThanOk.GameData.GameObjects.Units;
using LessThanOk.GameData.GameObjects;
using LessThanOk.GameData.GameWorld;


namespace LessThanOk.GameData.GameWorld
{
    public class ClientGameWorld : GameWorld
    {
        public List<Command> Commands;
        public List<Command> toAdds;

        public ClientGameWorld() : base ()
        {
        }

        override public void update(GameTime elps)
        {
            gameTime = elps;
            Command cmd;
            /*
            while(ExicutionQueue.The.pullAdd(out cmd))
            {
                Unit newUnit = (Unit)fact.resurrectGameObject(cmd.UnitID, cmd.Type);
                ActiveGameObject builder = (ActiveGameObject)fact.getGameObject(cmd.ParentID);

                newUnit._Position = builder._Position;

                units.Add(newUnit);
            }
            

            /*
             *switch (cmd.getCommandType())
                {
                    case Command.T_COMMAND.ADD:

                        

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
             */

            UpdateUnits();

            ConstructTileMap();
        }
    }
}
