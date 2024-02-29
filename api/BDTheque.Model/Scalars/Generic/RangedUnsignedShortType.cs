namespace BDTheque.Model.Scalars;

using HotChocolate.Language;

public abstract class RangedUnsignedShortType(string name, ushort min, ushort max) : UnsignedShortType(name, $"Value between {min} and {max}")
{
    protected override bool IsInstanceOfType(ushort runtimeValue)
    {
        return runtimeValue >= min && runtimeValue <= max;
    }

    protected override bool IsInstanceOfType(IntValueNode valueSyntax)
    {
        return IsInstanceOfType(ParseLiteral(valueSyntax));
    }
}
