namespace BDTheque.Model.Scalars;

using HotChocolate.Language;

public class CharType : ScalarType<char, StringValueNode>
{
    public CharType() : this("Char", string.Empty, BindingBehavior.Implicit)
    {
    }

    public CharType(string name, string? description = null, BindingBehavior bind = BindingBehavior.Explicit) : base(name, bind)
    {
        Description = description;
    }

    protected override bool IsInstanceOfType(StringValueNode valueSyntax) =>
        valueSyntax.Value != string.Empty && valueSyntax.Value.Length <= 1;

    protected override char ParseLiteral(StringValueNode valueSyntax) =>
        string.IsNullOrEmpty(valueSyntax.Value) ? default : valueSyntax.Value[0];

    protected override StringValueNode ParseValue(char runtimeValue) =>
        new(runtimeValue.ToString());

    public override IValueNode ParseResult(object? resultValue) =>
        ParseValue(resultValue);
}
