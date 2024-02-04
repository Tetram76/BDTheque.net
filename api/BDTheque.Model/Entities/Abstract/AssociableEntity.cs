namespace BDTheque.Model.Entities.Abstract;

using BDTheque.Model.Interfaces;

public abstract class AssociableEntity : UniqueIdEntity, IAssociable
{
    public IList<string>? Associations { get; set; }
}
