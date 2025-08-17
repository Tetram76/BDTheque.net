namespace BDTheque.Model.Entities;

using System.Diagnostics.CodeAnalysis;

using BDTheque.Model.Entities.Abstract;

using Microsoft.EntityFrameworkCore;

[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.UnlimitedStringLength")]
[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.CircularDependency")]
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
[ObjectType]
[InputObjectType]
[Index(nameof(Nom), IsUnique = true)]
public class Genre : MandatoryLabelEntity
{
    [RequiredName]
    public string Nom { get; set; } = null!;

    public string NomRaw { get; set; } = null!;

    public virtual ICollection<GenreAlbum> GenresAlbums { get; set; } = new List<GenreAlbum>();
    public virtual ICollection<GenreSerie> GenresSeries { get; set; } = new List<GenreSerie>();
}
