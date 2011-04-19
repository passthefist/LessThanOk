using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.GamerServices;
using LessThanOk.UI.Events;
using LessThanOk.Network;
using LessThanOk.BufferedCommunication;
using LessThanOk.Network.Commands;
using LessThanOk.GameData.GameObjects;

namespace LessThanOk.UI
{
    public class UIEventListener
    {
        private RightClick RightClickEvent;
        private LeftClick LeftClickEvent;
        private GlobalEvent _JoinGame;
        private GlobalEvent _CreateGame;
        private GlobalEvent _StartGame;
        private GlobalEvent _EndGame;
        private CommandEvent _AddEvent;

        public UIEventListener(List<GlobalEvent> events, List<CommandEvent> cevents)
        {
            RightClickEvent = UIManager.RightClickEvent;
            LeftClickEvent = UIManager.LeftClickEvent;

            RightClickEvent.Handler += new UIEventHandler(RightClickHandler);
            LeftClickEvent.Handler += new UIEventHandler(LeftClickHandler);

            foreach (GlobalEvent e in events)
            {
                switch (e.Name)
                {
                    case GlobalEvent.EVENTNAME.JOINGAME:
                        _JoinGame = e;
                        break;
                    case GlobalEvent.EVENTNAME.CREATEGAME:
                        _CreateGame = e;
                        break;
                    case GlobalEvent.EVENTNAME.STARTGAME:
                        _StartGame = e;
                        break;
                    case GlobalEvent.EVENTNAME.ENDGAME:
                        _EndGame = e;
                        break;
                    default:
                        break;
                }
            }
            foreach (CommandEvent e in cevents)
            {
                switch (e.Name)
                {
                    case CommandEvent.EVENTNAME.ADD:
                        _AddEvent = e;
                        break;
                    default:
                        break;
                }
            }
        }
        private void RightClickHandler(object sender, EventArgs e)
        {
            if (sender is UIElement)
            {
                UIElement element = (UIElement)sender;
            }
            else
            {

            }
        }
        private void LeftClickHandler(object sender, EventArgs e)
        {
            if (sender is UIElement)
            {
                UIElement element = (UIElement)sender;
                if (element.Name == "home")
                {
                    //UIManager.The.switchFrame("home");
                }
                else if (element.Name == "end")
                {
                    _EndGame.trigger();
                }
                else if (element.Name == "join")
                {
                    Console.WriteLine("Joining Session...");
                    _JoinGame.trigger();
                }
                else if (element.Name == "create")
                {
                    Console.WriteLine("Creating Session...");
                    _CreateGame.trigger();
                }
                else if (element.Name == "start")
                {
                    _StartGame.trigger();
                }
                else if (element.Name == "ready")
                {
            
                }
                else if (element.Name == "add")
                {
                    _AddEvent.trigger();

                    /*
                    if (NetworkManager.Session.IsHost)
                    {
                        UInt16 builder = InputManager.Selected.ID;
                        UInt16 type = GameObjectFactory.The.getType("TestUnit").ID;

                        Command_Add cmd = new Command_Add(builder, 0, type, new TimeSpan());
                        RequestQueue_Server.The.push(cmd);
                    }
                     */
                }
            }
            else
            {

            }
        }

    }
}
