namespace BDTheque.Model.Scalars;

using HotChocolate.Language;

public abstract class OptionType(string name, string description, IEnumerable<ushort> values)
    : UnsignedShortType(name, description, BindingBehavior.Explicit)
{
    protected static string BuildName(string name, IEnumerable<ushort> values) =>
        $"{name}\nValid values: [{string.Join(", ", values.Select(v => v.ToString()))}]";

    protected override bool IsInstanceOfType(ushort runtimeValue) => values.Contains(runtimeValue);

    protected override bool IsInstanceOfType(IntValueNode valueSyntax) => values.Contains(valueSyntax.ToUInt16());
}

public class OptionEtatType() : OptionType(
    name: "EditionEtat",
    BuildName("Integers that will represent a book condition.", ValidValues),
    ValidValues
)
{
    private static readonly IEnumerable<ushort> ValidValues = [100, 103, 105, 108, 110];
}

public class OptionReliureType() : OptionType(
    name: "EditionReliure",
    BuildName("Integers that will represent a book binding.", ValidValues),
    ValidValues
)
{
    private static readonly IEnumerable<ushort> ValidValues = [200, 201];
}

public class OptionTypeEditionType() : OptionType(
    name: "EditionType",
    BuildName("Integers that will represent a book edition kind.", ValidValues),
    ValidValues
)
{
    private static readonly IEnumerable<ushort> ValidValues = [301, 302, 303];
}

public class OptionOrientationType() : OptionType(
    name: "EditionOrientation",
    BuildName("Integers that will represent a book edition orientation.", ValidValues),
    ValidValues
)
{
    private static readonly IEnumerable<ushort> ValidValues = [401, 402];
}

public class OptionFormatEditionType() : OptionType(
    name: "EditionFormat",
    BuildName("Integers that will represent a book edition format.", ValidValues),
    ValidValues
)
{
    private static readonly IEnumerable<ushort> ValidValues = [501, 503, 504, 505, 506, 510];
}

public class TypeImageType() : OptionType(
    name: "ImageType",
    BuildName("Integers that will represent an image kind.", ValidValues),
    ValidValues
)
{
    private static readonly IEnumerable<ushort> ValidValues = [600, 601, 602, 603, 604];
}

public class OptionSensLectureType() : OptionType(
    name: "EditionSensLecture",
    BuildName("Integers that will represent a book reading direction.", ValidValues),
    ValidValues
)
{
    private static readonly IEnumerable<ushort> ValidValues = [801, 802];
}

public class OptionNotationType() : OptionType(
    name: "Notation",
    BuildName("Integers that will represent a notation.", ValidValues),
    ValidValues
)
{
    private static readonly IEnumerable<ushort> ValidValues = [900, 901, 902, 903, 904, 905];
}
