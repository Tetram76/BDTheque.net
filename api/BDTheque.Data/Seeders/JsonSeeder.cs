namespace BDTheque.Data.Seeders;

using System.Reflection;
using System.Text.Json;
using BDTheque.Model.Entities;
using BDTheque.Model.Entities.Abstract;
using Microsoft.EntityFrameworkCore;

public static class JsonSeeder
{
    private static IEnumerable<T> LoadDataFromEmbeddedResource<T>(string resourceName)
    {
        var assembly = Assembly.GetExecutingAssembly();

        using Stream? stream = assembly.GetManifestResourceStream(resourceName);
        using var reader = new StreamReader(stream!);

        string json = reader.ReadToEnd();
        return JsonSerializer.Deserialize<IEnumerable<T>>(json) ?? new List<T>();
    }

    private static IEnumerable<T> LoadDataFromFile<T>(string filePath) where T : VersioningEntity
    {
        using var reader = new StreamReader(filePath);

        string json = reader.ReadToEnd();
        return JsonSerializer.Deserialize<IEnumerable<T>>(json) ?? new List<T>();
    }

    private static void SeedFromFile<T>(this ModelBuilder modelBuilder, string fileName) where T : VersioningEntity =>
        modelBuilder.Entity<T>().HasData(LoadDataFromFile<T>(fileName));

    private static void SeedFromFile<T>(this ModelBuilder modelBuilder) where T : VersioningEntity =>
        modelBuilder.SeedFromFile<T>($"{typeof(T).Name}.json");

    private static void SeedFromResource<T>(this ModelBuilder modelBuilder, string resourceName) where T : VersioningEntity =>
        modelBuilder.Entity<T>().HasData(LoadDataFromEmbeddedResource<T>(resourceName));

    private static void SeedFromResource<T>(this ModelBuilder modelBuilder) where T : VersioningEntity =>
        modelBuilder.SeedFromResource<T>($"BDTheque.Data.Seeders.{typeof(T).Name}.json");

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.SeedFromResource<Genre>();
        modelBuilder.SeedFromResource<Option>();
    }
}
