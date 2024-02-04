namespace BDTheque.Model.Entities.Abstract;

using BDTheque.Model.Interfaces;

public abstract class EntityWithOptionalLabel : AssociableEntity, IEntityWithOptionalLabel
{
    public char? Initiale { get; set; }
}
