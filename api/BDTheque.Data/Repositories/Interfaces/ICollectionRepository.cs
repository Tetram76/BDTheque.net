namespace BDTheque.Data.Repositories.Interfaces;

using BDTheque.Model.Entities;

public interface ICollectionRepository : IUniqueIdRepository<Collection>, IEditableEntityRepository<Collection>
{
    Task<bool> IsNameAllowed(Collection collection, CancellationToken cancellationToken = default);
}
