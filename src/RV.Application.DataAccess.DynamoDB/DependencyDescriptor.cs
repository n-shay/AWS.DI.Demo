using Autofac;

namespace RV.Application.DataAccess.DynamoDB
{
    public class DependencyDescriptor : DependencyInjection.DependencyDescriptor
    {
        public const string UnitOfWorkNamedScope = "UnitOfWork";

        protected override void Register(RegistrationBuilder builder)
        {
            builder.Include(container =>
            {
                container.RegisterGeneric(typeof(ContextProvider<>))
                    .Named(UnitOfWorkNamedScope, typeof(IContextProvider<>));

                container.RegisterType<UnitOfWorkFactory>()
                    .As<IUnitOfWorkFactory>();
            });
        }
    }
}