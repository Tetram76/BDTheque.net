namespace BDTheque.Tests.Schema;

using BDTheque.Tests.Helpers;
using Snapshooter;

public class SchemaTest
{
    [Fact]
    public async Task SchemaChangeTest()
    {
        ISchema schema = await TestServices.Executor.GetSchemaAsync(default);
        schema.ToString().MatchSnapshot();
    }

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
