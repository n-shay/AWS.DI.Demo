namespace TRG.Extensions.Logging.Serilog
{
    using System;

    using TRG.Extensions.Logging;

    public class SerilogLogger<T> : ILogger
    {
        private readonly global::Serilog.ILogger logger;

        public SerilogLogger()
        {
            this.logger = global::Serilog.Log.ForContext<T>();
        }

        public void Verbose(Exception exception, string message, params object[] propertyValues)
        {
            this.logger.Verbose(exception, message, propertyValues);
        }

        public void Verbose(string message, params object[] propertyValues)
        {
            this.logger.Verbose(message, propertyValues);
        }

        public void Debug(Exception exception, string message, params object[] propertyValues)
        {
            this.logger.Debug(exception, message, propertyValues);
        }

        public void Debug(string message, params object[] propertyValues)
        {
            this.logger.Debug(message, propertyValues);
        }

        public void Information(Exception exception, string message, params object[] propertyValues)
        {
            this.logger.Information(exception, message, propertyValues);
        }

        public void Information(string message, params object[] propertyValues)
        {
            this.logger.Information(message, propertyValues);
        }

        public void Warning(Exception exception, string message, params object[] propertyValues)
        {
            this.logger.Warning(exception, message, propertyValues);
        }

        public void Warning(string message, params object[] propertyValues)
        {
            this.logger.Warning(message, propertyValues);
        }

        public void Error(Exception exception, string message, params object[] propertyValues)
        {
            this.logger.Error(exception, message, propertyValues);
        }

        public void Error(string message, params object[] propertyValues)
        {
            this.logger.Error(message, propertyValues);
        }

        public void Fatal(Exception exception, string message, params object[] propertyValues)
        {
            this.logger.Fatal(exception, message, propertyValues);
        }

        public void Fatal(string message, params object[] propertyValues)
        {
            this.logger.Fatal(message, propertyValues);
        }
    }
}