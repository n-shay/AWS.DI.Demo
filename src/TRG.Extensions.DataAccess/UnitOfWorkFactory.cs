namespace TRG.Extensions.DataAccess
{
    using TRG.Extensions.DependencyInjection;

    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IServiceProvider serviceProvider;

        public UnitOfWorkFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public TUoW Create<TUoW>() where TUoW : IUnitOfWork
        {
            return this.serviceProvider.Resolve<TUoW>();
        }
    }
}