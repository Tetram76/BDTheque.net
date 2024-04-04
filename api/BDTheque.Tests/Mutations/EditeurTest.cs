namespace BDTheque.Tests.Mutations;

using Xunit.Abstractions;

public class EditeurTest(ITestOutputHelper testOutputHelper) : DatabaseTest(testOutputHelper)
{
    [Fact]
    public async Task CreateEditeur_ShouldSucceed()
    {
        string response = await TestServices.ExecuteRequestAsync(
            b => b.SetQuery(
                """
                mutation {
                    createEditeur(input: {editeur: {nom: "test"}})
                    {
                        editeur {
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

        const string idField = "data.createEditeur.editeur.id";

        RegisterFinalizer(() => RemoveEntities<Editeur>(response, idField));

        response.Should().MatchSnapshot(options => options.IgnoreField(idField));

        Editeur editeur = Assert.IsType<Editeur>(await DbContext.Editeurs.FindAsync(response.AsGuid(idField)));
        editeur.Nom.Should().Be("test");
    }

    [Fact]
    public async Task CreateEditeur_ShouldFail()
    {
        var alreadyExists = new Editeur
        {
            Nom = "test"
        };
        RegisterFinalizer(() => RemoveEntities(alreadyExists));
        await AddEntities(alreadyExists);

        string response = await TestServices.ExecuteRequestAsync(
            b => b.SetQuery(
                """
                mutation {
                    createEditeur(input: {editeur: {nom: "test"}})
                    {
                        editeur {
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
    public async Task UpdateEditeur_ShouldSucceed()
    {
        var toUpdate = new Editeur
        {
            Nom = "zzz"
        };
        RegisterFinalizer(() => RemoveEntities(toUpdate));
        await AddEntities(toUpdate);

        string response = await TestServices.ExecuteRequestAsync(
            b => b.SetQuery(
                $$"""
                  mutation {
                      updateEditeur(input: {editeur: {id: "{{toUpdate.Id}}", nom: "test"} })
                      {
                          editeur {
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

        const string idField = "data.updateEditeur.editeur.id";

        RegisterFinalizer(() => RemoveEntities<Editeur>(response, idField));
        response.Should().MatchSnapshot(options => options.IgnoreField(idField));

        await DbContext.Entry(toUpdate).ReloadAsync();
        toUpdate.Nom.Should().Be("test");
    }

    [Fact]
    public async Task UpdateEditeur_ShouldFail_Duplicate()
    {
        var toUpdate = new Editeur
        {
            Nom = "zzz"
        };
        var alreadyExists = new Editeur
        {
            Nom = "Test"
        };
        RegisterFinalizer(() => RemoveEntities(toUpdate, alreadyExists));
        await AddEntities(toUpdate, alreadyExists);

        string response = await TestServices.ExecuteRequestAsync(
            b => b.SetQuery(
                $$"""
                  mutation {
                      updateEditeur(input: {editeur: {id: "{{toUpdate.Id}}", nom: "test"} })
                      {
                          editeur {
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
    public async Task UpdateEditeur_ShouldFail_IdNotFound()
    {
        string response = await TestServices.ExecuteRequestAsync(
            b => b.SetQuery(
                $$"""
                  mutation {
                      updateEditeur(input: {editeur: {id: "{{Guid.Empty}}", nom: "test"} })
                      {
                          editeur {
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
    public async Task DeleteEditeur_ShouldSucceed()
    {
        var toDelete = new Editeur
        {
            Nom = "Test"
        };
        RegisterFinalizer(() => ReloadAndRemoveEntities(toDelete));
        await AddEntities(toDelete);

        string response = await TestServices.ExecuteRequestAsync(
            b => b.SetQuery(
                $$"""
                  mutation {
                      deleteEditeur(input: {id: "{{toDelete.Id}}"})
                      {
                          editeur {
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

        const string idField = "data.deleteEditeur.editeur.id";
        response.Should().MatchSnapshot(options => options.IgnoreField(idField));
    }

    [Fact]
    public async Task DeleteEditeur_ShouldFail()
    {
        string response = await TestServices.ExecuteRequestAsync(
            b => b.SetQuery(
                $$"""
                  mutation {
                      deleteEditeur(input: {id: "{{Guid.NewGuid()}}"})
                      {
                          editeur {
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

        response.Should().MatchSnapshot(options => options.IgnoreField("data.deleteEditeur.errors[0].message"));
    }
}
