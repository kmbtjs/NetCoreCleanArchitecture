using App.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace App.Persistence.Interceptors
{
    public class AuditDbContextInterceptor: SaveChangesInterceptor
    {

        private static readonly Dictionary<EntityState, Action<DbContext, IAuditEntity>> Behaviors = new()
        {
            { EntityState.Added, AddBehavior },
            { EntityState.Modified, ModifiedBehavior }
        };

        private static void AddBehavior(DbContext dbContext, IAuditEntity auditEntity)
        {
            auditEntity.Created = DateTime.Now;
            dbContext.Entry(auditEntity).Property(x=> x.Updated).IsModified = false;
        }

        private static void ModifiedBehavior(DbContext dbContext, IAuditEntity auditEntity)
        {
            auditEntity.Updated = DateTime.Now;
            dbContext.Entry(auditEntity).Property(x=> x.Created).IsModified = false;
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entityEntry in eventData.Context!.ChangeTracker.Entries().ToList())
            {
                if (entityEntry.Entity is not IAuditEntity auditEntity) continue;

                if (entityEntry.State is not EntityState.Added and not EntityState.Modified) continue;

                Behaviors[entityEntry.State].Invoke(eventData.Context, auditEntity);
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
