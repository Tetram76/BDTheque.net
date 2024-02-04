namespace BDTheque.Model.Entities.Abstract;

using BDTheque.Model.Interfaces;

public abstract class MandatoryLabelEntity : AssociableEntity, IMandatoryLabel
{
    public char Initiale { get; set; }
}
