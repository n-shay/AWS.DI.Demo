using System;
using TRG.Extensions.Logging;

namespace TRG.Extensions.DataAccess.DynamoDB
{
    public class RepositoryBase : IRepository
    {
        private bool _isDisposed;

        protected ILogger Logger { get; }

        /// <summary>
        /// The Context.
        /// </summary>
        protected internal IDbContext Context { get; }
        
        protected RepositoryBase(ILogger logger, IDbContext context)
        {
            Logger = logger;
            Context = context;
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