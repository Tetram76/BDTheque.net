namespace BDTheque.Data.Repositories;

using BDTheque.Data.Context;
using BDTheque.Model.Interfaces;

public interface IEntityRepository<TEntity, in TKey>
    where TEntity : class, IEntity<TKey>
{
    BDThequeContext DbContext { get; }

    Task<TEntity> GetById(TKey id, CancellationToken cancellationToken = default);
    Task<TEntity?> GetByIdOrDefault(TKey id, CancellationToken cancellationToken = default);
}
