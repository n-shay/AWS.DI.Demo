namespace TRG.Extensions.DataAccess
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class UnitOfWork<TContext> : IUnitOfWork
        where TContext : IContext
    {
        private readonly ConcurrentDictionary<Type, IRepository> repositoriesContainer = new ConcurrentDictionary<Type, IRepository>();
        private readonly SemaphoreSlim mutex = new SemaphoreSlim(1, 1);
        private bool isDisposed;

        protected IContextProvider<TContext> ContextProvider { get; }

        protected UnitOfWork(IContextProvider<TContext> contextProvider)
        {
            this.ContextProvider = contextProvider;
        }

        protected abstract TRepository InstantiateRepository<TRepository>()
            where TRepository : class, IRepository;

        protected TRepository GetRepository<TRepository>()
            where TRepository : class, IRepository
        {
            var type = typeof(TRepository);

            return
                (TRepository)
                    this.repositoriesContainer.GetOrAdd(type, k => this.InstantiateRepository<TRepository>());
        }

        public virtual bool Save()
        {
            this.ClearRepositories();

            return true;
        }

        public virtual Task<bool> SaveAsync()
        {
            return Task.FromResult(this.Save());
        }

        // Public implementation of Dispose pattern callable by consumers. 
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
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
                this.ClearRepositories();

                this.ContextProvider?.Dispose();

                this.mutex.Dispose();
            }

            this.isDisposed = true;
        }

        private void ClearRepositories()
        {
            this.mutex.Wait();
            try
            {
                foreach (var repository in this.repositoriesContainer.Values)
                {
                    repository.Dispose();
                }

                this.repositoriesContainer.Clear();
            }
            finally
            {
                this.mutex.Release();
            }
        }
    }
}