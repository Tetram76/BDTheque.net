namespace BDTheque.Model.Interfaces;

public interface IAssociableEntity : IEntityWithUniqueId
{
    public IList<string>? Associations { get; set; }
}
