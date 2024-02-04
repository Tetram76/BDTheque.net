namespace BDTheque.Model.Interfaces;

public interface IEntityWithUniqueId : IBaseEntity
{
    public Guid Id { get; set; }
}
