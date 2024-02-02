namespace BDTheque.Data.Entities;

using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.UnlimitedStringLength")]
[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.CircularDependency")]
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
public class UniversSerie : EntityWithUniqueId
{
    public Guid UniversId { get; set; }
    public virtual Univers Univers { get; set; } = null!;

    public Guid SerieId { get; set; }
    public virtual Serie Serie { get; set; } = null!;
}

public static partial class ModelBuilderExtensions
{
    public static void ApplyEntityConfiguration(this EntityTypeBuilder<UniversSerie> entity)
    {
        entity.ToTable("UniversSeries");

        SetupVersioning(entity);
        SetupUniqueIdPrimaryKey(entity);

        entity.HasIndex(
            e => new
            {
                e.UniversId,
                e.SerieId
            }
        ).IsUnique();

        entity.HasOne(d => d.Serie).WithMany(p => p.UniversSeries).HasForeignKey(d => d.SerieId);
        entity.HasOne(d => d.Univers).WithMany(p => p.UniversSeries).HasForeignKey(d => d.UniversId);
    }
}
