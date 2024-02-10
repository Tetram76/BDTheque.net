namespace BDTheque.GraphQL.Helpers;

using HotChocolate.Data.Filters;
using HotChocolate.Language;

public static class ErrorHelper
{
    public static IError CreateNonNullError<T>(
        IFilterField field,
        IValueNode value,
        IFilterVisitorContext<T> context,
        bool isMemberInvalid = false)
    {
        IFilterInputType filterType = context.Types.OfType<IFilterInputType>().First();

        INullabilityNode nullability =
            isMemberInvalid && field.Type.IsListType()
                ? new ListNullabilityNode(null, new RequiredModifierNode(null, null))
                : new RequiredModifierNode(null, null);

        return ErrorBuilder.New()
            .SetMessage(
                "The provided value for filter `{0}` of type {1} is invalid. Null values are not supported.",
                context.Operations.Peek().Name,
                filterType.Print()
            )
            .AddLocation(value)
            .SetCode(ErrorCodes.Data.NonNullError)
            .SetExtension("expectedType", field.Type.RewriteNullability(nullability).Print())
            .SetExtension("filterType", filterType.Print())
            .Build();
    }
}
