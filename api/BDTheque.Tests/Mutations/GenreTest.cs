namespace BDTheque.Tests.Mutations;

using Xunit.Abstractions;

public class GenreTest(ITestOutputHelper testOutputHelper) : DatabaseTest(testOutputHelper)
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
                        errors {
                            __typename
                            ... on Error {
                                message
                            }
                        }
                    }
                }
                """
            )
        );

        const string idField = "data.createGenre.genre.id";

        RegisterFinalizer(() => RemoveEntities<Genre>(response, idField));

        response.Should().MatchSnapshot(options => options.IgnoreField(idField));

        Genre genre = Assert.IsType<Genre>(await DbContext.Genres.FindAsync(response.AsGuid(idField)));
        genre.Nom.Should().Be("test");
    }

    [Fact]
    public async Task CreateGenre_ShouldFail()
    {
        var alreadyExists = new Genre
        {
            Nom = "test"
        };
        RegisterFinalizer(() => RemoveEntities(alreadyExists));
        await AddEntities(alreadyExists);

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
                        errors {
                            __typename
                            ... on Error {
                                message
                            }
                        }
                    }
                }
                """
            )
        );

        response.Should().MatchSnapshot();
    }

    [Fact]
    public async Task UpdateGenre_ShouldSucceed()
    {
        var toUpdate = new Genre
        {
            Nom = "zzz"
        };
        RegisterFinalizer(() => RemoveEntities(toUpdate));
        await AddEntities(toUpdate);

        string response = await TestServices.ExecuteRequestAsync(
            b => b.SetQuery(
                $$"""
                  mutation {
                      updateGenre(input: {genre: {id: "{{toUpdate.Id}}", nom: "test"} })
                      {
                          genre {
                              id
                              nom
                          }
                          errors {
                              __typename
                              ... on Error {
                                  message
                              }
                          }
                      }
                  }
                  """
            )
        );

        const string idField = "data.updateGenre.genre.id";

        RegisterFinalizer(() => RemoveEntities<Genre>(response, idField));
        response.Should().MatchSnapshot(options => options.IgnoreField(idField));

        await DbContext.Entry(toUpdate).ReloadAsync();
        toUpdate.Nom.Should().Be("test");
    }

    [Fact]
    public async Task UpdateGenre_ShouldFail_Duplicate()
    {
        var toUpdate = new Genre
        {
            Nom = "zzz"
        };
        var alreadyExists = new Genre
        {
            Nom = "Test"
        };
        RegisterFinalizer(() => RemoveEntities(toUpdate, alreadyExists));
        await AddEntities(toUpdate, alreadyExists);

        string response = await TestServices.ExecuteRequestAsync(
            b => b.SetQuery(
                $$"""
                  mutation {
                      updateGenre(input: {genre: {id: "{{toUpdate.Id}}", nom: "test"} })
                      {
                          genre {
                              id
                              nom
                          }
                          errors {
                              __typename
                              ... on Error {
                                  message
                              }
                          }
                      }
                  }
                  """
            )
        );

        response.Should().MatchSnapshot();
    }

    [Fact]
    public async Task UpdateGenre_ShouldFail_IdNotFound()
    {
        string response = await TestServices.ExecuteRequestAsync(
            b => b.SetQuery(
                $$"""
                  mutation {
                      updateGenre(input: {genre: {id: "{{Guid.Empty}}", nom: "test"} })
                      {
                          genre {
                              id
                              nom
                          }
                          errors {
                              __typename
                              ... on Error {
                                  message
                              }
                          }
                      }
                  }
                  """
            )
        );

        response.Should().MatchSnapshot();
    }

    [Fact]
    public async Task DeleteGenre_ShouldSucceed()
    {
        var toDelete = new Genre
        {
            Nom = "Test"
        };
        RegisterFinalizer(() => ReloadAndRemoveEntities(toDelete));
        await AddEntities(toDelete);

        string response = await TestServices.ExecuteRequestAsync(
            b => b.SetQuery(
                $$"""
                  mutation {
                      deleteGenre(input: {id: "{{toDelete.Id}}"})
                      {
                          genre {
                              id
                              nom
                          }
                          errors {
                              __typename
                              ... on Error {
                                  message
                              }
                          }
                      }
                  }
                  """
            )
        );

        const string idField = "data.deleteGenre.genre.id";
        response.Should().MatchSnapshot(options => options.IgnoreField(idField));
    }

    [Fact]
    public async Task DeleteGenre_ShouldFail()
    {
        string response = await TestServices.ExecuteRequestAsync(
            b => b.SetQuery(
                $$"""
                  mutation {
                      deleteGenre(input: {id: "{{Guid.NewGuid()}}"})
                      {
                          genre {
                              id
                              nom
                          }
                          errors {
                              __typename
                              ... on Error {
                                  message
                              }
                          }
                      }
                  }
                  """
            )
        );

        response.Should().MatchSnapshot(options => options.IgnoreField("data.deleteGenre.errors[0].message"));
    }
}
