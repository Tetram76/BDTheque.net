using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BDTheque.Data.Migrations
{
    using BDTheque.Data.Seeders;
    using BDTheque.Model.Entities;

    /// <inheritdoc />
    public partial class SeedGenres : Migration
    {
        /// <inheritdoc />
        protected override async void Up(MigrationBuilder migrationBuilder)
        {
            await migrationBuilder.SeedFromResource<Genre>();
        }

        /// <inheritdoc />
        protected override async void Down(MigrationBuilder migrationBuilder)
        {
            await migrationBuilder.UnseedFromResource<Genre>();
        }
    }
}
