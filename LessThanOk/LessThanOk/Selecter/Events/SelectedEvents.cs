using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LessThanOk.Selecter.Events.Args;

namespace LessThanOk.Selecter.Events
{
    class SelectedEvents
    {
        public event EventHandler<SelectedEventArgs> GameObjectsSelected;

        public static SelectedEvents The { get { return the; } }
        private static SelectedEvents the = new SelectedEvents();
        static SelectedEvents() { }

        public void TriggerGameObjectsSelected(object sender, SelectedEventArgs args)
        {
            GameObjectsSelected.Invoke(sender, args);
        }
    }
}
