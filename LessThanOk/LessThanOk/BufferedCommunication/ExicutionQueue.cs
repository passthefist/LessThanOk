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
        
        public Command pollAdd()
        {
            if (_adds.Count > 0)
                return _adds.Dequeue();
            return null;
        }
        public Command poolSet()
        {
            if (_sets.Count > 0)
                return _sets.Dequeue();
            return null;
        }
        public Boolean addAdd(Command add)
        {
            _adds.Enqueue(add);
            return true;
        }
        public Boolean addSet(Command set)
        {
            _sets.Enqueue(set);
            return true;
        }
    }
}
