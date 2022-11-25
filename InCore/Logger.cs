using System;
using System.Collections.Generic;
using System.Linq;

namespace InLogger
{
    public class Logger
    {
        static Logger Instance { get; set; } = null;

        static object lockObject = new object();

        public static Logger GetLogger()
        {
            lock (lockObject)
            {
                if (Instance == null)
                {
                    Instance = new Logger();
                }
                return Instance;
            }
        }

        private LinkedList<string> Events { get; set; } = new LinkedList<string>();

        public void Log(string message) 
        {
            var dt = DateTime.Now;
            Events.AddLast($"{dt.ToLocalTime()} {message}");
        }

        public string StrEvents => string.Join("\r\n", Events.ToArray());
    }
}
