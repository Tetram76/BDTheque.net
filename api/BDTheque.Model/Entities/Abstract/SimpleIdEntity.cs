namespace BDTheque.Model.Entities.Abstract;

using BDTheque.Model.Interfaces;

public abstract class SimpleIdEntity : VersioningEntity, ISimpleId
{
    public ushort Id { get; set; }
}
