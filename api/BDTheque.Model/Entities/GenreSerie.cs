namespace BDTheque.Model.Entities;

using System.Diagnostics.CodeAnalysis;
using BDTheque.Model.Entities.Abstract;

[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.UnlimitedStringLength")]
[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.CircularDependency")]
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
public class GenreSerie : EntityWithUniqueId
{
    public Guid GenreId { get; set; }
    public virtual Genre Genre { get; set; } = null!;

    public Guid SerieId { get; set; }
    public virtual Serie Serie { get; set; } = null!;
}
