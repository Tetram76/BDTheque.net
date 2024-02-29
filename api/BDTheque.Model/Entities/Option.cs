namespace BDTheque.Model.Entities;

using System.Diagnostics.CodeAnalysis;
using BDTheque.Model.Entities.Abstract;
using BDTheque.Model.Enums;

[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.UnlimitedStringLength")]
[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.CircularDependency")]
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
[ObjectType]
public class Option : VersioningEntity
{
    [ID]
    public ushort Id { get; set; }

    public OptionCategory Category { get; set; }

    public string Libelle { get; set; } = null!;

    public ushort Ordre { get; set; }

    public bool Defaut { get; set; }

    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();
    public virtual ICollection<Serie> Series { get; set; } = new List<Serie>();

    public virtual ICollection<Edition> EditionFormatEditions { get; set; } = new List<Edition>();
    public virtual ICollection<Edition> EditionOrientations { get; set; } = new List<Edition>();
    public virtual ICollection<Edition> EditionReliures { get; set; } = new List<Edition>();
    public virtual ICollection<Edition> EditionSensLectures { get; set; } = new List<Edition>();
    public virtual ICollection<Edition> EditionTypeEditions { get; set; } = new List<Edition>();
    public virtual ICollection<Edition> EditionsEtats { get; set; } = new List<Edition>();

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();
}
