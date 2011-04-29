using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LessThanOk.GameData.GameWorld.Events;
using Microsoft.Xna.Framework;
using LessThanOk.Network.Commands;
using LessThanOk.GameData.GameWorld.Monirator;
using LessThanOk.GameData.GameWorld.GameSim;
using Microsoft.Xna.Framework.Net;

namespace LessThanOk.GameData.GameWorld
{
    class GameWorldController
    {
        Monirator monirator;
        GameSimulator simulator;

        public GameWorldController()
        {
        }

        public void update(GameTime elps)
        {
            //won't work needs to be last
            monirator.setState(simulator.getTileMap());

            Queue<Command> commandsToDo = new List<Command>(2);
            Command nextCommand;
            //needs to be outvalue
            while(monirator.GetNextScheduledCommand(nextCommand))
            {
                commandsToDo.Enqueue(nextCommand);
            }

            simulator.dispatchCommands(commandsToDo);
            simulator.step(elps);

            //write out to network?
        }

        public void sendCommand(Command cmd)
        {
            monirator.EvaluateCommand(cmd);
        }
    }
}
