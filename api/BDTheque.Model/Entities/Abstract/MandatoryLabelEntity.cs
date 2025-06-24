namespace BDTheque.Model.Entities.Abstract;

using BDTheque.Model.Interfaces;

public abstract class MandatoryLabelEntity : AssociableEntity, IMandatoryLabelEntity
{
    public char Initiale { get; set; } = '\0';
}
