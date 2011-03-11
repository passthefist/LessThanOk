using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LessThanOk.Network.Commands;

namespace LessThanOk.Network
{
    class NetworkManager
    {
        private List<Command> Changes;

        public NetworkManager()
        {

        }
        public void getChanges(List<Command> changes)
        {
            Changes = changes;
        } 
    }
}
