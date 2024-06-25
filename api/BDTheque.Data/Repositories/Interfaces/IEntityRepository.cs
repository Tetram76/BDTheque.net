namespace BDTheque.Data.Repositories.Interfaces;

using BDTheque.Model.Interfaces;

public interface IEntityRepository<TEntity, in TKey>
    where TEntity : class, IEntity<TKey>
{
    Task<TEntity> GetById(TKey id, CancellationToken cancellationToken = default);
    Task<TEntity?> GetByIdOrDefault(TKey id, CancellationToken cancellationToken = default);
}
