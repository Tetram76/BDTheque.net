namespace BDTheque.Model.Entities.Abstract;

using BDTheque.Model.Interfaces;

public abstract class EntityWithId : BaseEntity, IEntityWithId
{
    public ushort Id { get; set; }
}
