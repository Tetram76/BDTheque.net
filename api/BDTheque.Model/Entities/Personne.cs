namespace BDTheque.Model.Entities;

using System.Diagnostics.CodeAnalysis;

using BDTheque.Model.Entities.Abstract;

using Microsoft.EntityFrameworkCore;

[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.UnlimitedStringLength")]
[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.CircularDependency")]
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
[ObjectType]
[Index(nameof(Nom), IsUnique = true)]
public class Personne : MandatoryLabelEntity
{
    [RequiredName]
    public string Nom { get; set; } = null!;

    public string NomRaw { get; set; } = null!;

    public string? Biographie { get; set; }
    public string? BiographieRaw { get; set; }

    [Url]
    public Uri? SiteWeb { get; set; }

    public virtual ICollection<Auteur> Auteurs { get; set; } = new List<Auteur>();
}
