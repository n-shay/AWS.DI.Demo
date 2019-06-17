using System;

namespace TRG.Extensions.Logging.Serilog
{
    public class SerilogLogger<T> : ILogger
    {
        private readonly global::Serilog.ILogger _logger;

        public SerilogLogger()
        {
            _logger = global::Serilog.Log.Logger.ForContext<T>();
        }

        public void Verbose(Exception exception, string message, params object[] propertyValues)
        {
            _logger.Verbose(exception, message, propertyValues);
        }

        public void Verbose(string message, params object[] propertyValues)
        {
            _logger.Verbose(message, propertyValues);
        }

        public void Debug(Exception exception, string message, params object[] propertyValues)
        {
            _logger.Debug(exception, message, propertyValues);
        }

        public void Debug(string message, params object[] propertyValues)
        {
            _logger.Debug(message, propertyValues);
        }

        public void Information(Exception exception, string message, params object[] propertyValues)
        {
            _logger.Information(exception, message, propertyValues);
        }

        public void Information(string message, params object[] propertyValues)
        {
            _logger.Information(message, propertyValues);
        }

        public void Warning(Exception exception, string message, params object[] propertyValues)
        {
            _logger.Warning(exception, message, propertyValues);
        }

        public void Warning(string message, params object[] propertyValues)
        {
            _logger.Warning(message, propertyValues);
        }

        public void Error(Exception exception, string message, params object[] propertyValues)
        {
            _logger.Error(exception, message, propertyValues);
        }

        public void Error(string message, params object[] propertyValues)
        {
            _logger.Error(message, propertyValues);
        }

        public void Fatal(Exception exception, string message, params object[] propertyValues)
        {
            _logger.Fatal(exception, message, propertyValues);
        }

        public void Fatal(string message, params object[] propertyValues)
        {
            _logger.Fatal(message, propertyValues);
        }
    }
}