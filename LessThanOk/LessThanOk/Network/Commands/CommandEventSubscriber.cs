using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LessThanOk.Network.Commands
{
    interface CommandEventSubscriber
    {
        void AddEventHandler(object sender, EventArgs e);
        void subscribe(List<CommandEvent> events);
    }
}
