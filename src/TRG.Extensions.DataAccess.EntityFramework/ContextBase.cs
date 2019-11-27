using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TRG.Extensions.DataAccess.EntityFramework
{
    public abstract class ContextBase : DbContext, IDbContext
    {
        // TODO: don't expose DbContextOptions
        protected ContextBase(DbContextOptions options)
            : base(options)
        {
        }

        public bool IsDetached(object entity)
        {
            return this.Entry(entity).State == EntityState.Detached;
        }

        public bool Save()
        {
            return this.SaveChanges() > 0;
        }

        public async Task<bool> SaveAsync(CancellationToken cancellationToken = default)
        {
            var result = await this.SaveChangesAsync(cancellationToken);
            return result > 0;
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}