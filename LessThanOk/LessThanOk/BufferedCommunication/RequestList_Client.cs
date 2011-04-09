using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LessThanOk.BufferedCommunication
{
    public sealed class RequestQueue_Client : RequestQueue
    {
        static readonly RequestQueue_Client the = new RequestQueue_Client();
        static RequestQueue_Client(){}
        public static RequestQueue_Client The {get{return the;}}

    }
}
