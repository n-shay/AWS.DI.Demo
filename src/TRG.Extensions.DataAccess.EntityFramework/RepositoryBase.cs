
namespace TRG.Extensions.DataAccess.EntityFramework
{
    using TRG.Extensions.DependencyInjection;
    using TRG.Extensions.Logging;

    public class RepositoryBase : IRepository
    {
        private readonly IContextProvider<IDbContext> contextProvider;
        private bool isDisposed;

        protected ILogger Logger { get; }

        protected IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// The Context.
        /// </summary>
        protected internal IDbContext Context => this.contextProvider.Get() ?? throw new EntityFrameworkException("Data access context was not initialized.");
        
        protected RepositoryBase(ILogger logger, IContextProvider<IDbContext> contextProvider, IServiceProvider serviceProvider)
        {
            this.contextProvider = contextProvider;
            this.Logger = logger;
            this.ServiceProvider = serviceProvider;
        }

        public void Dispose()
        {
            this.Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern. 
        protected virtual void Dispose(bool disposing)
        {
            if (this.isDisposed)
            {
                return;
            }

            if (disposing)
            {
                // disposing here
                this.contextProvider.Dispose();
            }

            this.isDisposed = true;
        }
    }
}