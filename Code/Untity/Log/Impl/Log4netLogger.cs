using System;
using System.Collections.Generic;
using System.Text;
using log4net;
using log4net.Ext.Trace;

namespace GeneralAviationWebSite.Untity.Impl
{
    internal class Log4netLogger : ILog
    {
        private readonly ITraceLog _log;

        public Log4netLogger(ITraceLog log)
        {
            this._log = log;
        }

        public bool IsTraceEnabled
        {
            get { return _log.IsTraceEnabled; }
        }

        public bool IsDebugEnabled
        {
            get { return _log.IsDebugEnabled; }
        }

        public bool IsInfoEnabled
        {
            get { return _log.IsInfoEnabled; }
        }

        public bool IsWarnEnabled
        {
            get { return _log.IsWarnEnabled; }
        }

        public bool IsErrorEnabled
        {
            get { return _log.IsErrorEnabled; }
        }

        public bool IsFatalEnabled
        {
            get { return _log.IsFatalEnabled; }
        }

        public void Trace(string message)
        {
            if(IsTraceEnabled)
            {
                _log.Debug(message);
            }
        }

        public void Trace(string message, params object[] args)
        {
            if(IsTraceEnabled)
            {
                _log.Debug(Format(message,args));
            }
        }

        public void Trace(string message, Exception exception)
        {
            if(IsTraceEnabled)
            {
                _log.Debug(message,exception);
            }
        }

        public void Trace(string message, object arg1, Exception exception)
        {
            if(IsTraceEnabled)
            {
                _log.Debug(Format(message,arg1), exception);
            }
        }

        public void Trace(string message, object arg1, object arg2, Exception exception)
        {
            if(IsTraceEnabled)
            {
                _log.Debug(Format(message, arg1, arg2), exception);
            }
        }

        public void Trace(string message, object arg1, object arg2, object arg3, Exception exception)
        {
            if(IsTraceEnabled)
            {
                _log.Debug(Format(message,arg1,arg2,arg3),exception);
            }
        }

        public void Trace(Func<string> message)
        {
            if(IsTraceEnabled)
            {
                _log.Debug(message());
            }
        }

        public void Trace(Func<string> message, Exception exception)
        {
            if(IsTraceEnabled)
            {
                _log.Debug(message(),exception);
            }
        }

        public void Debug(string message)
        {
            _log.Debug(message);
        }

        public void Debug(string message, params object[] args)
        {
            if (IsDebugEnabled)
            {
                _log.Debug(Format(message, args));
            }
        }

        public void Debug(Func<string> message)
        {
            if (IsDebugEnabled)
            {
                _log.Debug(message());
            }
        }

        public void Debug(string message, Exception exception)
        {
            _log.Debug(message, exception);
        }

        public void Debug(string message, object arg1, Exception exception)
        {
            if (IsDebugEnabled)
            {
                _log.Debug(Format(message, arg1), exception);
            }
        }

        public void Debug(string message, object arg1, object arg2, Exception exception)
        {
            if (IsDebugEnabled)
            {
                _log.Debug(Format(message, arg1, arg2), exception);
            }
        }

        public void Debug(string message, object arg1, object arg2, object arg3, Exception exception)
        {
            if (IsDebugEnabled)
            {
                _log.Debug(Format(message, arg1, arg2, arg3), exception);
            }
        }

        public void Debug(Func<string> message, Exception exception)
        {
            if (IsDebugEnabled)
            {
                _log.Debug(message(), exception);
            }
        }

        public void Info(string message)
        {
            _log.Info(message);
        }

        public void Info(string message, params object[] args)
        {
            if (IsInfoEnabled)
            {
                _log.Info(Format(message, args));
            }
        }

        public void Info(Func<string> message)
        {
            if (IsInfoEnabled)
            {
                _log.Info(message());
            }
        }

        public void Info(string message, Exception exception)
        {
            _log.Info(message, exception);
        }

        public void Info(string message, object arg1, Exception exception)
        {
            if (IsInfoEnabled)
            {
                _log.Info(Format(message, arg1), exception);
            }
        }

        public void Info(string message, object arg1, object arg2, Exception exception)
        {
            if (IsInfoEnabled)
            {
                _log.Info(Format(message, arg1, arg2), exception);
            }
        }

