namespace BDTheque.Data.Repositories;

using BDTheque.Data.Context;
using BDTheque.Data.Repositories.Interfaces;
using BDTheque.Model.Entities;

using Microsoft.EntityFrameworkCore;

public class GenreRepository(BDThequeContext dbContext, IServiceProvider serviceProvider)
    : EditableEntityRepository<Genre>(dbContext, serviceProvider), IGenreRepository
{
    public async Task<bool> IsNameAllowed(Genre genre, CancellationToken cancellationToken = default) =>
        !await DbContext.Genres.AnyAsync(
            g => g.Id != genre.Id
                 && g.Nom.ToLower().Equals(genre.Nom.ToLower()),
            cancellationToken
        );
}
