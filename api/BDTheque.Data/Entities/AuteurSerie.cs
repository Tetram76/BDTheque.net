namespace BDTheque.Data.Entities;

using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.UnlimitedStringLength")]
[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.CircularDependency")]
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
public class AuteurSerie : EntityWithUniqueId
{
    public Guid AuteurId { get; set; }
    public virtual Auteur Auteur { get; set; } = null!;

    public Guid SerieId { get; set; }
    public virtual Serie Serie { get; set; } = null!;
}

public static partial class ModelBuilderExtensions
{
    public static void ApplyEntityConfiguration(this EntityTypeBuilder<AuteurSerie> entity)
    {
        entity.ToTable("AuteursSeries");

        SetupVersioning(entity);
        SetupUniqueIdPrimaryKey(entity);

        entity.HasIndex(
            e => new
            {
                e.AuteurId,
                e.SerieId
            }
        ).IsUnique();

        entity.HasOne(d => d.Auteur).WithMany(p => p.AuteursSeries).HasForeignKey(d => d.AuteurId);
        entity.HasOne(d => d.Serie).WithMany(p => p.AuteursSeries).HasForeignKey(d => d.SerieId);
    }
}
