using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LessThanOk.GameData.GameWorld.Monirator
{
    public class RuleBook
    {
        private Dictionary<String, UnitEvaluation> _UnitDependencies;
        private Dictionary<String, int> _refrenceCount;
        private Dictionary<String, bool> _unitExists;

        public int GetUnitBuildTime(String unitName)
        {
            return 0;
        }
        public void LoadXMLData(String file)
        {
            // No time to impliment xml parsing.
            // Hard code rule book.
        }
        public void UnitDoneBuildingEventHandler(object sender, EventArgs args)
        {

        }
        public UnitEvaluation getDependancy(String name)
        {
            UnitEvaluation retval;
            _UnitDependencies.TryGetValue(name, out retval);
            return retval;
        }
    }
}
