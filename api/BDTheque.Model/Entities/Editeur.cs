namespace BDTheque.Model.Entities;

using System.Diagnostics.CodeAnalysis;

using BDTheque.Model.Entities.Abstract;

using Microsoft.EntityFrameworkCore;

[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.UnlimitedStringLength")]
[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.CircularDependency")]
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
[ObjectType]
[Index(nameof(Nom), IsUnique = true)]
public class Editeur : MandatoryLabelEntity
{
    [RequiredName]
    public string Nom { get; set; } = null!;

    public string NomRaw { get; set; } = null!;

    [Url]
    public Uri? SiteWeb { get; set; }

    public virtual ICollection<Collection> Collections { get; set; } = new List<Collection>();
    public virtual ICollection<Serie> Series { get; set; } = new List<Serie>();
    public virtual ICollection<Edition> Editions { get; set; } = new List<Edition>();
}
