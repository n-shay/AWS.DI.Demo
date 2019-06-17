using System;

namespace TRG.Extensions.Logging
{
    public interface ILogger
    {
        void Verbose(Exception exception, string message, params object[] propertyValues);

        void Verbose(string message, params object[] propertyValues);

        void Debug(Exception exception, string message, params object[] propertyValues);

        void Debug(string message, params object[] propertyValues);

        void Information(Exception exception, string message, params object[] propertyValues);

        void Information(string message, params object[] propertyValues);

        void Warning(Exception exception, string message, params object[] propertyValues);

        void Warning(string message, params object[] propertyValues);
        
        void Error(Exception exception, string message, params object[] propertyValues);

        void Error(string message, params object[] propertyValues);
        
        void Fatal(Exception exception, string message, params object[] propertyValues);

        void Fatal(string message, params object[] propertyValues);
    }
}