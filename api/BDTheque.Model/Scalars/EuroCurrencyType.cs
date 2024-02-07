namespace BDTheque.Model.Scalars;

using HotChocolate.Language;

public class EuroCurrencyType() : LocalCurrencyType("EuroCurrency", "fr")
{
    protected override decimal ParseLiteral(StringValueNode valueSyntax)
    {
        decimal value = base.ParseLiteral(valueSyntax);
        if (0 > value)
            CreateParseLiteralError(valueSyntax);
        return value;
    }

    protected override StringValueNode ParseValue(decimal runtimeValue)
    {
        if (0 > runtimeValue)
            CreateParseValueError(runtimeValue);
        return base.ParseValue(runtimeValue);
    }
}
