using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace LessThanOk
{
    public delegate void GlobalHandler(object sender, EventArgs e);

    public class GlobalEvent
    {
        public event GlobalHandler Handler;
        public EVENTNAME Name {get{return _name;}}
        public enum EVENTNAME
        {
            JOINGAME,
            CREATEGAME,
            STARTGAME,
            ENDGAME
        }

        private EVENTNAME _name;

        public GlobalEvent(EVENTNAME name)
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
