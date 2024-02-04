namespace BDTheque.Model.Entities.Abstract;

using BDTheque.Model.Interfaces;

public abstract class EntityWithLabel : AssociableEntity, IMandatoryLabel
{
    public char Initiale { get; set; }
}
