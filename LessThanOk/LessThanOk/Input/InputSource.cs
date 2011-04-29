using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LessThanOk.Network.Commands.Events;

namespace LessThanOk.Input
{
    interface InputSource
    {
        event EventHandler<NewCommandEventArgs> NewCommandEvent;
    }
}
