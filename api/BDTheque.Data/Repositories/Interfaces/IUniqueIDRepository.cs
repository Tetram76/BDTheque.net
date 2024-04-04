namespace BDTheque.Data.Repositories.Interfaces;

using BDTheque.Model.Interfaces;

public interface IUniqueIdRepository<TEntity> : IEntityRepository<TEntity, Guid>
    where TEntity : class, IUniqueIdEntity
{
}
