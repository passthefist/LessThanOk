using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LessThanOk.GameData.GameObjects;

namespace LessThanOk.Selecter.Events
{
    public class SelectedEventArgs:EventArgs
    {
        private List<ActiveGameObject> _objects;
        public List<ActiveGameObject> Objects { get { return _objects; } }
        public SelectedEventArgs(List<ActiveGameObject> objects) { _objects = objects; }

    }
}
