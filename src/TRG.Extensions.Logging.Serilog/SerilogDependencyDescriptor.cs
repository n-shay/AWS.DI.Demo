using TRG.Extensions.DependencyInjection;

namespace TRG.Extensions.Logging.Serilog
{
    internal class SerilogDependencyDescriptor : DependencyDescriptor
    {
        protected override void Register(RegistrationBuilder builder)
        {
            builder.Include(container =>
            {
                container.RegisterConditional(
                    typeof(ILogger),
                    c => typeof(SerilogLogger<>).MakeGenericType(c.Consumer.ImplementationType),
                    SimpleInjector.Lifestyle.Singleton,
                    c => c.ImplementationType != typeof(object));
            });
        }
    }
}