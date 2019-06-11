using System.IO;
using Microsoft.Extensions.Configuration;

namespace AWS.DI.Application.Lambda
{
    public class LambdaConfiguration : ILambdaConfiguration
    {
        private IConfigurationRoot _configuration;

        private readonly object _syncRoot = new object();

        public IConfigurationRoot Configuration => GetOrCreateInternal();

        private IConfigurationRoot GetOrCreateInternal()
        {
            if (_configuration == null)
            {
                lock (_syncRoot)
                {
                    if (_configuration == null)
                    {
                        _configuration = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .AddEnvironmentVariables()
                            .Build();
                    }
                }
            }

            return _configuration;
        }
    }
}