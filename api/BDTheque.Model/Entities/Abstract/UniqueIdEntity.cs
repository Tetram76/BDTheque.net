namespace BDTheque.Model.Entities.Abstract;

using BDTheque.Model.Interfaces;

public abstract class UniqueIdEntity : VersioningEntity, IUniqueId
{
    [ID]
    public Guid Id { get; set; }
}
