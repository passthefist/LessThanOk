using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LessThanOk.Network.Commands;

namespace LessThanOk.BufferedCommunication
{
    public abstract class RequestQueue
    {
        private Queue<Command> requests;

        public Queue<Command> Requests
        {
            /*
            get
            {
                Queue<Command> retval = new Queue<Command>(requests);
                requests.Clear();
                return retval;
            }
            */
            set
            {
                requests = value;
            }
        }

        public void push(Command command)
        {
            requests.Enqueue(command);
        }

        public Command poll()
        {
            if (requests.Count == 0)
                return null;
            Command retval = requests.Dequeue();
            return retval;
        }

        public RequestQueue()
        {
            requests = new Queue<Command>();
        }

    }
}
