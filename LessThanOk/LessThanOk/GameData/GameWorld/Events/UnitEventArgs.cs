using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LessThanOk.GameData.GameObjects.Units;

namespace LessThanOk.GameData.GameWorld.Events
{
    public class UnitEventArgs : EventArgs
    {

        private Unit unit;

        public Unit UnitBuilt
        {
            get { return unit; }
        }

        public UnitEventArgs(Unit u)
        {
            unit = u;
        }

    }
}
