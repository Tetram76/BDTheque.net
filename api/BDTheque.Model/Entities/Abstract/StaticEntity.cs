namespace BDTheque.Model.Entities.Abstract;

using BDTheque.Model.Interfaces;

[StaticEntity]
public class StaticEntity : BaseEntity, IEntity<ushort>
{
    [ID]
    public ushort Id { get; set; }

    public static implicit operator ushort(StaticEntity entity) =>
        entity.Id;
}
