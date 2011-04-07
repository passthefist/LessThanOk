using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LessThanOk.Debug
{
    static class Logging
    {
        private static Logger log;

        private class Logger
        {
            private TextWriter log;

            public Logger()
            {
                log = new StreamWriter("LessThanOKLog.txt");
            }

            public void WriteLog(string text)
            {
                log.WriteLine(text);
                log.Flush();
            }
        }
        
        static Logging()
        {
            log = new Logger();
        }

        public static void LOG(string text)
        {
            log.WriteLog("LOG " + DateTime.Now + ": " + text);
        }

        public static void WARN(string text)
        {
            log.WriteLog("WARN " + DateTime.Now + ": " + text);
        }

        public static void FATAL(string text)
        {
            log.WriteLog("FATAL " + DateTime.Now + ": " + text);
        }
    }
}
