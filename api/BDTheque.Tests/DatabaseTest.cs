namespace BDTheque.Tests;

using BDTheque.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit.Abstractions;

[Collection(CollectionName)]
public class DatabaseTest : BaseTest
{
    public const string CollectionName = "UseDatabase";

    public DatabaseTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        DbContext = TestServices.Services.GetRequiredService<BDThequeContext>();
        DbContext.Database.Migrate();
    }

    public BDThequeContext DbContext { get; }

    public Task AddEntities<T>(params T[] entities) where T : class =>
        AddEntities(entities.Cast<object>().ToArray());

    public async Task AddEntities(params object[] entities)
    {
        entities = entities
            .Where(
                entity =>
                {
                    EntityEntry entry = DbContext.Entry(entity);
                    entry.Reload();
                    return entry.State == EntityState.Detached;
                }
            )
            .ToArray();

        DbContext.AddRange(entities);
        await DbContext.SaveChangesAsync();
    }

    public Task RemoveEntities<T>(params T[] entities) where T : class =>
        RemoveEntities(entities.Cast<object>().ToArray());

    public async Task RemoveEntities(params object[] entities)
    {
        entities = entities
            .Where(
                entity =>
                {
                    EntityEntry entry = DbContext.Entry(entity);
                    entry.Reload();
                    return entry.State != EntityState.Detached;
                }
            )
            .ToArray();

        if (entities.Length == 0)
            return;

        DbContext.RemoveRange(entities);
        await DbContext.SaveChangesAsync();
    }

    public async Task RemoveEntities<T>(params Guid[] ids) where T : class
    {
        List<T> entities = [];
        foreach (Guid entityId in ids)
            if (await DbContext.FindAsync<T>(entityId) is { } entity)
                entities.Add(entity);

        await RemoveEntities(entities.Cast<object>().ToArray());
    }

    public async Task RemoveEntities<T>(string jsonGraphQLResponse, params string[] entityIdJsonPaths) where T : class
    {
        if (JsonConvert.DeserializeObject<JObject>(jsonGraphQLResponse) is not { } jsonObject) return;

        List<Guid> ids = [];

        foreach (string entityIdJsonPath in entityIdJsonPaths)
        {
            if (jsonObject.SelectToken(entityIdJsonPath) is not { } entityId) continue;
            ids.Add(entityId.ToObject<Guid>());
        }

        await RemoveEntities<T>(ids.ToArray());
    }
}
