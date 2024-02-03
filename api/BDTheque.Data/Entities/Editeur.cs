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
public class Editeur : EntityWithLabel
{
    public string Nom { get; set; } = null!;
    public string NomRaw { get; set; } = null!;

    [Url]
    public string? SiteWeb { get; set; }

    public virtual ICollection<Collection> Collections { get; set; } = new List<Collection>();

    public virtual ICollection<Serie> Series { get; set; } = new List<Serie>();

    public virtual ICollection<EditionAlbum> EditionsAlbums { get; set; } = new List<EditionAlbum>();
}

public static partial class ModelBuilderExtensions
{
    public static void ApplyEntityConfiguration(this EntityTypeBuilder<Editeur> entity)
    {
        entity.ToTable("Editeurs");

        SetupVersioning(entity);
        SetupUniqueIdPrimaryKey(entity);

        entity.HasIndex(e => e.Nom).UseCollation(BDThequeContext.FrenchCollation).IsUnique();
        entity.HasIndex(e => e.NomRaw);

        entity.Property(e => e.Initiale).HasMaxLength(1).HasComputedColumnSql($"(upper({entity.GetColumnName(e => e.Nom).ToSnakeCase()}))::character(1)", true);
        entity.Property(e => e.Nom).UseCollation(BDThequeContext.FrenchCollation);
        entity.Property(e => e.NomRaw).HasComputedColumnSql($"({entity.GetColumnName(e => e.Nom).ToSnakeCase()} COLLATE \"fr-x-icu\")", true);
    }
}
