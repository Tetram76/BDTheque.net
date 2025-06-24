namespace BDTheque.Data.Repositories.Interfaces;

using BDTheque.Model.Entities;

public interface IPersonneRepository : IUniqueIdRepository<Personne>, IEditableEntityRepository<Personne>
{
    Task<bool> IsNameAllowed(Personne personne, CancellationToken cancellationToken = default);
}
