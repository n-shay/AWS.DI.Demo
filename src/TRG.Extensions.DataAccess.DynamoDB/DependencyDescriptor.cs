namespace TRG.Extensions.DataAccess.DynamoDB
{
    using Autofac;

    public class DependencyDescriptor : TRG.Extensions.DependencyInjection.DependencyDescriptor
    {
        public const string UNIT_OF_WORK_NAMED_SCOPE = "UnitOfWork";

        protected override void Register(RegistrationBuilder builder)
        {
            builder.Include(container =>
            {
                container.RegisterGeneric(typeof(ContextProvider<>))
                    .Named(UNIT_OF_WORK_NAMED_SCOPE, typeof(IContextProvider<>));

                container.RegisterType<UnitOfWorkFactory>()
                    .As<IUnitOfWorkFactory>();
            });
        }
    }
}