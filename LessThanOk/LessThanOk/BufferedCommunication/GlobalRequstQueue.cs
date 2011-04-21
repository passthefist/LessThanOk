using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LessThanOk.BufferedCommunication
{
    public sealed class GlobalRequestQueue : RequestQueue
    {
        static readonly GlobalRequestQueue the = new GlobalRequestQueue();
        static GlobalRequestQueue(){}
        public static GlobalRequestQueue The {get{return the;}}

    }
}
