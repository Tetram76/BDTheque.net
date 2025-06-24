namespace BDTheque.Tests.Mutations;

using Xunit.Abstractions;

public class CollectionTest(ITestOutputHelper testOutputHelper) : DatabaseTest(testOutputHelper)
{
    [Fact]
    public async Task CreateCollection_ShouldSucceed()
    {
        var editeur = new Editeur
        {
            Nom = "Aa"
        };
        RegisterFinalizer(() => RemoveEntities(editeur));
        await AddEntities(editeur);

        string response = await TestServices.ExecuteRequestAsync(
            b => b.SetQuery(
                $$"""
                  mutation {
                      createCollection(input: {collection: {nom: "test", editeur: {id: "{{editeur.Id}}"} } })
                      {
                          collection {
                              id
                              nom
                              editeur {
                                  nom
                              }
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

        const string idField = "data.createCollection.collection.id";

        RegisterFinalizer(() => RemoveEntities<Collection>(response, idField));

        response.Should().MatchSnapshot(options => options.IgnoreField(idField));

        Collection collection = Assert.IsType<Collection>(await DbContext.Collections.FindAsync(response.AsGuid(idField)));
        collection.Nom.Should().Be("test");
    }

    [Fact]
    public async Task CreateCollection_ShouldNotFail()
    {
        var editeur = new Editeur
        {
            Nom = "Aa"
        };
        RegisterFinalizer(() => RemoveEntities(editeur));
        await AddEntities(editeur);

        var alreadyExists = new Collection
        {
            Nom = "test",
            Editeur = editeur
        };
        RegisterFinalizer(() => RemoveEntities(alreadyExists));
        await AddEntities(alreadyExists);

        var editeur2 = new Editeur
        {
            Nom = "Bb"
        };
        RegisterFinalizer(() => RemoveEntities(editeur2));
        await AddEntities(editeur2);

        string response = await TestServices.ExecuteRequestAsync(
            b => b.SetQuery(
                $$"""
                  mutation {
                      createCollection(input: {collection: {nom: "test", editeur: {id: "{{editeur2.Id}}"} } })
                      {
                          collection {
                              id
                              nom
                              editeur {
                                  nom
                              }
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

        const string idField = "data.createCollection.collection.id";

        RegisterFinalizer(() => RemoveEntities<Collection>(response, idField));

        response.Should().MatchSnapshot(options => options.IgnoreField(idField));

        Collection collection = Assert.IsType<Collection>(await DbContext.Collections.FindAsync(response.AsGuid(idField)));
        collection.Nom.Should().Be("test");
    }

    [Fact]
    public async Task CreateCollection_ShouldFail()
    {
        var editeur = new Editeur
        {
            Nom = "Aa"
        };
        RegisterFinalizer(() => RemoveEntities(editeur));
        await AddEntities(editeur);

        var alreadyExists = new Collection
        {
            Nom = "test",
            Editeur = editeur
        };
        RegisterFinalizer(() => RemoveEntities(alreadyExists));
        await AddEntities(alreadyExists);

        string response = await TestServices.ExecuteRequestAsync(
            b => b.SetQuery(
                $$"""
                  mutation {
                      createCollection(input: {collection: {nom: "test", editeur: {id: "{{editeur.Id}}"} } })
                      {
                          collection {
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
    public async Task UpdateCollection_ShouldSucceed()
    {
        var editeur = new Editeur
        {
            Nom = "Aa"
        };
        RegisterFinalizer(() => RemoveEntities(editeur));
        await AddEntities(editeur);

        var toUpdate = new Collection
        {
            Nom = "zzz",
            Editeur = editeur
        };
        RegisterFinalizer(() => RemoveEntities(toUpdate));
        await AddEntities(toUpdate);

        string response = await TestServices.ExecuteRequestAsync(
            b => b.SetQuery(
                $$"""
                  mutation {
                      updateCollection(input: {collection: {id: "{{toUpdate.Id}}", nom: "test"} })
                      {
                          collection {
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

        const string idField = "data.updateCollection.collection.id";

        RegisterFinalizer(() => RemoveEntities<Collection>(response, idField));
        response.Should().MatchSnapshot(options => options.IgnoreField(idField));

        await DbContext.Entry(toUpdate).ReloadAsync();
        toUpdate.Nom.Should().Be("test");
    }

    [Fact]
    public async Task UpdateCollection_ShouldFail_Duplicate()
    {
        var editeur = new Editeur
        {
            Nom = "Aa"
        };
        RegisterFinalizer(() => RemoveEntities(editeur));
        await AddEntities(editeur);

        var toUpdate = new Collection
        {
            Nom = "zzz",
            Editeur = editeur
        };
        var alreadyExists = new Collection
        {
            Nom = "Test",
            Editeur = editeur
        };
        RegisterFinalizer(() => RemoveEntities(toUpdate, alreadyExists));
        await AddEntities(toUpdate, alreadyExists);

        string response = await TestServices.ExecuteRequestAsync(
            b => b.SetQuery(
                $$"""
                  mutation {
                      updateCollection(input: {collection: {id: "{{toUpdate.Id}}", nom: "test"} })
                      {
                          collection {
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
    public async Task UpdateCollection_ShouldFail_IdNotFound()
    {
        string response = await TestServices.ExecuteRequestAsync(
            b => b.SetQuery(
                $$"""
                  mutation {
                      updateCollection(input: {collection: {id: "{{Guid.Empty}}", nom: "test"} })
                      {
                          collection {
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
    public async Task DeleteCollection_ShouldSucceed()
    {
        var editeur = new Editeur
        {
            Nom = "Aa"
        };
        RegisterFinalizer(() => RemoveEntities(editeur));
        await AddEntities(editeur);

        var toDelete = new Collection
        {
            Nom = "Test",
            Editeur = editeur
        };
        RegisterFinalizer(() => ReloadAndRemoveEntities(toDelete));
        await AddEntities(toDelete);

        string response = await TestServices.ExecuteRequestAsync(
            b => b.SetQuery(
                $$"""
                  mutation {
                      deleteCollection(input: {id: "{{toDelete.Id}}"})
                      {
                          collection {
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

        const string idField = "data.deleteCollection.collection.id";
        response.Should().MatchSnapshot(options => options.IgnoreField(idField));
    }

    [Fact]
    public async Task DeleteCollection_ShouldFail()
    {
        string response = await TestServices.ExecuteRequestAsync(
            b => b.SetQuery(
                $$"""
                  mutation {
                      deleteCollection(input: {id: "{{Guid.NewGuid()}}"})
                      {
                          collection {
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

        response.Should().MatchSnapshot(options => options.IgnoreField("data.deleteCollection.errors[0].message"));
    }
}
