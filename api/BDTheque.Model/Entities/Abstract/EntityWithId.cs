namespace BDTheque.Model.Entities.Abstract;

using BDTheque.Model.Interfaces;

public abstract class EntityWithId : BaseEntity, ISimpleId
{
    public ushort Id { get; set; }
}
