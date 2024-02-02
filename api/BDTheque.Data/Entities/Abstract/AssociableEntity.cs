namespace BDTheque.Data.Entities;

public abstract class AssociableEntity : EntityWithUniqueId
{
    public IList<string>? Associations { get; set; }
}
