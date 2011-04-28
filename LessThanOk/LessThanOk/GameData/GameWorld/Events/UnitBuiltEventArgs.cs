using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LessThanOk.GameData.GameObjects.Units;

namespace LessThanOk.GameData.GameWorld.Events
{
    class UnitBuiltEventArgs : EventArgs
    {

        private Unit unit;

        public Unit UnitBuilt
        {
            get { return unit; }
        }

        public UnitBuiltEventArgs(Unit u)
        {
            unit = u;
        }

    }
}
