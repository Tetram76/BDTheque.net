namespace BDTheque.Data.Entities;

using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.UnlimitedStringLength")]
[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.CircularDependency")]
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
public class UniversAlbum : EntityWithUniqueId
{
    public Guid UniversId { get; set; }
    public virtual Univers Univers { get; set; } = null!;

    public Guid AlbumId { get; set; }
    public virtual Album Album { get; set; } = null!;

    public bool FromSerie { get; set; } = false;
}

public static partial class ModelBuilderExtensions
{
    public static void ApplyEntityConfiguration(this EntityTypeBuilder<UniversAlbum> entity)
    {
        entity.ToTable("UniversAlbums");

        SetupVersioning(entity);
        SetupUniqueIdPrimaryKey(entity);

        entity.HasIndex(
            e => new
            {
                e.UniversId,
                e.AlbumId
            }
        ).IsUnique();

        entity.Property(e => e.FromSerie).HasDefaultValue(false);

        entity.HasOne(d => d.Album).WithMany(p => p.UniversAlbums).HasForeignKey(d => d.AlbumId);
        entity.HasOne(d => d.Univers).WithMany(p => p.UniversAlbums).HasForeignKey(d => d.UniversId);
    }
}
