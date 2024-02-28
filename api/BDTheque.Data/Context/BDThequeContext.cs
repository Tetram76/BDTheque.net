namespace BDTheque.Data.Context;

using BDTheque.Data.Entities;
using BDTheque.Data.Enums;
using BDTheque.Data.Seeders;
using BDTheque.Extensions;
using BDTheque.Model.Entities;
using BDTheque.Model.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Hosting;

public class BDThequeContext(DbContextOptions<BDThequeContext> options) : DbContext(options)
{
    public const string FrenchCollation = "french_ci_ai";
    public const string PredictiveFrenchCollation = "fr-x-icu";

    public virtual DbSet<Album> Albums { get; init; } = null!;
    public virtual DbSet<Auteur> Auteurs { get; init; } = null!;
    public virtual DbSet<AuteurAlbum> AuteursAlbums { get; init; } = null!;
    public virtual DbSet<AuteurSerie> AuteursSeries { get; init; } = null!;
    public virtual DbSet<Collection> Collections { get; init; } = null!;
    public virtual DbSet<Cote> CotesAlbums { get; init; } = null!;
    public virtual DbSet<Editeur> Editeurs { get; init; } = null!;
    public virtual DbSet<Edition> Editions { get; init; } = null!;
    public virtual DbSet<EditionAlbum> EditionsAlbums { get; init; } = null!;
    public virtual DbSet<Genre> Genres { get; init; } = null!;
    public virtual DbSet<GenreAlbum> GenresAlbums { get; init; } = null!;
    public virtual DbSet<GenreSerie> GenresSeries { get; init; } = null!;
    public virtual DbSet<Image> Images { get; init; } = null!;
    public virtual DbSet<Option> Options { get; init; } = null!;
    public virtual DbSet<Personne> Personnes { get; init; } = null!;
    public virtual DbSet<Serie> Series { get; init; } = null!;
    public virtual DbSet<Univers> Univers { get; init; } = null!;
    public virtual DbSet<UniversAlbum> UniversAlbums { get; init; } = null!;
    public virtual DbSet<UniversSerie> UniversSeries { get; init; } = null!;

    protected override async void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .RegisterEnums()
            .HasPostgresExtension("pgcrypto")
            .HasAnnotation(
                $"Npgsql:CollationDefinition:public.{FrenchCollation}", "fr_fr-u-ks-level1,fr_fr-u-ks-level1,icu,False"
            );

        modelBuilder.Entity<Album>().ApplyEntityConfiguration();
        modelBuilder.Entity<Cote>().ApplyEntityConfiguration();

        modelBuilder.Entity<Serie>().ApplyEntityConfiguration();

        modelBuilder.Entity<Personne>().ApplyEntityConfiguration();
        modelBuilder.Entity<Auteur>().ApplyEntityConfiguration();
        modelBuilder.Entity<AuteurAlbum>().ApplyEntityConfiguration();
        modelBuilder.Entity<AuteurSerie>().ApplyEntityConfiguration();

        modelBuilder.Entity<Editeur>().ApplyEntityConfiguration();
        modelBuilder.Entity<Collection>().ApplyEntityConfiguration();

        modelBuilder.Entity<Edition>().ApplyEntityConfiguration();
        modelBuilder.Entity<EditionAlbum>().ApplyEntityConfiguration();

        modelBuilder.Entity<Genre>().ApplyEntityConfiguration();
        modelBuilder.Entity<GenreAlbum>().ApplyEntityConfiguration();
        modelBuilder.Entity<GenreSerie>().ApplyEntityConfiguration();

        modelBuilder.Entity<Univers>().ApplyEntityConfiguration();
        modelBuilder.Entity<UniversAlbum>().ApplyEntityConfiguration();
        modelBuilder.Entity<UniversSerie>().ApplyEntityConfiguration();

        modelBuilder.Entity<Image>().ApplyEntityConfiguration();

        modelBuilder.Entity<Option>().ApplyEntityConfiguration();

        foreach (IMutableEntityType? entityType in modelBuilder.Model.GetEntityTypes())
        {
            entityType.SetTableName(entityType.GetTableName()?.ToSnakeCase());
            foreach (IMutableProperty? property in entityType.GetProperties())
                property.SetColumnName(property.GetColumnName().ToSnakeCase());
        }

        await modelBuilder.SeedFromResource<Option>();
    }

    public override int SaveChanges()
    {
        UpdateTimestamps();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateTimestamps()
    {
        IEnumerable<EntityEntry> entities = ChangeTracker.Entries().Where(
            x => x is { Entity: VersioningEntity, State: EntityState.Added or EntityState.Modified }
        );

        DateTime now = DateTime.UtcNow;
        foreach (EntityEntry entity in entities)
        {
            var versioningEntity = (VersioningEntity) entity.Entity;
            if (entity.State == EntityState.Added)
                versioningEntity.CreatedAt = now;

            versioningEntity.UpdatedAt = now;
        }
    }
}
