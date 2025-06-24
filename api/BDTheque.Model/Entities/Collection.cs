namespace BDTheque.Model.Entities;

using System.Diagnostics.CodeAnalysis;

using BDTheque.Model.Entities.Abstract;

using Microsoft.EntityFrameworkCore;

[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.UnlimitedStringLength")]
[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.CircularDependency")]
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
[ObjectType]
[Index(nameof(EditeurId), nameof(Nom), IsUnique = true)]
public class Collection : MandatoryLabelEntity
{
    [RequiredName]
    public string Nom { get; set; } = null!;

    public string NomRaw { get; set; } = null!;

    public Guid EditeurId { get; set; }
    public virtual Editeur Editeur { get; set; } = null!;

    public virtual ICollection<Edition> Editions { get; set; } = new List<Edition>();
    public virtual ICollection<Serie> Series { get; set; } = new List<Serie>();
}
