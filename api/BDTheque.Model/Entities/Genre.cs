namespace BDTheque.Model.Entities;

using System.Diagnostics.CodeAnalysis;
using BDTheque.Model.Entities.Abstract;

[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.UnlimitedStringLength")]
[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.CircularDependency")]
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
public class Genre : EntityWithLabel
{
    public string Nom { get; set; } = null!;
    public string NomRaw { get; set; } = null!;

    public virtual ICollection<GenreAlbum> GenresAlbums { get; set; } = new List<GenreAlbum>();
    public virtual ICollection<GenreSerie> GenresSeries { get; set; } = new List<GenreSerie>();
}
