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
public class Edition : EntityWithUniqueId
{
    [Range(0, 2999)]
    public ushort? AnneeEdition { get; set; }

    public string? Notes { get; set; }
    public string? NotesRaw { get; set; }

    public string? Isbn { get; set; }

    public ushort? NombreDePages { get; set; }

    public bool? Couleur { get; set; } = true;
    public bool? Vo { get; set; } = false;

    public ushort? ReliureId { get; set; }
    public virtual Option? Reliure { get; set; }

    public ushort? FormatEditionId { get; set; }
    public virtual Option? FormatEdition { get; set; }

    public ushort? TypeEditionId { get; set; }
    public virtual Option? TypeEdition { get; set; }

    public ushort? OrientationId { get; set; }
    public virtual Option? Orientation { get; set; }

    public ushort? SensLectureId { get; set; }
    public virtual Option? SensLecture { get; set; }

    public virtual ICollection<EditionAlbum> EditionsAlbums { get; set; } = new List<EditionAlbum>();

    public virtual Serie? Serie { get; set; }
}

public static partial class ModelBuilderExtensions
{
    public static void ApplyEntityConfiguration(this EntityTypeBuilder<Edition> entity)
    {
        entity.ToTable("Editions");

        SetupVersioning(entity);
        SetupUniqueIdPrimaryKey(entity);

        entity.Property(e => e.Notes).UseCollation(BDThequeContext.FrenchCollation);
        entity.Property(e => e.NotesRaw).HasComputedColumnSql($"({entity.GetColumnName(e => e.Notes).ToSnakeCase()} COLLATE \"fr-x-icu\")", true);
        entity.Property(e => e.Couleur).HasDefaultValue(true);
        entity.Property(e => e.Vo).HasDefaultValue(false);

        entity.HasOne(d => d.FormatEdition).WithMany(p => p.EditionFormatEditions).HasForeignKey(d => d.FormatEditionId).OnDelete(DeleteBehavior.Restrict);
        entity.HasOne(d => d.Orientation).WithMany(p => p.EditionOrientations).HasForeignKey(d => d.OrientationId).OnDelete(DeleteBehavior.Restrict);
        entity.HasOne(d => d.Reliure).WithMany(p => p.EditionReliures).HasForeignKey(d => d.ReliureId).OnDelete(DeleteBehavior.Restrict);
        entity.HasOne(d => d.SensLecture).WithMany(p => p.EditionSensLectures).HasForeignKey(d => d.SensLectureId).OnDelete(DeleteBehavior.Restrict);
        entity.HasOne(d => d.TypeEdition).WithMany(p => p.EditionTypeEditions).HasForeignKey(d => d.TypeEditionId).OnDelete(DeleteBehavior.Restrict);
    }
}
