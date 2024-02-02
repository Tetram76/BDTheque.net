namespace BDTheque.Data.Entities;

using System.Diagnostics.CodeAnalysis;
using BDTheque.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.UnlimitedStringLength")]
[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.CircularDependency")]
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
public class Image : EntityWithUniqueId
{
    public string? Titre { get; set; }

    public byte[] Bytes { get; set; } = null!;

    public ushort Ordre { get; set; }

    public ushort TypeId { get; set; }
    public virtual Option Type { get; set; } = null!;

    public Guid EditionId { get; set; }
    public virtual EditionAlbum Edition { get; set; } = null!;
}

public static partial class ModelBuilderExtensions
{
    public static void ApplyEntityConfiguration(this EntityTypeBuilder<Image> entity)
    {
        entity.ToTable("Images");

        SetupVersioning(entity);
        SetupUniqueIdPrimaryKey(entity);

        entity.HasIndex(
            e => new
            {
                e.EditionId,
                e.TypeId,
                e.Ordre
            }
        ).IsUnique();

        entity.Property(e => e.Titre).UseCollation(BDThequeContext.FrenchCollation);

        entity.HasOne(d => d.Edition).WithMany(p => p.Images).HasForeignKey(d => d.EditionId);
        entity.HasOne(d => d.Type).WithMany(p => p.Images).HasForeignKey(d => d.TypeId).OnDelete(DeleteBehavior.Restrict);
    }
}
