namespace BDTheque.Model.Scalars;

using HotChocolate.Language;

public abstract class RangedUnsignedShortType(string name, ushort min, ushort max) : UnsignedShortType(name, $"Value between {min} and {max}")
{
    protected override ushort ParseLiteral(IntValueNode valueSyntax)
    {
        ushort value = base.ParseLiteral(valueSyntax);
        if (min > value || value > max)
            CreateParseLiteralError(valueSyntax);
        return value;
    }

    protected override IntValueNode ParseValue(ushort runtimeValue)
    {
        if (min > runtimeValue || runtimeValue > max)
            CreateParseValueError(runtimeValue);
        return base.ParseValue(runtimeValue);
    }
}
