namespace BDTheque.Data.Services;

using BDTheque.Data.Context;
using BDTheque.Data.Entities;

public class GenreService(BDThequeContext context)
{
    public List<Genre> GetAllGenres() => context.Genres.ToList();
}
