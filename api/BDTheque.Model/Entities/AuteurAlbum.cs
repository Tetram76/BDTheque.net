namespace BDTheque.Model.Entities;

using System.Diagnostics.CodeAnalysis;

using BDTheque.Model.Entities.Abstract;

[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.UnlimitedStringLength")]
[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.CircularDependency")]
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
[ObjectType]
public class AuteurAlbum : VersioningEntity
{
    public Guid AlbumId { get; set; }
    public virtual Album Album { get; set; } = null!;

    public Guid AuteurId { get; set; }
    public virtual Auteur Auteur { get; set; } = null!;
}
