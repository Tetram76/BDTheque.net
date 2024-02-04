namespace BDTheque.Model.Entities;

using System.Diagnostics.CodeAnalysis;
using BDTheque.Model.Entities.Abstract;

[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.UnlimitedStringLength")]
[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.CircularDependency")]
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
public class UniversAlbum : EntityWithUniqueId
{
    public Guid UniversId { get; set; }
    public virtual Univers Univers { get; set; } = null!;

    public Guid AlbumId { get; set; }
    public virtual Album Album { get; set; } = null!;

    public bool FromSerie { get; set; }
}
