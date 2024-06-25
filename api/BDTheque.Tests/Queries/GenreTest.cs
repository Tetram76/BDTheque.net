namespace BDTheque.Tests.Queries;

public class GenreTest
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
