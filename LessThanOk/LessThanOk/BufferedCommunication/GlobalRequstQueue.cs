using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LessThanOk.Network.Commands;

namespace LessThanOk.BufferedCommunication
{
    public sealed class GlobalRequestQueue
    {
        public static GlobalRequestQueue The { get { return the; } }
        static readonly GlobalRequestQueue the = new GlobalRequestQueue();
        static GlobalRequestQueue(){}

        private Queue<Command> requests;

        public Queue<Command> Requests
        {
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

        public bool hasItems()
        {
            return requests.Count > 0;
        }

        public Command pull()
        {
            return requests.Dequeue();
        }

        public GlobalRequestQueue()
        {
            requests = new Queue<Command>();
        }

    }
}

