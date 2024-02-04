namespace BDTheque.Model.Interfaces;

internal interface IEntityWithUniqueId : IBaseEntity
{
    public Guid Id { get; set; }
}
