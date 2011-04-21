using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LessThanOk.Network.Commands;

namespace LessThanOk.BufferedCommunication
{
    public sealed class ExicutionQueue
    {
        private static Queue<Command_Add> _adds;
        private static Queue<Command_Set> _sets;

        public static ExicutionQueue The { get { return the; } }
        static readonly ExicutionQueue the = new ExicutionQueue();
        static ExicutionQueue()
        {
            _adds = new Queue<Command_Add>();
            _sets = new Queue<Command_Set>();
        }
        
        public Command_Add pollAdd()
        {
            if (_adds.Count > 0)
                return _adds.Dequeue();
            return null;
        }
        public Command_Set poolSet()
        {
            if (_sets.Count > 0)
                return _sets.Dequeue();
            return null;
        }
        public Boolean addAdd(Command_Add add)
        {
            _adds.Enqueue(add);
            return true;
        }
        public Boolean addSet(Command_Set set)
        {
            _sets.Enqueue(set);
            return true;
        }
    }
}
