namespace BDTheque.Model.Entities.Abstract;

using BDTheque.Model.Interfaces;

public abstract class EntityWithUniqueId : BaseEntity, IUniqueId
{
    public Guid Id { get; set; }
}
