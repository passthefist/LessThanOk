using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LessThanOk
{
    interface GlobalEventSubscriber
    {
        void JoinSessionHandler(object sender, EventArgs e);
        void CreateSessionHandler(object sender, EventArgs e);
        void StartGameHandler(object sender, EventArgs e);
        void EndGameHandler(object sender, EventArgs e);
        void subscribe(List<GlobalEvent> events);
    }
}
