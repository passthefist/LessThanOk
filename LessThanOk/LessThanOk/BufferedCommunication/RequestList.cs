using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LessThanOk.Network.Commands;

namespace LessThanOk.BufferedCommunication
{
    class RequestList
    {
        private List<Command> Requests;

        public void addRequests(List<Command> requests)
        {
            Requests = requests;
        }

        public List<Command> getRequests()
        {
            //Adventually will be a distructive read.
            return Requests;
        }
    }
}
