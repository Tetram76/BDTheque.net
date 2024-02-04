namespace BDTheque.Model.Entities.Abstract;

using BDTheque.Model.Interfaces;

public abstract class AssociableEntity : EntityWithUniqueId, IAssociableEntity
{
    public IList<string>? Associations { get; set; }
}
