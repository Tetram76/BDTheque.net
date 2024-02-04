namespace BDTheque.Model.Interfaces;

public interface IEntityWithOptionalLabel : IAssociableEntity
{
    public char? Initiale { get; set; }
}
