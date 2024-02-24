namespace BDTheque.Model.Entities.Abstract;

using BDTheque.Model.Interfaces;

public abstract class OptionalLabelEntity : AssociableEntity, IOptionalLabel
{
    public char? Initiale { get; set; }
}
