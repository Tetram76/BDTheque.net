namespace BDTheque.GraphQL.Queries;

using BDTheque.Data.Context;

[SuppressMessage("ReSharper", "UnusedMember.Global")]

// [QueryType]
public static class EditeurQueries
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLType<ListType<EditeurType>>]
    public static IQueryable<Editeur> GetEditeurs(BDThequeContext dbContext)
        => dbContext.Editeurs;
}
