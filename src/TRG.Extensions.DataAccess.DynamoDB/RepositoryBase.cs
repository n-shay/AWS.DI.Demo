namespace TRG.Extensions.DataAccess.DynamoDB
{
    using System;

    using TRG.Extensions.Logging;

    public class RepositoryBase : IRepository
    {
        private readonly IContextProvider<IDbContext> contextProvider;
        private bool isDisposed;

        protected ILogger Logger { get; }

        /// <summary>
        /// The Context.
        /// </summary>
        protected internal IDbContext Context => this.contextProvider.Get() ?? throw new DynamoDbException("Data access context was not initialized.");

        protected RepositoryBase(ILogger logger, IContextProvider<IDbContext> contextProvider)
        {
            this.contextProvider = contextProvider;
            this.Logger = logger;
        }

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
                // disposing here
            }

            this.isDisposed = true;
        }
    }
}