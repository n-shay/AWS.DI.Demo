using TRG.Extensions.DependencyInjection;

namespace TRG.Extensions.Lambda
{
    public class ExecutionContext
    {
        public ExecutionContext(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public IServiceProvider ServiceProvider { get; }
    }
}