namespace BDTheque.Data.Entities;

using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using BDTheque.Data.Context;
using BDTheque.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.UnlimitedStringLength")]
[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.CircularDependency")]
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
public class Serie : EntityWithOptionalLabel
{
    public string? Titre { get; set; }
    public string? TitreRaw { get; set; }

    public string? Sujet { get; set; }
    public string? SujetRaw { get; set; }
    public string? Notes { get; set; }
    public string? NotesRaw { get; set; }

    [Url]
    public string? SiteWeb { get; set; }

    public Guid? EditeurId { get; set; }
    public virtual Editeur? Editeur { get; set; }
    public Guid? CollectionId { get; set; }
    public virtual Collection? Collection { get; set; }

    public Guid ModeleEditionId { get; set; }
    public virtual Edition ModeleEdition { get; set; } = null!;

    [Range(0, Int32.MaxValue)]
    public ushort? NbAlbums { get; set; }

    public bool? Terminee { get; set; } = false;
    public bool? Complete { get; set; } = false;

    public bool? SuivreSorties { get; set; } = true;
    public bool? SuivreManquants { get; set; } = true;

    public ushort? NotationId { get; set; }
    public virtual Option? Notation { get; set; }

    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();

    public virtual ICollection<AuteurSerie> AuteursSeries { get; set; } = new List<AuteurSerie>();

    public virtual ICollection<GenreSerie> GenresSeries { get; set; } = new List<GenreSerie>();

    public virtual ICollection<UniversSerie> UniversSeries { get; set; } = new List<UniversSerie>();
}

public static partial class ModelBuilderExtensions
{
    public static void ApplyEntityConfiguration(this EntityTypeBuilder<Serie> entity)
    {
        entity.ToTable("Series");

        SetupVersioning(entity);
        SetupUniqueIdPrimaryKey(entity);

        entity.HasIndex(e => e.ModeleEditionId).IsUnique();
        entity.HasIndex(e => e.Titre).UseCollation(BDThequeContext.FrenchCollation);
        entity.HasIndex(e => e.TitreRaw);

        entity.Property(e => e.Titre).UseCollation(BDThequeContext.FrenchCollation);
        entity.Property(e => e.TitreRaw).HasComputedColumnSql($"({entity.GetColumnName(e => e.Titre).ToSnakeCase()} COLLATE \"fr-x-icu\")", true);
        entity.Property(e => e.Initiale).HasMaxLength(1).HasComputedColumnSql($"(upper({entity.GetColumnName(e => e.Titre).ToSnakeCase()}))::character(1)", true);
        entity.Property(e => e.Sujet).UseCollation(BDThequeContext.FrenchCollation);
        entity.Property(e => e.SujetRaw).HasComputedColumnSql($"({entity.GetColumnName(e => e.Sujet).ToSnakeCase()} COLLATE \"fr-x-icu\")", true);
        entity.Property(e => e.Notes).UseCollation(BDThequeContext.FrenchCollation);
        entity.Property(e => e.NotesRaw).HasComputedColumnSql($"({entity.GetColumnName(e => e.Notes).ToSnakeCase()} COLLATE \"fr-x-icu\")", true);
        entity.Property(e => e.Complete).HasDefaultValue(false);
        entity.Property(e => e.SuivreManquants).HasDefaultValue(true);
        entity.Property(e => e.SuivreSorties).HasDefaultValue(true);
        entity.Property(e => e.Terminee).HasDefaultValue(false);

        entity.HasOne(d => d.Editeur).WithMany(p => p.Series).HasForeignKey(d => d.EditeurId).OnDelete(DeleteBehavior.Restrict);
        entity.HasOne(d => d.ModeleEdition).WithOne(p => p.Serie).HasForeignKey<Serie>(d => d.ModeleEditionId).OnDelete(DeleteBehavior.Restrict);
        entity.HasOne(d => d.Notation).WithMany(p => p.Series).HasForeignKey(d => d.NotationId).OnDelete(DeleteBehavior.SetNull);
        entity.HasOne(d => d.Collection).WithMany(p => p.Series)
            .HasPrincipalKey(
                p => new
                {
                    p.EditeurId,
                    p.Id
                }
            )
            .HasForeignKey(
                d => new
                {
                    d.EditeurId,
                    d.CollectionId
                }
            )
            .OnDelete(DeleteBehavior.SetNull);
    }
}
