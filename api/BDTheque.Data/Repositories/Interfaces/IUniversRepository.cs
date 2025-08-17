namespace BDTheque.Data.Repositories.Interfaces;

using BDTheque.Model.Entities;

public interface IUniversRepository : IUniqueIdRepository<Univers>, IEditableEntityRepository<Univers>
{
    Task<bool> IsNameAllowed(Univers univers, CancellationToken cancellationToken = default);
}
