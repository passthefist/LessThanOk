using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LessThanOk.GameData.GameWorld.Events;
using Microsoft.Xna.Framework;
using LessThanOk.Network.Commands;
using LessThanOk.GameData.GameWorld.MoniratorSpace;
using LessThanOk.GameData.GameWorld.GameSim;
using Microsoft.Xna.Framework.Net;
using LessThanOk.Network;
using Microsoft.Xna.Framework.Graphics;
using LessThanOk.GameData.GameObjects.Tiles;
using LessThanOk.GameData.GameObjects.Units;
using Microsoft.Xna.Framework.GamerServices;
using LessThanOk.Network.Commands.Events;
using LessThanOk.UI;

namespace LessThanOk.GameData.GameWorld
{
    class GameWorldController
    {
        Monirator monirator;
        GameSimulator simulator;
        NetworkManager NetworkController;
        CommandRequester CmdRequester;
        bool HostSession;

        public GameWorldController()
        {
            monirator = new Monirator();
            CmdRequester = new CommandRequester();
        }
        public void Initialize(String XMLFile, bool isHost, Frame_Game frame)
        {
            TileMap map = new TileMap();
            RuleBook rulebook = new RuleBook();
            rulebook.LoadXMLData(XMLFile);

            if (isHost)
            {
                simulator = new MasterSimulator(map);
            }
            else
            {
                simulator = new GameSimulator(map);
            }
            monirator.Initialize(map, rulebook);
            HostSession = isHost;

            frame.AddUnitEvent +=new EventHandler(CmdRequester.AddButtonHandler);
            NetworkController.NewCommandEvent += new EventHandler<NewCommandEventArgs>(monirator.EvaluateNewCommand);
            CmdRequester.NewCommandEvent += new EventHandler<NewCommandEventArgs>(monirator.EvaluateNewCommand);
        }
        public void update(GameTime elps, GamerCollection<LocalNetworkGamer> Gamers)
        {
            if (HostSession)
            {
                NetworkController.serverReadPackets(Gamers, elps);
                Queue<Command> commandsToDo = new Queue<Command>(2);
                Command nextCommand = new Command();
                //needs to be outvalue
                while (monirator.GetNextScheduledCommand(ref nextCommand))
                {
                    commandsToDo.Enqueue(nextCommand);
                }

                simulator.dispatchCommands(commandsToDo);
                simulator.step(elps);
            }
            else
            {

            }
            //write out to network?
        }

        public void Draw(SpriteBatch batch)
        {
            List<Tile> tiles = simulator.getTilesInRect(new Rectangle(0, 0, 800, 500));
            List<Unit> units = simulator.getUnitsInRect(new Rectangle(0, 0, 800, 500));

            foreach (Tile t in tiles)
            {
                t.draw(batch);
            }

            foreach (Unit u in units)
            {
                u.draw(batch);
            }
        }
    }
}
