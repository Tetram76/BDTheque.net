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
public class Album : EntityWithOptionalLabel
{
    public string? Titre { get; set; }
    public string? TitreRaw { get; set; }

    public string? Sujet { get; set; }
    public string? SujetRaw { get; set; }
    public string? Notes { get; set; }
    public string? NotesRaw { get; set; }

    public bool? HorsSerie { get; set; } = false;

    [Range(0, Int32.MaxValue)]
    public ushort? Tome { get; set; }

    public bool? Integrale { get; set; } = false;

    [Range(0, Int32.MaxValue)]
    public ushort? TomeDebut { get; set; }

    [Range(0, Int32.MaxValue)]
    public ushort? TomeFin { get; set; }

    [Range(1, 12)]
    public ushort? MoisParution { get; set; }

    [Range(1900, 2999)]
    public ushort? AnneeParution { get; set; }

    public Guid? SerieId { get; set; }
    public virtual Serie? Serie { get; set; }

    public ushort? NotationId { get; set; }
    public virtual Option? Notation { get; set; }

    public virtual ICollection<AuteurAlbum> AuteursAlbums { get; set; } = new List<AuteurAlbum>();
    public virtual ICollection<EditionAlbum> EditionsAlbums { get; set; } = new List<EditionAlbum>();
    public virtual ICollection<GenreAlbum> GenresAlbums { get; set; } = new List<GenreAlbum>();

    public virtual ICollection<UniversAlbum> UniversAlbums { get; set; } = new List<UniversAlbum>();
}

public static partial class ModelBuilderExtensions
{
    public static void ApplyEntityConfiguration(this EntityTypeBuilder<Album> entity)
    {
        entity.ToTable("Albums");

        SetupVersioning(entity);
        SetupUniqueIdPrimaryKey(entity);

        entity.HasIndex(e => e.Titre).UseCollation(BDThequeContext.FrenchCollation);
        entity.HasIndex(e => e.TitreRaw);

        entity.Property(e => e.Initiale).HasComputedColumnSql($"(upper({entity.GetColumnName(e => e.Titre).ToSnakeCase()}))::character(1)", true);
        entity.Property(e => e.Notes).UseCollation(BDThequeContext.FrenchCollation);
        entity.Property(e => e.NotesRaw).HasComputedColumnSql($"({entity.GetColumnName(e => e.Notes).ToSnakeCase()} COLLATE \"fr-x-icu\")", true);
        entity.Property(e => e.Sujet).UseCollation(BDThequeContext.FrenchCollation);
        entity.Property(e => e.SujetRaw).HasComputedColumnSql($"({entity.GetColumnName(e => e.Sujet).ToSnakeCase()} COLLATE \"fr-x-icu\")", true);
        entity.Property(e => e.Titre).UseCollation(BDThequeContext.FrenchCollation);
        entity.Property(e => e.TitreRaw).HasComputedColumnSql($"({entity.GetColumnName(e => e.Titre).ToSnakeCase()} COLLATE \"fr-x-icu\")", true);
        entity.Property(e => e.HorsSerie).HasDefaultValue(false);
        entity.Property(e => e.Integrale).HasDefaultValue(false);

        entity.HasOne(d => d.Notation).WithMany(p => p.Albums).HasForeignKey(d => d.NotationId).OnDelete(DeleteBehavior.SetNull);
        entity.HasOne(d => d.Serie).WithMany(p => p.Albums).HasForeignKey(d => d.SerieId).OnDelete(DeleteBehavior.SetNull);
    }
}
