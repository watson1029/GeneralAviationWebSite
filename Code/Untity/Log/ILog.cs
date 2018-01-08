using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralAviationWebSite.Untity
{
    public interface ILog
    {
        bool IsTraceEnabled { get; }

        bool IsDebugEnabled { get; }

        bool IsInfoEnabled { get; }

        bool IsWarnEnabled { get; }

        bool IsErrorEnabled { get; }

        bool IsFatalEnabled { get; }

        void Trace(string message);

        void Trace(string message, params Object[] args);

        void Trace(string message, Exception exception);

        void Trace(string message, object arg1, Exception exception);

        void Trace(string message, object arg1, object arg2, Exception exception);

        void Trace(string message, object arg1, object arg2, object arg3, Exception exception);

        void Trace(Func<string> message);

        void Trace(Func<string> message, Exception exception);

        void Debug(string message);

        void Debug(string message, params Object[] args);

        void Debug(string message, Exception exception);

        void Debug(string message, object arg1, Exception exception);

        void Debug(string message, object arg1, object arg2, Exception exception);

        void Debug(string message, object arg1, object arg2, object arg3, Exception exception);

        void Debug(Func<string> message);

        void Debug(Func<string> message, Exception exception);

        void Info(string message);

        void Info(string message, Exception exception);

        void Info(string message, params Object[] args);

        void Info(string message, object arg1, Exception exception);

        void Info(string message, object arg1, object arg2, Exception exception);

        void Info(string message, object arg1, object arg2, object arg3, Exception exception);

        void Info(Func<string> message);

        void Info(Func<string> message, Exception exception);

        void Warn(string message);

        void Warn(string message, Exception exception);

        void Warn(string message, params Object[] args);

        void Warn(string message, object arg1, Exception exception);

        void Warn(string message, object arg1, object arg2, Exception exception);

        void Warn(string message, object arg1, object arg2, object arg3, Exception exception);

        void Warn(Func<string> message);

        void Warn(Func<string> message, Exception exception);

        void Error(string message);

        void Error(string message, Exception exception);

        void Error(string message, params Object[] args);

        void Error(string message, object arg1, Exception exception);

        void Error(string message, object arg1, object arg2, Exception exception);

        void Error(string message, object arg1, object arg2, object arg3, Exception exception);

        void Error(Func<string> message);

        void Error(Func<string> message, Exception exception);

        void Fatal(string message);

        void Fatal(string message, Exception exception);

        void Fatal(string message, params Object[] args);

        void Fatal(string message, object arg1, Exception exception);

        void Fatal(string message, object arg1, object arg2, Exception exception);

        void Fatal(string message, object arg1, object arg2, object arg3, Exception exception);

        void Fatal(Func<string> message);

        void Fatal(Func<string> message, Exception exception);
    }
}