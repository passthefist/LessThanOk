using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LessThanOk.UI.Events.Args;

namespace LessThanOk.UI.Events
{
    public sealed class UIElementEvents
    {
        public static event EventHandler<ButtonEventArgs> ButtonPress;

        static readonly UIElementEvents the = new UIElementEvents();
        public static UIElementEvents The { get { return the; } }
        static UIElementEvents() { }

        public void TriggerButtonPress(object sender, ButtonEventArgs args)
        {
            ButtonPress.Invoke(sender, args);
        }

    }
}
