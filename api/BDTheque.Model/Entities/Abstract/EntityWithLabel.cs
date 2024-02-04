namespace BDTheque.Model.Entities.Abstract;

using BDTheque.Model.Interfaces;

public abstract class EntityWithLabel : AssociableEntity, IEntityWithLabel
{
    public char Initiale { get; set; }
}
