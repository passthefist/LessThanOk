using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LessThanOk.GameData.GameWorld.Events;
using LessThanOk.GameData.GameObjects;

namespace LessThanOk.GameData.GameWorld.Monirator
{
    public class RuleBook
    {
        private Dictionary<String, UnitEvaluation> _UnitDependencies;
        private Dictionary<String, int> _refrenceCount;
        public Dictionary<String, bool> _unitExists;

        public RuleBook()
        {
            _UnitDependencies = new Dictionary<string, UnitEvaluation>();
            _unitExists = new Dictionary<string, bool>();
            _refrenceCount = new Dictionary<string, int>();
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
        public void UnitBuiltEventHandler(object sender, UnitEventArgs args)
        {
            String name = GameObjectFactory.The.getType(args.UnitBuilt.ID).Name;
            incrementRefCount(name);
        }
        public void UnitRemovedEventHandler(object sender, UnitEventArgs args)
        {
            String name = GameObjectFactory.The.getType(args.UnitBuilt.ID).Name;
            decrementRefCount(name);
        }
        private bool incrementRefCount(String name)
        {
            int val;
            if (_refrenceCount.TryGetValue(name, out val))
            {
                val += 1;
                if (val > 0)
                {
                    updateUnitExists(name, true);
                }
                _refrenceCount.Remove(name);
                _refrenceCount.Add(name, val);
                return true;
            }
            return false;
        }
        private bool decrementRefCount(String name)
        {
            int val;
            if (_refrenceCount.TryGetValue(name, out val))
            {
                val -= 1;
                _refrenceCount.Remove(name);
                _refrenceCount.Add(name, val);
                return true;
            }
            return false;
        }
        private void updateUnitExists(String name, bool b)
        {
            if (_unitExists.ContainsKey(name))
            {
                _unitExists.Remove(name);
                _unitExists.Add(name, b);
            }
            _unitExists.Add(name, b);
        }

 
    }
}
