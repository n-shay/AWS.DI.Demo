using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace RV.Application.DataAccess
{
    public abstract class UnitOfWork<TContext> : IUnitOfWork
        where TContext : IContext
    {
        private readonly ConcurrentDictionary<Type, IRepository> _repositoriesContainer = new ConcurrentDictionary<Type, IRepository>();
        private readonly SemaphoreSlim _mutex = new SemaphoreSlim(1, 1);
        private bool _isDisposed;

        protected IContextProvider<TContext> ContextProvider { get; }

        protected UnitOfWork(IContextProvider<TContext> contextProvider)
        {
            ContextProvider = contextProvider;
        }

        protected abstract TRepository InstantiateRepository<TRepository>()
            where TRepository : class, IRepository;

        protected TRepository GetRepository<TRepository>()
            where TRepository : class, IRepository
        {
            var type = typeof(TRepository);

            return
                (TRepository)
                    _repositoriesContainer.GetOrAdd(type, k => InstantiateRepository<TRepository>());
        }

        public virtual bool Save()
        {
            ClearRepositories();

            return true;
        }

        public virtual Task<bool> SaveAsync()
        {
            return Task.FromResult(Save());
        }

        // Public implementation of Dispose pattern callable by consumers. 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern. 
        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposing)
            {
                ClearRepositories();

                ContextProvider?.Dispose();
            }

            _isDisposed = true;
        }

        private void ClearRepositories()
        {
            _mutex.Wait();
            try
            {
                foreach (var repository in _repositoriesContainer.Values)
                {
                    repository.Dispose();
                }

                _repositoriesContainer.Clear();
            }
            finally
            {
                _mutex.Release();
            }
        }
    }
}