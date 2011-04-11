using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LessThanOk.UI.Events
{
    public class LeftClick
    {
        public event UIEventHandler Handler;
        
        public virtual void click(UIElement sender)
        {
            if (Handler != null)
                Handler(sender, EventArgs.Empty);
        }
    }
}

