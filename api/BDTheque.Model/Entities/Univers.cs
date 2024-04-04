namespace BDTheque.Model.Entities;

using System.Diagnostics.CodeAnalysis;

using BDTheque.Model.Entities.Abstract;

using Microsoft.EntityFrameworkCore;

[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.UnlimitedStringLength")]
[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.CircularDependency")]
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
[ObjectType]
[Index(nameof(Nom), IsUnique = true)]
public class Univers : MandatoryLabelEntity
{
    [RequiredName]
    public string Nom { get; set; } = null!;

    public string NomRaw { get; set; } = null!;

    public string? Description { get; set; }
    public string? DescriptionRaw { get; set; }

    [Url]
    public Uri? SiteWeb { get; set; }

    public Guid UniversRacineId { get; set; }

    [GraphQLName("racine")]
    [GraphQLReadOnly]
    public virtual Univers UniversRacine { get; set; } = null!;

    public Guid? UniversParentId { get; set; }

    [GraphQLName("parent")]
    public virtual Univers? UniversParent { get; set; }

    public virtual ICollection<Univers> UniversParents { get; set; } = new List<Univers>();
    public virtual ICollection<Univers> UniversRacines { get; set; } = new List<Univers>();

    public virtual ICollection<UniversAlbum> UniversAlbums { get; set; } = new List<UniversAlbum>();
    public virtual ICollection<UniversSerie> UniversSeries { get; set; } = new List<UniversSerie>();
}
