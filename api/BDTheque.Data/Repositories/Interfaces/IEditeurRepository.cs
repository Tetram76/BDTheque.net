namespace BDTheque.Data.Repositories;

using BDTheque.Model.Entities;

public interface IEditeurRepository : IUniqueIdRepository<Editeur>, IEditableEntityRepository<Editeur>
{
    Task<bool> IsNameAllowed(Editeur editeur, CancellationToken cancellationToken = default);
}
