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

        public UIEventListener()
        {
            RightClickEvent = UIManager.RightClickEvent;
            LeftClickEvent = UIManager.LeftClickEvent;

            RightClickEvent.Handler += new UIEventHandler(RightClickHandler);
            LeftClickEvent.Handler += new UIEventHandler(LeftClickHandler);

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
                    UIManager.The.switchFrame("home");
                }
                else if (element.Name == "end")
                {
                    UIManager.The.switchFrame("postgame");
                }
                else if (element.Name == "join")
                {
                    Console.WriteLine("Joining Session...");
                    NetworkManager.The.joinSession();
                    UIManager.The.switchFrame("clientlobby");
                }
                else if (element.Name == "create")
                {
                    Console.WriteLine("Creating Session...");
                    NetworkManager.The.startSession();
                    UIManager.The.switchFrame("hostlobby");
                }
                else if (element.Name == "start")
                {

                    UIManager.The.switchFrame("game");
                }
                else if (element.Name == "ready")
                {
            
                }
                else if (element.Name == "add")
                {
                    if (NetworkManager.Session.IsHost)
                    {
                        UInt16 builder = InputManager.Selected.ID;
                        UInt16 type = GameObjectFactory.The.getType("TestUnit").ID;

                        Command_Add cmd = new Command_Add(builder, 0, type, new TimeSpan());
                        RequestQueue_Server.The.push(cmd);
                    }
                }
            }
            else
            {

            }
        }

    }
}
