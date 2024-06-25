namespace BDTheque.Data.Seeders;

using System.Collections.Immutable;
using System.Reflection;
using System.Text;

using BDTheque.Model.Entities.Abstract;

using HotChocolate.Types.Relay;

using Humanizer;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public static class JsonSeeder
{
    private static IEnumerable<T> LoadDataFromJson<T>(string json) =>
        JsonConvert.DeserializeObject<IEnumerable<T>>(json) ?? [];

    private static async Task<string> LoadJsonFromEmbeddedResource(string resourceName)
    {
        var assembly = Assembly.GetExecutingAssembly();

        await using Stream? stream = assembly.GetManifestResourceStream(resourceName);
        using var reader = new StreamReader(stream!);

        return await reader.ReadToEndAsync();
    }

    private static async Task<string> LoadJsonFromFile(string filePath)
    {
        using var reader = new StreamReader(filePath);
        return await reader.ReadToEndAsync();
    }

    #region Seed

    public static void SeedFromJson<T>(this ModelBuilder modelBuilder, string json) where T : VersioningEntity
    {
        IEnumerable<T> entities = LoadDataFromJson<T>(json).ToImmutableList();

        var insertedTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        foreach (T entity in entities)
        {
            entity.CreatedAt = insertedTime;
            entity.UpdatedAt = insertedTime;
        }

        modelBuilder.Entity<T>().HasData(entities);
    }

    public static void SeedFromJson<T>(this MigrationBuilder migrationBuilder, string json) where T : VersioningEntity
    {
        var dataFromEmbeddedResource = LoadDataFromJson<JObject>(json).ToImmutableList();
        if (dataFromEmbeddedResource.Count == 0)
            return;

        var entityProperties = typeof(T).GetRuntimeProperties()
            .Select(info => info.Name)
            .ToImmutableList();

        string tableName = typeof(T).Name.Pluralize().ToSnakeCase();
        var insertedTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        dataFromEmbeddedResource.ForEach(
            o =>
            {
                IEnumerable<JProperty> fields = o.Properties()
                    .Where(
                        property => entityProperties.Contains(property.Name, StringComparer.InvariantCultureIgnoreCase)
                            ? true
                            : throw new JsonSeederException($"Unknown <{typeof(T).Name}> entity field \"{property.Name}\"")
                    )
                    .OrderBy(property => property.Name)
                    .ToImmutableArray();

                object?[] values = fields
                    .Select(property => property.Value.ToObject<object>())
                    .ToArray();

                migrationBuilder.InsertData(
                    tableName,
                    fields
                        .Select(info => info.Name.ToSnakeCase())
                        .Concat(["created_at", "updated_at"])
                        .ToArray(),
                    values
                        .Concat([insertedTime, insertedTime])
                        .ToArray()
                );
            }
        );
    }

    private static async Task SeedFromFile<T>(this ModelBuilder modelBuilder) where T : VersioningEntity =>
        await modelBuilder.SeedFromFile<T>($"{typeof(T).Name}.json");

    private static async Task SeedFromFile<T>(this ModelBuilder modelBuilder, string fileName) where T : VersioningEntity =>
        modelBuilder.SeedFromJson<T>(await LoadJsonFromFile(fileName));

    public static async Task SeedFromFile<T>(this MigrationBuilder migrationBuilder) where T : VersioningEntity =>
        await migrationBuilder.SeedFromFile<T>($"{typeof(T).Name}.json");

    public static async Task SeedFromFile<T>(this MigrationBuilder migrationBuilder, string fileName) where T : VersioningEntity =>
        migrationBuilder.SeedFromJson<T>(await LoadJsonFromFile(fileName));

    public static async Task SeedFromResource<T>(this ModelBuilder modelBuilder) where T : VersioningEntity =>
        await modelBuilder.SeedFromResource<T>($"BDTheque.Data.Seeders.{typeof(T).Name}.json");

    public static async Task SeedFromResource<T>(this ModelBuilder modelBuilder, string resourceName) where T : VersioningEntity =>
        modelBuilder.SeedFromJson<T>(await LoadJsonFromEmbeddedResource(resourceName));

    public static async Task SeedFromResource<T>(this MigrationBuilder migrationBuilder) where T : VersioningEntity =>
        await migrationBuilder.SeedFromResource<T>($"BDTheque.Data.Seeders.{typeof(T).Name}.json");

    public static async Task SeedFromResource<T>(this MigrationBuilder migrationBuilder, string resourceName) where T : VersioningEntity =>
        migrationBuilder.SeedFromJson<T>(await LoadJsonFromEmbeddedResource(resourceName));

    #endregion

    #region Unseed

    public static void UnseedFromJson<T>(this MigrationBuilder migrationBuilder, string json) where T : VersioningEntity
    {
        var dataFromEmbeddedResource = LoadDataFromJson<JObject>(json).ToImmutableList();
        if (dataFromEmbeddedResource.Count == 0)
            return;

        var idProperties = typeof(T).GetRuntimeProperties()
            .Where(info => info.GetCustomAttributes<IDAttribute>().Any())
            .Select(info => info.Name)
            .ToImmutableArray();

        if (idProperties.Length == 0)
            throw new JsonSeederException($"Entity <{typeof(T).Name}> has no key field");

        string tableName = typeof(T).Name.Pluralize().ToSnakeCase();
        dataFromEmbeddedResource.ForEach(
            o =>
            {
                var fields = o.Properties()
                    .Where(property => idProperties.Contains(property.Name, StringComparer.InvariantCultureIgnoreCase))
                    .OrderBy(property => property.Name)
                    .ToImmutableArray();

                if (fields.Length != idProperties.Length)
                    throw new JsonSeederException($"<{typeof(T).Name}> entity keys expected {string.Join(", ", idProperties.Select(s => $"\"{s}\""))} but found {(fields.Length == 0 ? "none" : string.Join(", ", fields.Select(property => $"\"{property.Name}\"")))}");

                object?[] values = fields
                    .Select(property => property.Value.ToObject<object>())
                    .ToArray();

                migrationBuilder.DeleteData(
                    tableName,
                    fields.Select(info => info.Name.ToSnakeCase()).ToArray(),
                    values
                );
            }
        );
    }

    public static async Task UnseedFromResource<T>(this MigrationBuilder migrationBuilder) where T : VersioningEntity =>
        await migrationBuilder.UnseedFromResource<T>($"BDTheque.Data.Seeders.{typeof(T).Name}.json");

    private static async Task UnseedFromResource<T>(this MigrationBuilder migrationBuilder, string resourceName) where T : VersioningEntity =>
        migrationBuilder.UnseedFromJson<T>(await LoadJsonFromEmbeddedResource(resourceName));

    #endregion

    public class JsonSeederException(string message) : Exception(message);
}
