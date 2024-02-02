namespace BDTheque.GraphQL.Queries;

using BDTheque.Data.Context;
using BDTheque.Data.Entities;
using BDTheque.GraphQL.Types;

[QueryType]
public static class EditeurQueries
{
    [UsePaging]
    [UseProjection]
    [UseFiltering<EditeurType>]
    [UseSorting<EditeurType>]
    [GraphQLType<ListType<EditeurType>>]
    public static IQueryable<Editeur> GetEditeurs(BDThequeContext dbContext, CancellationToken cancellationToken)
        => dbContext.Editeurs;
}
