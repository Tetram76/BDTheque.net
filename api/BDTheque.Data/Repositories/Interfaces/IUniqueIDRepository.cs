namespace BDTheque.Data.Repositories;

using BDTheque.Model.Interfaces;

public interface IUniqueIdRepository<TEntity> : IEntityRepository<TEntity, Guid>
    where TEntity : class, IUniqueIdEntity
{
}
