namespace BDTheque.Model.Interfaces;

internal interface IEntityWithOptionalLabel : IAssociableEntity
{
    public char? Initiale { get; set; }
}
