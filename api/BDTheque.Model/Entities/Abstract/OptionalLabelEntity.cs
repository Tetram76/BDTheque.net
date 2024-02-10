namespace BDTheque.Model.Entities.Abstract;

using BDTheque.Model.Interfaces;

public abstract class OptionalLabelEntity : AssociableEntity, IOptionalLabel
{
    [GraphQLIgnore]
    public char? InitialeChar { get => Initiale?[0]; set => Initiale = value?.ToString(); }

    public string? Initiale { get; set; }
}
