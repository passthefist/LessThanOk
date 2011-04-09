using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LessThanOk.BufferedCommunication
{
    public sealed class RequestQueue_Server : RequestQueue
    {
        static readonly RequestQueue_Server the = new RequestQueue_Server();
        static RequestQueue_Server() { }
        public static RequestQueue_Server The { get { return the; } }

    }
}
