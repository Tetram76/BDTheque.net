namespace BDTheque.Model.Entities.Abstract;

using BDTheque.Model.Interfaces;

public abstract class MandatoryLabelEntity : AssociableEntity, IMandatoryLabel
{
    [GraphQLIgnore]
    public char InitialeChar { get => Initiale.Length > 0 ? Initiale[0] : '\0'; set => Initiale = value.ToString(); }

    public string Initiale { get; set; } = string.Empty;
}
