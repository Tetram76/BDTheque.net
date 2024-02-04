namespace BDTheque.Model.Entities.Abstract;

using BDTheque.Model.Interfaces;

public abstract class EntityWithUniqueId : BaseEntity, IEntityWithUniqueId
{
    public Guid Id { get; set; }
}
