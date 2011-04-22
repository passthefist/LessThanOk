using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LessThanOk.Network.Commands;

namespace LessThanOk.BufferedCommunication
{
    public sealed class ExicutionQueue
    {
        private static Queue<Command> _adds;
        private static Queue<Command> _sets;

        public static ExicutionQueue The { get { return the; } }
        static readonly ExicutionQueue the = new ExicutionQueue();
        static ExicutionQueue()
        {
            _adds = new Queue<Command>();
            _sets = new Queue<Command>();
        }
        
        public Boolean pullAdd(out Command val)
        {
            if (_adds.Count == 0)
            {
                val = null;
                return false;
            }
                
            val = _adds.Dequeue();
            return true;
        }
        public Boolean pullSet(out Command val)
        {
            if (_sets.Count == 0)
            {
                val = null;
                return false;
            }

            val = _sets.Dequeue();
            return true;
        }
        public Boolean pushAdd(Command add)
        {
            _adds.Enqueue(add);
            return true;
        }
        public Boolean pushSet(Command set)
        {
            _sets.Enqueue(set);
            return true;
        }
    }
}
