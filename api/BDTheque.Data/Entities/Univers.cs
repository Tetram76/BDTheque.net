namespace BDTheque.Data.Entities;

using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using BDTheque.Data.Context;
using BDTheque.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.UnlimitedStringLength")]
[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.CircularDependency")]
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
public class Univers : EntityWithLabel
{
    public string Nom { get; set; } = null!;
    public string NomRaw { get; set; } = null!;

    public string? Description { get; set; }
    public string? DescriptionRaw { get; set; }

    [Url]
    public string? SiteWeb { get; set; }

    public Guid UniversRacineId { get; set; }
    public virtual Univers UniversRacine { get; set; } = null!;
    public Guid? UniversParentId { get; set; }
    public virtual Univers? UniversParent { get; set; }

    public List<Guid>? UniversBranches { get; set; }

    public virtual ICollection<Univers> UniversParents { get; set; } = new List<Univers>();
    public virtual ICollection<Univers> UniversRacines { get; set; } = new List<Univers>();

    public virtual ICollection<UniversAlbum> UniversAlbums { get; set; } = new List<UniversAlbum>();
    public virtual ICollection<UniversSerie> UniversSeries { get; set; } = new List<UniversSerie>();
}

public static partial class ModelBuilderExtensions
{
    public static void ApplyEntityConfiguration(this EntityTypeBuilder<Univers> entity)
    {
        entity.ToTable("Univers");

        SetupVersioning(entity);
        SetupUniqueIdPrimaryKey(entity);

        entity.HasIndex(e => e.Nom).UseCollation(BDThequeContext.FrenchCollation);
        entity.HasIndex(e => e.NomRaw);

        entity.Property(e => e.Description).UseCollation(BDThequeContext.FrenchCollation);
        entity.Property(e => e.DescriptionRaw).HasComputedColumnSql($"({entity.GetColumnName(e => e.Description).ToSnakeCase()} COLLATE \"fr-x-icu\")", true);
        entity.Property(e => e.Initiale).HasMaxLength(1).HasComputedColumnSql($"(upper({entity.GetColumnName(e => e.Nom).ToSnakeCase()}))::character(1)", true);
        entity.Property(e => e.Nom).UseCollation(BDThequeContext.FrenchCollation);
        entity.Property(e => e.NomRaw).HasComputedColumnSql($"({entity.GetColumnName(e => e.Nom).ToSnakeCase()} COLLATE \"fr-x-icu\")", true);

        entity.HasOne(d => d.UniversParent).WithMany(p => p.UniversParents).HasForeignKey(d => d.UniversParentId).OnDelete(DeleteBehavior.Restrict);
        entity.HasOne(d => d.UniversRacine).WithMany(p => p.UniversRacines).HasForeignKey(d => d.UniversRacineId).OnDelete(DeleteBehavior.Restrict);
    }
}
