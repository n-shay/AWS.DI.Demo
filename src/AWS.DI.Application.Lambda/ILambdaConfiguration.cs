using Microsoft.Extensions.Configuration;

namespace AWS.DI.Application.Lambda
{
    public interface ILambdaConfiguration
    {
        IConfigurationRoot Configuration { get; }
    }
}