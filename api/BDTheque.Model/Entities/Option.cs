namespace BDTheque.Model.Entities;

using System.Diagnostics.CodeAnalysis;

using BDTheque.Model.Entities.Abstract;
using BDTheque.Model.Enums;

[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.UnlimitedStringLength")]
[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.CircularDependency")]
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
[ObjectType]
public class Option : StaticEntity
{
    public OptionCategory Category { get; set; }

    public string Libelle { get; set; } = null!;

    public ushort Ordre { get; set; }

    public bool Defaut { get; set; }

    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();
    public virtual ICollection<Serie> Series { get; set; } = new List<Serie>();

    public virtual ICollection<EditionDetail> EditionFormatEditions { get; set; } = new List<EditionDetail>();
    public virtual ICollection<EditionDetail> EditionOrientations { get; set; } = new List<EditionDetail>();
    public virtual ICollection<EditionDetail> EditionReliures { get; set; } = new List<EditionDetail>();
    public virtual ICollection<EditionDetail> EditionSensLectures { get; set; } = new List<EditionDetail>();
    public virtual ICollection<EditionDetail> EditionTypeEditions { get; set; } = new List<EditionDetail>();
    public virtual ICollection<EditionDetail> EditionsEtats { get; set; } = new List<EditionDetail>();

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();
}
