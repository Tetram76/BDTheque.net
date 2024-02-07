namespace BDTheque.GraphQL.Filters;

using HotChocolate.Data.Filters;

public class CharOperationFilterInputType :
    // FilterInputType
    StringOperationFilterInputType
{
    protected override void Configure(IFilterInputTypeDescriptor descriptor)
    {
        // base.Configure(descriptor);

        descriptor.AllowAnd().AllowOr();

        descriptor.Operation(DefaultFilterOperations.Equals).Type<StringType>();
        descriptor.Operation(DefaultFilterOperations.NotEquals).Type<StringType>();
        descriptor.Operation(DefaultFilterOperations.In).Type<StringType>();
        descriptor.Operation(DefaultFilterOperations.NotIn).Type<StringType>();
    }
}
