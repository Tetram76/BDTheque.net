namespace BDTheque.Data.Entities;

using System.Diagnostics.CodeAnalysis;
using BDTheque.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.UnlimitedStringLength")]
[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.CircularDependency")]
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
public class Option : EntityWithId
{
    public ushort Category { get; set; }

    public string Libelle { get; set; } = null!;

    public ushort Ordre { get; set; }

    public bool Defaut { get; set; }

    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();
    public virtual ICollection<Serie> Series { get; set; } = new List<Serie>();

    public virtual ICollection<Edition> EditionFormatEditions { get; set; } = new List<Edition>();
    public virtual ICollection<Edition> EditionOrientations { get; set; } = new List<Edition>();
    public virtual ICollection<Edition> EditionReliures { get; set; } = new List<Edition>();
    public virtual ICollection<Edition> EditionSensLectures { get; set; } = new List<Edition>();
    public virtual ICollection<Edition> EditionTypeEditions { get; set; } = new List<Edition>();
    public virtual ICollection<EditionAlbum> EditionsAlbums { get; set; } = new List<EditionAlbum>();

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();
}

public static partial class ModelBuilderExtensions
{
    public static void ApplyEntityConfiguration(this EntityTypeBuilder<Option> entity)
    {
        entity.ToTable("Options");

        SetupVersioning(entity);
        SetupIdPrimaryKey(entity);

        entity.HasIndex(
                e => new
                {
                    e.Category,
                    e.Libelle
                }
            )
            .IsUnique().UseCollation(null!, BDThequeContext.FrenchCollation);

        entity.HasIndex(
            e => new
            {
                e.Category,
                e.Ordre
            }
        ).IsUnique();

        entity.Property(e => e.Libelle).UseCollation(BDThequeContext.FrenchCollation);
    }
}
