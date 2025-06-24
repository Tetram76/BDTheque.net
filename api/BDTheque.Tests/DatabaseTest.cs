namespace BDTheque.Tests;

using BDTheque.Data.Context;
using BDTheque.Model.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Xunit.Abstractions;

[Collection(CollectionName)]
public class DatabaseTest : BaseTest
{
    private const string CollectionName = "UseDatabase";

    protected DatabaseTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        DbContext = TestServices.Services.GetRequiredService<BDThequeContext>();
        DbContext.Database.Migrate();
    }

    protected BDThequeContext DbContext { get; }

    protected async Task AddEntities<T>(params T[] entities) where T : class, IBaseEntity
    {
        entities = entities
            .Where(entity => DbContext.Entry(entity) is { State: EntityState.Detached })
            .ToArray();

        if (entities.Length == 0)
            return;

        DbContext.AddRange(entities.Cast<object>().ToArray());
        await DbContext.SaveChangesAsync();
    }

    protected async Task ReloadAndRemoveEntities<T>(params T[] entities) where T : class, IBaseEntity
    {
        foreach (T entity in entities)
            await DbContext.Entry(entity).ReloadAsync();
        await RemoveEntities(entities);
    }

    protected async Task RemoveEntities<T>(params T[] entities) where T : class, IBaseEntity
    {
        entities = entities
            .Where(entity => DbContext.Entry(entity) is not { State: EntityState.Detached })
            .ToArray();

        if (entities.Length == 0)
            return;

        DbContext.RemoveRange(entities.Cast<object>().ToArray());
        await DbContext.SaveChangesAsync();
    }

    private async Task RemoveEntities<T>(params Guid[] ids) where T : class, IBaseEntity
    {
        List<T> entities = [];
        foreach (Guid entityId in ids)
            if (await DbContext.FindAsync<T>(entityId) is { } entity)
                entities.Add(entity);

        await RemoveEntities(entities.ToArray());
    }

    protected async Task RemoveEntities<T>(string jsonGraphQLResponse, params string[] entityIdJsonPaths) where T : class, IBaseEntity
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
