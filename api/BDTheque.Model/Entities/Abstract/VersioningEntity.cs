namespace BDTheque.Model.Entities.Abstract;

using BDTheque.Model.Attributes;
using BDTheque.Model.Interfaces;

[ApplyDefaultObjectMapping]
public abstract class VersioningEntity : IVersioning
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
