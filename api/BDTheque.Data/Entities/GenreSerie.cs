namespace BDTheque.Data.Entities;

using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

public static partial class ModelBuilderExtensions
{
    public static void ApplyEntityConfiguration(this EntityTypeBuilder<GenreSerie> entity)
    {
        entity.ToTable("GenresSeries");

        SetupVersioning(entity);
        SetupUniqueIdPrimaryKey(entity);

        entity.HasIndex(
            e => new
            {
                e.GenreId,
                e.SerieId
            }
        ).IsUnique();

        entity.HasOne(d => d.Genre).WithMany(p => p.GenresSeries).HasForeignKey(d => d.GenreId);
        entity.HasOne(d => d.Serie).WithMany(p => p.GenresSeries).HasForeignKey(d => d.SerieId);
    }
}
