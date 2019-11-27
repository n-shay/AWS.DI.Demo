using System.Threading.Tasks;
using TRG.Extensions.DependencyInjection;

namespace TRG.Extensions.DataAccess.EntityFramework.UnitOfWork
{
    public abstract class UnitOfWork : UnitOfWork<IDbContext>
    {
        private readonly IServiceProvider serviceProvider;

        protected UnitOfWork(IContextProvider<IDbContext> contextProvider, IServiceProvider serviceProvider)
            : base(contextProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        protected override TRepository InstantiateRepository<TRepository>()
        {
            var rep =
                this.serviceProvider.Resolve<TRepository>(this.ContextProvider);

            if (rep == null)
                throw new DependencyResolutionException(typeof(TRepository));

            return rep;
        }

        public override bool Save()
        {
            var context = this.ContextProvider.Get();

            if (context == null)
                return false;

            return context.Save() && base.Save();
        }

        public override async Task<bool> SaveAsync()
        {
            var context = this.ContextProvider.Get();

            if (context == null)
                return false;

            return await context.SaveAsync() && await base.SaveAsync();
        }
    }
}