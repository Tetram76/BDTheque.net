namespace BDTheque.Model.Entities;

using System.Diagnostics.CodeAnalysis;
using BDTheque.Model.Entities.Abstract;

[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.UnlimitedStringLength")]
[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.CircularDependency")]
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
public class UniversSerie : UniqueIdEntity
{
    public Guid UniversId { get; set; }
    public virtual Univers Univers { get; set; } = null!;

    public Guid SerieId { get; set; }
    public virtual Serie Serie { get; set; } = null!;
}
