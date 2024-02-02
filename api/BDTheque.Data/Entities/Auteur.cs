namespace BDTheque.Data.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.UnlimitedStringLength")]
[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.CircularDependency")]
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
public class Auteur : EntityWithUniqueId
{
    public Guid PersonneId { get; set; }
    public virtual Personne Personne { get; set; } = null!;

    [Column(TypeName = "metier")]
    public Metier Metier { get; set; }

    public virtual ICollection<AuteurAlbum> AuteursAlbums { get; set; } = new List<AuteurAlbum>();
    public virtual ICollection<AuteurSerie> AuteursSeries { get; set; } = new List<AuteurSerie>();
}

public static partial class ModelBuilderExtensions
{
    public static void ApplyEntityConfiguration(this EntityTypeBuilder<Auteur> entity)
    {
        entity.ToTable("Auteurs");

        SetupVersioning(entity);
        SetupUniqueIdPrimaryKey(entity);

        entity.HasOne(d => d.Personne).WithMany(p => p.Auteurs).HasForeignKey(d => d.PersonneId).OnDelete(DeleteBehavior.Restrict);
    }
}
