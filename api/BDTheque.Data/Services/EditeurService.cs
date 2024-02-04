namespace BDTheque.Data.Services;

using BDTheque.Data.Context;
using BDTheque.Model.Entities;

public class EditeurService(BDThequeContext context)
{
    public List<Editeur> GetAllEditeurs() => context.Editeurs.ToList();
}
