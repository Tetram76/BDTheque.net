namespace BDTheque.Data.Repositories;

using BDTheque.Model.Entities;

public interface IGenreRepository : IUniqueIdRepository<Genre>, IEditableEntityRepository<Genre>
{
    Task<bool> IsNameAllowed(Genre genre, CancellationToken cancellationToken = default);
}
