using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ucore
{
    public delegate void LoggingHandler(string format, params object[] args);
    public delegate void LoggingExceptionHandler(Exception e);

    public class Logging
    {
        public static event LoggingHandler Receivers;
        public static event LoggingExceptionHandler ExceptionReceivers;

        public static void Printf(string format, params object[] args)
        {
            LoggingHandler h = Receivers;
            if (h != null)
                h(format, args);
        }

        public static void PrintException(Exception e)
        {
            LoggingExceptionHandler h = ExceptionReceivers;
            if (h != null)
                h(e);
        }
    }
}
