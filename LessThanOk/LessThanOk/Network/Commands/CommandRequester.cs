using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LessThanOk.BufferedCommunication;
using LessThanOk.UI;
using LessThanOk.GameData.GameObjects;

namespace LessThanOk.Network.Commands
{
    class CommandRequester: CommandEventSubscriber
    {

        #region CommandEventSubscriber Members

        public void AddEventHandler(object sender, EventArgs e)
        {
            UInt16 builder = InputManager.Selected.ID;
            UInt16 type = GameObjectFactory.The.getType("TestUnit").ID;

            Command_Add cmd = new Command_Add(builder, 0, type, new TimeSpan());
            GlobalRequestQueue.The.push(cmd);
        }

        public void subscribe(List<CommandEvent> events)
        {
            foreach (CommandEvent e in events)
            {
                switch (e.Name)
                {
                    case CommandEvent.EVENTNAME.ADD:
                        e.Handler += AddEventHandler;
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion
    }
}
