namespace BDTheque.Data.Repositories.Interfaces;

using BDTheque.Model.Entities;

public interface IGenreRepository : IUniqueIdRepository<Genre>, IEditableEntityRepository<Genre>
{
    Task<bool> IsNameAllowed(Genre genre, CancellationToken cancellationToken = default);
}
