namespace BDTheque.Tests.Queries;

using Xunit.Abstractions;

public class GenreTest(ITestOutputHelper testOutputHelper) : DatabaseTest(testOutputHelper)
{
    [Fact]
    public async Task GetGenresTest()
    {
        string response = await TestServices.ExecuteRequestAsync(
            b => b.SetQuery(
                """
                query {
                    genreList(last: 10, where: {initiale: {eq: "A"}}, order: {nom: ASC}) {
                      pageInfo {
                        hasNextPage
                        hasPreviousPage
                      }
                      edges {
                        cursor
                        node {
                          id
                          nom
                        }
                      }
                    }
                }
                """
            )
        );

        response.Should().MatchSnapshot();
    }
}
