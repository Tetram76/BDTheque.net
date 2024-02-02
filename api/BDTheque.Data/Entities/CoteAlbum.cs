namespace BDTheque.Data.Entities;

using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.UnlimitedStringLength")]
[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.CircularDependency")]
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
public class CoteAlbum : EntityWithUniqueId
{
    public Guid EditionAlbumId { get; set; }
    public virtual EditionAlbum EditionAlbum { get; set; } = null!;

    [Range(1900, 2999)]
    public ushort Annee { get; set; }

    [Range(0, double.MaxValue)]
    public decimal Prix { get; set; }
}

public static partial class ModelBuilderExtensions
{
    public static void ApplyEntityConfiguration(this EntityTypeBuilder<CoteAlbum> entity)
    {
        entity.ToTable("CotesAlbums");

        SetupVersioning(entity);
        SetupUniqueIdPrimaryKey(entity);

        entity.HasIndex(
            e => new
            {
                e.EditionAlbumId,
                e.Annee
            }
        ).IsUnique();

        entity.Property(e => e.Prix).HasPrecision(8, 3);

        entity.HasOne(d => d.EditionAlbum).WithMany(p => p.CotesAlbums).HasForeignKey(d => d.EditionAlbumId);
    }
}
