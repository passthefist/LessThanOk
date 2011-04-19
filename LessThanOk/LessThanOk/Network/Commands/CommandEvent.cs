using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LessThanOk.Network.Commands
{
    public delegate void CommandHandler(object sender, EventArgs e);

    public class CommandEvent
    {
        public event CommandHandler Handler;
        public EVENTNAME Name { get { return _name; } }
        public enum EVENTNAME
        {
            ADD
        }

        private EVENTNAME _name;

        public CommandEvent(EVENTNAME name)
        {
            _name = name;
        }
        public virtual void trigger()
        {
            if (Handler != null)
                Handler(this, EventArgs.Empty);
        }
    }
}
