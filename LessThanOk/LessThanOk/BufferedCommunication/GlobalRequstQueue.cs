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

        public Boolean push(Command command)
        {
            requests.Enqueue(command);
            return true;
        }

        public Boolean pull(out Command val)
        {
            if (requests.Count == 0)
            {
                val = null;
                return false;
            }
            val = requests.Dequeue();
            return true;
        }

        public bool hasItems()
        {
            return requests.Count > 0;
        }

        public GlobalRequestQueue()
        {
            requests = new Queue<Command>();
        }

    }
}

