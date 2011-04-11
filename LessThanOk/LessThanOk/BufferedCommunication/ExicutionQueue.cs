using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LessThanOk.Network.Commands;

namespace LessThanOk.BufferedCommunication
{
    public sealed class ExicutionQueue
    {
        private static List<Command_Add> _adds;
        private static List<Command_Set> _sets;

        public static ExicutionQueue The { get { return the; } }
        static readonly ExicutionQueue the = new ExicutionQueue();
        static ExicutionQueue()
        {
            _adds = new List<Command_Add>();
            _sets = new List<Command_Set>();
        }

        public Boolean getAdds(out List<Command_Add> adds)
        {
            if (_adds.Count == 0)
            {
                adds = null;
                return false;
            }
            adds = new List<Command_Add>(_adds);
            return true;
        }
        public Boolean getSets(out List<Command_Set> sets)
        {
            if (_sets.Count == 0)
            {
                sets = null;
                return false;
            }
            sets = new List<Command_Set>(_sets);
            return true;
        }
        public Boolean addAdd(ref Command_Add add)
        {
            _adds.Add(add);
            add = null;
            return true;
        }
        public Boolean addSet(ref Command_Set set)
        {
            _sets.Add(set);
            set = null;
            return true;
        }
    }
}
