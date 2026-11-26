using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonGame.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddIsStarterToPokemonSpecies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLegendary",
                table: "PokemonSpecies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsStarter",
                table: "PokemonSpecies",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLegendary",
                table: "PokemonSpecies");

            migrationBuilder.DropColumn(
                name: "IsStarter",
                table: "PokemonSpecies");
        }
    }
}
