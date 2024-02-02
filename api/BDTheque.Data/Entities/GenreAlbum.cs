namespace BDTheque.Data.Entities;

using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.UnlimitedStringLength")]
[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.CircularDependency")]
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
public class GenreAlbum : EntityWithUniqueId
{
    public Guid GenreId { get; set; }
    public virtual Genre Genre { get; set; } = null!;

    public Guid AlbumId { get; set; }
    public virtual Album Album { get; set; } = null!;

    public bool FromSerie { get; set; } = false;
}

public static partial class ModelBuilderExtensions
{
    public static void ApplyEntityConfiguration(this EntityTypeBuilder<GenreAlbum> entity)
    {
        entity.ToTable("GenresAlbums");

        SetupVersioning(entity);
        SetupUniqueIdPrimaryKey(entity);

        entity.HasIndex(
            e => new
            {
                e.GenreId,
                e.AlbumId
            }
        ).IsUnique();

        entity.Property(e => e.FromSerie).HasDefaultValue(false);

        entity.HasOne(d => d.Album).WithMany(p => p.GenresAlbums).HasForeignKey(d => d.AlbumId);
        entity.HasOne(d => d.Genre).WithMany(p => p.GenresAlbums).HasForeignKey(d => d.GenreId);
    }
}
