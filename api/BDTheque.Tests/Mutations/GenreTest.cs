namespace BDTheque.Tests.Mutations;

using BDTheque.Model.Entities;

public class GenreTest : BaseTest
{
    [Fact]
    public async Task CreateGenre_ShouldSucceed()
    {
        string response = await TestServices.ExecuteRequestAsync(
            b => b.SetQuery(
                """
                mutation {
                    createGenre(input: {genre: {nom: "test"}})
                    {
                        genre {
                            id
                            nom
                        }
                    }
                }
                """
            )
        );

        RegisterFinalizer(async () => await TestServices.CleanDbAfterTest<Genre>(response, "data.createGenre.genre.id"));

        response.Should().MatchSnapshot(options => options.IgnoreField("data.createGenre.genre.id"));
    }
}
