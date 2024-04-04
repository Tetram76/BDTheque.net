namespace BDTheque.Model.Entities.Abstract;

using BDTheque.Model.Interfaces;

public abstract class AssociableEntity : UniqueIdEntity, IAssociableEntity
{
    public IList<string>? Associations { get; set; } = new List<string>();
}
