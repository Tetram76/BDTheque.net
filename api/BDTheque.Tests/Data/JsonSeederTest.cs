namespace BDTheque.Tests.Data;

using System.Reflection;
using BDTheque.Data.Seeders;
using BDTheque.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

#pragma warning disable CA1861
public class JsonSeederTest
{
    [Fact]
    public void SeedFromJson_ShouldSucceed()
    {
        var migrationBuilder = new MigrationBuilder(typeof(NpgsqlMigrationBuilderExtensions).GetTypeInfo().Assembly.GetName().Name);
        migrationBuilder.SeedFromJson<Genre>(
            """
            [
               {"Id": 123456, "Nom": "Nom1"},
               {"Id": 78910, "Nom": "Nom2"},
            ]
            """
        );

        var insertedDateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        migrationBuilder.Operations.Should()
            .HaveCount(2)
            .And.AllBeOfType<InsertDataOperation>()
            .And.BeEquivalentTo(
                [
                    new
                    {
                        Table = "genres",
                        Columns = new[]
                        {
                            "id", "nom", "created_at", "updated_at"
                        },
                        Values = new object[]
                        {
                            123456, "Nom1", insertedDateTime, insertedDateTime
                        }
                    },
                    new
                    {
                        Table = "genres",
                        Columns = new[]
                        {
                            "id", "nom", "created_at", "updated_at"
                        },
                        Values = new object[]
                        {
                            78910, "Nom2", insertedDateTime, insertedDateTime
                        }
                    }
                ]
            );
    }

    [Fact]
    public void SeedFromJson_ShouldFail()
    {
        var migrationBuilder = new MigrationBuilder(typeof(NpgsqlMigrationBuilderExtensions).GetTypeInfo().Assembly.GetName().Name);

        migrationBuilder
            .Invoking(
                builder => builder.SeedFromJson<Genre>(
                    """
                    [
                       {"Id": 123456, "Nom": "Nom1"},
                       {"Id": 78910, "Nom": "Nom2", "Unknown": null},
                    ]
                    """
                )
            )
            .Should().ThrowExactly<JsonSeeder.JsonSeederException>();
    }

    [Fact]
    public void UnseedFromJson_ShouldSucceed()
    {
        var migrationBuilder = new MigrationBuilder(typeof(NpgsqlMigrationBuilderExtensions).GetTypeInfo().Assembly.GetName().Name);
        migrationBuilder.UnseedFromJson<Genre>(
            """
            [
               {"Id": 123456, "Nom": "Nom1"},
               {"Id": 78910, "Nom": "Nom2"},
            ]
            """
        );

        migrationBuilder.Operations.Should()
            .HaveCount(2)
            .And.AllBeOfType<DeleteDataOperation>()
            .And.BeEquivalentTo(
                [
                    new
                    {
                        Table = "genres",
                        KeyColumns = new[]
                        {
                            "id"
                        },
                        KeyValues = new[]
                        {
                            123456
                        }
                    },
                    new
                    {
                        Table = "genres",
                        KeyColumns = new[]
                        {
                            "id"
                        },
                        KeyValues = new[]
                        {
                            78910
                        }
                    }
                ]
            );
    }

    [Fact]
    public void UnseedFromJson_ShouldFail()
    {
        var migrationBuilder = new MigrationBuilder(typeof(NpgsqlMigrationBuilderExtensions).GetTypeInfo().Assembly.GetName().Name);

        migrationBuilder
            .Invoking(
                builder => builder.UnseedFromJson<Genre>(
                    """
                    [
                       {"Id": 123456, "Nom": "Nom1"},
                       {"Nom": "Nom2", "Unknown": null},
                    ]
                    """
                )
            )
            .Should().ThrowExactly<JsonSeeder.JsonSeederException>();

        migrationBuilder
            .Invoking(
                builder => builder.UnseedFromJson<GenreAlbum>("[{}]")
            )
            .Should().ThrowExactly<JsonSeeder.JsonSeederException>();
    }
}
