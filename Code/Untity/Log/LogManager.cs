using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using GeneralAviationWebSite.Untity.Impl;

namespace GeneralAviationWebSite.Untity
{
    public class LogManager
    {
        private static ILog _log = GetLogger(typeof (LogManager));

        public static ILog GetLogger(String name)
        {
            return StaticLogFactoryBinder.Instance.GetLoggerFactory().GetLogger(name);
        }

        public static ILog GetLogger(Type type)
        {
            return StaticLogFactoryBinder.Instance.GetLoggerFactory().GetLogger(type);
        }

        public static ILog GetCurrentClassLogger()
        {
#if SILVERLIGHT
            StackFrame frame = new StackFrame(1);
#else
            StackFrame frame = new StackFrame(1, false);
#endif

            if (null == frame.GetMethod())
            {
                _log.Warn("Null MethodBase In StackFrame");
                return _log;
            }

            if (null == frame.GetMethod().DeclaringType)
            {
                _log.Warn("Null DeclaringType In StackFrame");
                return _log;
            }

            return GetLogger(frame.GetMethod().DeclaringType);
        }
    }
}
