namespace BDTheque.Data.Services;

using BDTheque.Data.Context;
using BDTheque.Model.Entities;

public class SerieService(BDThequeContext context)
{
    public List<Serie> GetAllSeries() => context.Series.ToList();
}
