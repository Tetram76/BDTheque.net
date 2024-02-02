namespace BDTheque.Data.Entities;

public abstract class EntityWithOptionalLabel : AssociableEntity
{
    public char? Initiale { get; set; }
}
