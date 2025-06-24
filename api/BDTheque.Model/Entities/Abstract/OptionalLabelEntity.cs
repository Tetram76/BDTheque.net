namespace BDTheque.Model.Entities.Abstract;

using BDTheque.Model.Interfaces;

public abstract class OptionalLabelEntity : AssociableEntity, IOptionalLabelEntity
{
    public char? Initiale { get; set; }
}
