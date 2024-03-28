namespace BDTheque.Data.Repositories.Interfaces;

using BDTheque.Model.Interfaces;

using Microsoft.EntityFrameworkCore.ChangeTracking;

public interface IEditableEntityRepository<TEntity>
    where TEntity : class, IUniqueIdEntity
{
    EntityEntry<TEntity> GetEntry(TEntity entity);

    Task<EntityEntry<TEntity>> Add(TEntity entity, CancellationToken cancellationToken = default);
    Task<EntityEntry<TEntity>> Update(TEntity entity, CancellationToken cancellationToken = default);
    Task<EntityEntry<TEntity>> Remove(TEntity entity, CancellationToken cancellationToken = default);
}