        public void Info(string message, object arg1, object arg2, object arg3, Exception exception)
        {
            if (IsInfoEnabled)
            {
                _log.Info(Format(message, arg1, arg2, arg3), exception);
            }
        }

        public void Info(Func<string> message, Exception exception)
        {
            if (IsInfoEnabled)
            {
                _log.Info(message(), exception);
            }
        }

        public void Warn(string message)
        {
            _log.Warn(message);
        }

        public void Warn(string message, params object[] args)
        {
            if (IsWarnEnabled)
            {
                _log.Warn(Format(message, args));
            }
        }

        public void Warn(Func<string> message)
        {
            if (IsWarnEnabled)
            {
                _log.Warn(message());
            }
        }

        public void Warn(string message, Exception exception)
        {
            _log.Warn(message, exception);
        }

        public void Warn(string message, object arg1, Exception exception)
        {
            if (IsWarnEnabled)
            {
                _log.Warn(Format(message, arg1), exception);
            }
        }

        public void Warn(string message, object arg1, object arg2, Exception exception)
        {
            if (IsWarnEnabled)
            {
                _log.Warn(Format(message, arg1, arg2), exception);
            }
        }

        public void Warn(string message, object arg1, object arg2, object arg3, Exception exception)
        {
            if (IsWarnEnabled)
            {
                _log.Warn(Format(message, arg1, arg2, arg3), exception);
            }
        }

        public void Warn(Func<string> message, Exception exception)
        {
            if (IsWarnEnabled)
            {
                _log.Warn(message(), exception);
            }
        }

        public void Error(string message)
        {
            _log.Error(message);
        }

        public void Error(string message, params object[] args)
        {
            if (IsErrorEnabled)
            {
                _log.Error(Format(message, args));
            }
        }

        public void Error(Func<string> message)
        {
            if (IsErrorEnabled)
            {
                _log.Error(message());
            }
        }

        public void Error(string message, Exception exception)
        {
            _log.Error(message, exception);
        }

        public void Error(string message, object arg1, Exception exception)
        {
            if (IsErrorEnabled)
            {
                _log.Error(Format(message, arg1), exception);
            }
        }

        public void Error(string message, object arg1, object arg2, Exception exception)
        {
            if (IsErrorEnabled)
            {
                _log.Error(Format(message, arg1, arg2), exception);
            }
        }

        public void Error(string message, object arg1, object arg2, object arg3, Exception exception)
        {
            if (IsErrorEnabled)
            {
                _log.Error(Format(message, arg1, arg2, arg3), exception);
            }
        }

        public void Error(Func<string> message, Exception exception)
        {
            if (IsErrorEnabled)
            {
                _log.Error(message(), exception);
            }
        }

        public void Fatal(string message)
        {
            _log.Fatal(message);
        }

        public void Fatal(string message, params object[] args)
        {
            if (IsFatalEnabled)
            {
                _log.Fatal(Format(message, args));
            }
        }

        public void Fatal(Func<string> message)
        {
            if (IsFatalEnabled)
            {
                _log.Fatal(message());
            }
        }

        public void Fatal(string message, Exception exception)
        {
            _log.Fatal(message, exception);
        }

        public void Fatal(string message, object arg1, Exception exception)
        {
            if (IsFatalEnabled)
            {
                _log.Fatal(Format(message, arg1), exception);
            }
        }

        public void Fatal(string message, object arg1, object arg2, Exception exception)
        {
            if (IsFatalEnabled)
            {
                _log.Fatal(Format(message, arg1, arg2), exception);
            }
        }

        public void Fatal(string message, object arg1, object arg2, object arg3, Exception exception)
        {
            if (IsFatalEnabled)
            {
                _log.Fatal(Format(message, arg1, arg2, arg3), exception);
            }
        }

        public void Fatal(Func<string> message, Exception exception)
        {
            if (IsFatalEnabled)
            {
                _log.Fatal(message(), exception);
            }
        }

        private static string Format(string format,params object[] args)
        {
            try
            {
                return string.Format(format, args);
            }
            catch(FormatException e)
            {
                return string.Format("invalid format -> {0}", format);
            }
        }
    }
}
