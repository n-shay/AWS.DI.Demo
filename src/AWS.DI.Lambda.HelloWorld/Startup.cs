using Amazon.Lambda.Core;
using AWS.DI.Business;
using AWS.DI.DependencyResolution;
using AWS.DI.Lambda.HelloWorld.Models;

namespace AWS.DI.Lambda.HelloWorld
{
    public class Startup
    {
        private readonly IServiceProvider _serviceProvider;

        public Startup(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Startup() : this(new CurrentAssemblyServiceProvider())
        {
            
        }

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public Output Handler(Input input, ILambdaContext context)
        {
            var speakService = _serviceProvider.Resolve<ISpeakService>();
            var output =  new Output
            {
                Message = speakService.SaySomething()
            };

            return output;
        }
    }
}
