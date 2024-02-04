namespace BDTheque.Model.Entities.Abstract;

using BDTheque.Model.Interfaces;

public abstract class UniqueIdEntity : VersioningEntity, IUniqueId
{
    public Guid Id { get; set; }
}
