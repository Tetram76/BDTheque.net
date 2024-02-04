namespace BDTheque.Model.Interfaces;

public interface IEntityWithLabel : IAssociableEntity
{
    public char Initiale { get; set; }
}
