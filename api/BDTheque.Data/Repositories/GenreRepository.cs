namespace BDTheque.Data.Repositories;

using BDTheque.Data.Context;
using BDTheque.Model.Entities;
using Microsoft.EntityFrameworkCore;

public class GenreRepository(IServiceProvider serviceProvider, BDThequeContext dbContext)
    : EditableEntityRepository<Genre>(serviceProvider, dbContext), IGenreRepository
{
    public async Task<bool> IsNameAllowed(Genre genre, CancellationToken cancellationToken = default) =>
        !await DbContext.Genres.AnyAsync(
            g => g.Id != genre.Id
                 && g.Nom.ToLower().Equals(genre.Nom.ToLower()),
            cancellationToken
        );
}
