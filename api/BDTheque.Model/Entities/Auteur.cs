namespace BDTheque.Model.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

using BDTheque.Model.Entities.Abstract;
using BDTheque.Model.Enums;

using Microsoft.EntityFrameworkCore;

[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.UnlimitedStringLength")]
[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.CircularDependency")]
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
[ObjectType]
[Index(nameof(PersonneId), nameof(Metier), IsUnique = true)]
public class Auteur : UniqueIdEntity
{
    public Guid PersonneId { get; set; }
    public virtual Personne Personne { get; set; } = null!;

    [Column(TypeName = "metier")]
    public Metier Metier { get; set; }

    public virtual ICollection<AuteurAlbum> AuteursAlbums { get; set; } = new List<AuteurAlbum>();
    public virtual ICollection<AuteurSerie> AuteursSeries { get; set; } = new List<AuteurSerie>();
}
