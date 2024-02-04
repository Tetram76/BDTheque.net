namespace BDTheque.Model.Interfaces;

internal interface IAssociableEntity : IEntityWithUniqueId
{
    public IList<string>? Associations { get; set; }
}
