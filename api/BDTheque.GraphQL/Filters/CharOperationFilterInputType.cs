namespace BDTheque.GraphQL.Filters;

using BDTheque.Model.Scalars;
using HotChocolate.Data.Filters;

public class CharOperationFilterInputType : FilterInputType
{
    protected override void Configure(IFilterInputTypeDescriptor descriptor)
    {
        descriptor.Operation(DefaultFilterOperations.Equals).Type<CharType>();
        descriptor.Operation(DefaultFilterOperations.NotEquals).Type<CharType>();
        descriptor.Operation(DefaultFilterOperations.In).Type<ListType<CharType>>();
        descriptor.Operation(DefaultFilterOperations.NotIn).Type<ListType<CharType>>();
    }
}
