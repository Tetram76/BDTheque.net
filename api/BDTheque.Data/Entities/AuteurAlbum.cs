namespace BDTheque.Data.Entities;

using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.UnlimitedStringLength")]
[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.CircularDependency")]
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
public class AuteurAlbum : EntityWithUniqueId
{
    public Guid AlbumId { get; set; }
    public virtual Album Album { get; set; } = null!;

    public Guid AuteurId { get; set; }
    public virtual Auteur Auteur { get; set; } = null!;
}

public static partial class ModelBuilderExtensions
{
    public static void ApplyEntityConfiguration(this EntityTypeBuilder<AuteurAlbum> entity)
    {
        entity.ToTable("AuteursAlbums");

        SetupVersioning(entity);
        SetupUniqueIdPrimaryKey(entity);

        entity.HasIndex(
            e => new
            {
                e.AuteurId,
                e.AlbumId
            }
        ).IsUnique();

        entity.HasOne(d => d.Album).WithMany(p => p.AuteursAlbums).HasForeignKey(d => d.AlbumId);
        entity.HasOne(d => d.Auteur).WithMany(p => p.AuteursAlbums).HasForeignKey(d => d.AuteurId);
    }
}
