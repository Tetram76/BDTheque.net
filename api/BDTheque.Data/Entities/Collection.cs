namespace BDTheque.Data.Entities;

using System.Diagnostics.CodeAnalysis;
using BDTheque.Data.Context;
using BDTheque.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.UnlimitedStringLength")]
[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.CircularDependency")]
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
public class Collection : EntityWithLabel
{
    public string Nom { get; set; } = null!;
    public string NomRaw { get; set; } = null!;

    public Guid EditeurId { get; set; }
    public virtual Editeur Editeur { get; set; } = null!;

    public virtual ICollection<EditionAlbum> EditionsAlbums { get; set; } = new List<EditionAlbum>();

    public virtual ICollection<Serie> Series { get; set; } = new List<Serie>();
}

public static partial class ModelBuilderExtensions
{
    public static void ApplyEntityConfiguration(this EntityTypeBuilder<Collection> entity)
    {
        entity.ToTable("Collections");

        SetupVersioning(entity);
        SetupUniqueIdPrimaryKey(entity);

        entity.HasIndex(
            e => new
            {
                e.EditeurId,
                e.Id
            }
        ).IsUnique();

        entity.HasIndex(
                e => new
                {
                    e.EditeurId,
                    e.Nom
                }
            )
            .IsUnique()
            .UseCollation(null!, BDThequeContext.FrenchCollation);

        entity.HasIndex(e => e.Nom).UseCollation(BDThequeContext.FrenchCollation);
        entity.HasIndex(e => e.NomRaw);

        entity.Property(e => e.Initiale).HasMaxLength(1).HasComputedColumnSql($"(upper({entity.GetColumnName(e => e.Nom).ToSnakeCase()}))::character(1)", true);
        entity.Property(e => e.Nom).UseCollation(BDThequeContext.FrenchCollation);
        entity.Property(e => e.NomRaw).HasComputedColumnSql($"({entity.GetColumnName(e => e.Nom).ToSnakeCase()} COLLATE \"fr-x-icu\")", true);

        entity.HasOne(d => d.Editeur).WithMany(p => p.Collections).HasForeignKey(d => d.EditeurId);
    }
}
