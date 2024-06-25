namespace BDTheque.Model.Entities;

using System.Diagnostics.CodeAnalysis;

using BDTheque.Model.Entities.Abstract;

[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.UnlimitedStringLength")]
[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.CircularDependency")]
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
[ObjectType]
public class Editeur : MandatoryLabelEntity
{
    public string Nom { get; set; } = null!;
    public string NomRaw { get; set; } = null!;

    [Url]
    public Uri? SiteWeb { get; set; }

    public virtual ICollection<Collection> Collections { get; set; } = new List<Collection>();
    public virtual ICollection<Serie> Series { get; set; } = new List<Serie>();
    public virtual ICollection<EditionAlbum> EditionsAlbums { get; set; } = new List<EditionAlbum>();
}
