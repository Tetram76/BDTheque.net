namespace BDTheque.Tests.Schema.Queries;

using BDTheque.Tests.Helpers;

public class GenreTest
{
    [Fact]
    public async Task GetGenresTest()
    {
        string response = await TestServices.ExecuteRequestAsync(
            b => b.SetQuery(
                """
                query {
                    genres(last: 10, where: {initiale: {eq: "A"}}) {
                        pageInfo {
                            hasNextPage
                            hasPreviousPage
                        }
                        nodes {
                            id
                            nom
                        }
                    }
                }
                """
            )
        );

        response.MatchSnapshot();
    }
}
