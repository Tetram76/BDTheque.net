namespace BDTheque.Model.Entities.Abstract;

using BDTheque.Model.Interfaces;

[ApplyObjectMapping]
[ApplyFilterInputMapping]
[ApplyMutationInputMapping]
public abstract class VersioningEntity : IVersioning
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
