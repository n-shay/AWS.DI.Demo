using System;
using RV.Application.Logging;

namespace RV.Application.DataAccess.DynamoDB
{
    public class RepositoryBase : IRepository
    {
        private readonly IContextProvider<IDbContext> _contextProvider;
        private bool _isDisposed;

        protected ILogger Logger { get; }

        /// <summary>
        /// The Context.
        /// </summary>
        protected internal IDbContext Context => _contextProvider.Get() ?? throw new DynamoDbException("Data access context was not initialized.");

        protected RepositoryBase(ILogger logger, IContextProvider<IDbContext> contextProvider)
        {
            _contextProvider = contextProvider;
            Logger = logger;
        }

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
                // disposing here
            }

            _isDisposed = true;
        }
    }
}