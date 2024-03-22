namespace BDTheque.Data.Repositories;

using BDTheque.Model.Entities;

public interface IUniversRepository : IUniqueIdRepository<Univers>, IEditableEntityRepository<Univers>
{
    Task<bool> IsNameAllowed(Univers univers, CancellationToken cancellationToken = default);
}
