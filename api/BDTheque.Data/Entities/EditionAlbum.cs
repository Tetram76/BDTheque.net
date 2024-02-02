namespace BDTheque.Data.Entities;

using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.UnlimitedStringLength")]
[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.CircularDependency")]
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
public class EditionAlbum : EntityWithUniqueId
{
    public Guid EditionId { get; set; }
    public virtual Edition Edition { get; set; } = null!;

    public Guid AlbumId { get; set; }
    public virtual Album Album { get; set; } = null!;

    public Guid EditeurId { get; set; }
    public virtual Editeur Editeur { get; set; } = null!;

    public Guid? CollectionId { get; set; }
    public virtual Collection? Collection { get; set; }

    public ushort? EtatId { get; set; }
    public virtual Option? Etat { get; set; }

    public bool Stock { get; set; } = true;
    public bool? Offert { get; set; } = false;
    public bool? Occasion { get; set; } = false;
    public bool? Gratuit { get; set; } = false;

    public DateOnly? DateAchat { get; set; }

    [Range(0, double.MaxValue)]
    public decimal? Prix { get; set; }

    public bool? Dedicace { get; set; } = false;

    public string? NumeroPerso { get; set; }

    public string? Notes { get; set; }

    public virtual ICollection<CoteAlbum> CotesAlbums { get; set; } = new List<CoteAlbum>();

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();
}

public static partial class ModelBuilderExtensions
{
    public static void ApplyEntityConfiguration(this EntityTypeBuilder<EditionAlbum> entity)
    {
        entity.ToTable("EditionsAlbums");

        SetupVersioning(entity);
        SetupUniqueIdPrimaryKey(entity);

        entity.HasIndex(
            e => new
            {
                e.AlbumId,
                e.Id
            }
        ).IsUnique();

        entity.Property(e => e.Dedicace).HasDefaultValue(false);
        entity.Property(e => e.Gratuit).HasDefaultValue(false);
        entity.Property(e => e.Occasion).HasDefaultValue(false);
        entity.Property(e => e.Offert).HasDefaultValue(false);
        entity.Property(e => e.Prix).HasPrecision(8, 3);
        entity.Property(e => e.Stock).HasDefaultValue(true);

        entity.HasOne(d => d.Album).WithMany(p => p.EditionsAlbums).HasForeignKey(d => d.AlbumId);
        entity.HasOne(d => d.Editeur).WithMany(p => p.EditionsAlbums).HasForeignKey(d => d.EditeurId).OnDelete(DeleteBehavior.Restrict);
        entity.HasOne(d => d.Edition).WithMany(p => p.EditionsAlbums).HasForeignKey(d => d.EditionId);
        entity.HasOne(d => d.Etat).WithMany(p => p.EditionsAlbums).HasForeignKey(d => d.EtatId).OnDelete(DeleteBehavior.SetNull);
        entity.HasOne(d => d.Collection).WithMany(p => p.EditionsAlbums)
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
            );
    }
}
