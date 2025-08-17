namespace BDTheque.Tests.Queries;

using Xunit.Abstractions;

public class CollectionTest(ITestOutputHelper testOutputHelper) : DatabaseTest(testOutputHelper)
{
    [Fact]
    public async Task GetCollectionsTest()
    {
        Editeur[] editeurs =
        [
            new Editeur
            {
                Nom = "E_Aa"
            },
            new Editeur
            {
                Nom = "E_Bb"
            }
        ];
        Collection[] collections =
        [
            new Collection
            {
                Nom = "Aa",
                Editeur = editeurs[0]
            },
            new Collection
            {
                Nom = "Bb",
                Editeur = editeurs[1]
            },
            new Collection
            {
                Nom = "Cc",
                Editeur = editeurs[0]
            }
        ];

        RegisterFinalizer(() => RemoveEntities(editeurs));
        await AddEntities(editeurs);

        RegisterFinalizer(() => RemoveEntities(collections));
        await AddEntities(collections);

        string response = await TestServices.ExecuteRequestAsync(
            b => b.SetQuery(
                """
                query {
                    collectionList(last: 10, where: {initiale: {eq: "A"}}, order: {nom: ASC}) {
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

    [Fact]
    public async Task GetCollectionsByEditeurTest()
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
            }
        ];
        Collection[] collections =
        [
            new Collection
            {
                Nom = "Aa",
                Editeur = editeurs[0]
            },
            new Collection
            {
                Nom = "Bb",
                Editeur = editeurs[1]
            },
            new Collection
            {
                Nom = "Cc",
                Editeur = editeurs[0]
            }
        ];

        RegisterFinalizer(() => RemoveEntities(editeurs));
        await AddEntities(editeurs);

        RegisterFinalizer(() => RemoveEntities(collections));
        await AddEntities(collections);

        string response = await TestServices.ExecuteRequestAsync(
            b => b.SetQuery(
                """
                query {
                    collectionList(last: 10, where: {editeur: {initiale: {eq: "A"}}}, order: {nom: ASC}) {
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
