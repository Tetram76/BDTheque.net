namespace BDTheque.Data.Services;

using BDTheque.Data.Context;
using BDTheque.Model.Entities;

public class PersonneService(BDThequeContext context)
{
    public List<Personne> GetAllPersonnes() => context.Personnes.ToList();
}
