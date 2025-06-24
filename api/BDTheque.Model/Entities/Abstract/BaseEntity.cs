namespace BDTheque.Model.Entities.Abstract;

using BDTheque.Model.Interfaces;

[ApplyObjectMapping]
[ApplyFilterInputMapping]
public abstract class BaseEntity : IBaseEntity, IVersioningEntity
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
