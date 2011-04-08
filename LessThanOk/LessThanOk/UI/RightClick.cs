using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LessThanOk.UI
{
    public delegate void UIEventHandler(object sender, EventArgs e);

    public class RightClick
    {
        public event UIEventHandler Handler;

        public virtual void click(UIElement sender)
        {
            if (Handler != null)
                Handler(sender, EventArgs.Empty);
        }
    }
}
