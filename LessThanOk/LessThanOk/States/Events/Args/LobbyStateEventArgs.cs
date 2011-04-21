using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LessThanOk.States.Events.Args
{
    public class LobbyStateEventArgs : EventArgs
    {
        public Boolean IsHost { get { return _isHost; } }
        private Boolean _isHost;
        public LobbyStateEventArgs(Boolean isHost)
        {
            _isHost = isHost;
        }
    }
}
