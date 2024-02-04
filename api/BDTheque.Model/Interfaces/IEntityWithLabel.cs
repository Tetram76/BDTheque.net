namespace BDTheque.Model.Interfaces;

internal interface IEntityWithLabel : IAssociableEntity
{
    public char Initiale { get; set; }
}
