using System;
using TRG.Extensions.DataAccess.Specification;
using TRG.Extensions.Logging;

namespace TRG.Extensions.DataAccess.DynamoDB
{
    public abstract class ReadableRepository<TEntity> :
        RepositoryBase,
        IReadableRepository<TEntity>
        where TEntity : class, IEntity
    {
        protected ReadableRepository(ILogger logger, IDbContext context)
            : base(logger, context)
        {
        }
    }
}