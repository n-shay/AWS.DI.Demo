namespace TRG.Extensions.DataAccess.EntityFramework
{
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    public interface IDbContext : IContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        bool IsDetached(object entity);

        /// <summary>
        /// Persists all changes to the backing data store.
        /// </summary>
        /// <returns>True if successful.</returns>
        bool Save();

        /// <summary>
        /// Persists all changes to the backing data store.
        /// </summary>
        /// <returns>True if successful.</returns>
        Task<bool> SaveAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
