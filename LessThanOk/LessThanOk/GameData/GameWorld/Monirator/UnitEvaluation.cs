using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LessThanOk.GameData.GameWorld.Monirator
{
    public class UnitEvaluation
    {
        public String Name { get { return _name; } }

        private List<Dependency> _dep;
        private String _name;

        public UnitEvaluation(String name)
        {
            _name = name;
        }

        public void addDependency(Dependency dep)
        {
            _dep.Add(dep);
        }

        public String getType(Dictionary<String, bool> map)
        {
            String retval;

            foreach (Dependency d in _dep)
            {
                if ((retval = d.Evaluate(map)) != null)
                    return retval;
            }
            return _name;
        }
        
    }
}
