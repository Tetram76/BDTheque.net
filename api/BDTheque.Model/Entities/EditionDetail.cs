namespace BDTheque.Model.Entities;

using System.Diagnostics.CodeAnalysis;

using BDTheque.Model.Entities.Abstract;
using BDTheque.Model.Scalars;

[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.UnlimitedStringLength")]
[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.CircularDependency")]
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
[ObjectType]
public class EditionDetail : UniqueIdEntity
{
    [NonNegative<decimal>]
    public decimal? Prix { get; set; }

    public bool? Couleur { get; set; } = true;
    public bool? Vo { get; set; } = false;

    public ushort? EtatId { get; set; }

    [MutationScalarType<OptionEtatType>]
    public virtual Option? Etat { get; set; }

    public ushort? ReliureId { get; set; }

    [MutationScalarType<OptionReliureType>]
    public virtual Option? Reliure { get; set; }

    public ushort? FormatEditionId { get; set; }

    [MutationScalarType<OptionFormatEditionType>]
    public virtual Option? FormatEdition { get; set; }

    public ushort? TypeEditionId { get; set; }

    [MutationScalarType<OptionTypeEditionType>]
    public virtual Option? TypeEdition { get; set; }

    public ushort? OrientationId { get; set; }

    [MutationScalarType<OptionOrientationType>]
    public virtual Option? Orientation { get; set; }

    public ushort? SensLectureId { get; set; }

    [MutationScalarType<OptionSensLectureType>]
    public virtual Option? SensLecture { get; set; }

    public virtual ICollection<Edition> Editions { get; set; } = new List<Edition>();

    public virtual Serie? Serie { get; set; }
}
