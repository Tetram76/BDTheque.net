namespace BDTheque.Tests.Queries;

using Xunit.Abstractions;

public class EditeurTest(ITestOutputHelper testOutputHelper) : DatabaseTest(testOutputHelper)
{
    [Fact]
    public async Task GetEditeursTest()
    {
        Editeur[] editeurs =
        [
            new Editeur
            {
                Nom = "Aa"
            },
            new Editeur
            {
                Nom = "Bb"
            },
            new Editeur
            {
                Nom = "Cc"
            }
        ];

        RegisterFinalizer(() => RemoveEntities(editeurs));
        await AddEntities(editeurs);

        string response = await TestServices.ExecuteRequestAsync(
            b => b.SetQuery(
                """
                query {
                    editeurList(last: 10, where: {initiale: {eq: "A"}}, order: {nom: ASC}) {
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

        response.Should().MatchSnapshot(options => options.IgnoreAllFields("id"));
    }
}
