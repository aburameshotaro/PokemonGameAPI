using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonGame.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ConfigurationsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bags_Items_ItemId",
                table: "Bags");

            migrationBuilder.DropForeignKey(
                name: "FK_Bags_Trainers_TrainerId",
                table: "Bags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypesAttackBonuses",
                table: "TypesAttackBonuses");

            migrationBuilder.DropIndex(
                name: "IX_TypesAttackBonuses_DefenderTypeId",
                table: "TypesAttackBonuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TMAttacks",
                table: "TMAttacks");

            migrationBuilder.DropIndex(
                name: "IX_TMAttacks_AttackId",
                table: "TMAttacks");

            migrationBuilder.AlterColumn<int>(
                name: "DefenderTypeId",
                table: "TypesAttackBonuses",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<int>(
                name: "AttackTypeId",
                table: "TypesAttackBonuses",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "AttackId",
                table: "TMAttacks",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<int>(
                name: "PokemonSpeciesId",
                table: "TMAttacks",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "PokemonProbabilityAppearances",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<int>(
                name: "PokemonSpeciesId",
                table: "PokemonProbabilityAppearances",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypesAttackBonuses",
                table: "TypesAttackBonuses",
                columns: new[] { "DefenderTypeId", "AttackTypeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TMAttacks",
                table: "TMAttacks",
                columns: new[] { "AttackId", "PokemonSpeciesId" });

            migrationBuilder.CreateIndex(
                name: "IX_TypesAttackBonuses_AttackTypeId",
                table: "TypesAttackBonuses",
                column: "AttackTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TMAttacks_PokemonSpeciesId",
                table: "TMAttacks",
                column: "PokemonSpeciesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bags_Items_ItemId",
                table: "Bags",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bags_Trainers_TrainerId",
                table: "Bags",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bags_Items_ItemId",
                table: "Bags");

            migrationBuilder.DropForeignKey(
                name: "FK_Bags_Trainers_TrainerId",
                table: "Bags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypesAttackBonuses",
                table: "TypesAttackBonuses");

            migrationBuilder.DropIndex(
                name: "IX_TypesAttackBonuses_AttackTypeId",
                table: "TypesAttackBonuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TMAttacks",
                table: "TMAttacks");

            migrationBuilder.DropIndex(
                name: "IX_TMAttacks_PokemonSpeciesId",
                table: "TMAttacks");

            migrationBuilder.AlterColumn<int>(
                name: "AttackTypeId",
                table: "TypesAttackBonuses",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "DefenderTypeId",
                table: "TypesAttackBonuses",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<int>(
                name: "PokemonSpeciesId",
                table: "TMAttacks",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "AttackId",
                table: "TMAttacks",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "PokemonProbabilityAppearances",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<int>(
                name: "PokemonSpeciesId",
                table: "PokemonProbabilityAppearances",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypesAttackBonuses",
                table: "TypesAttackBonuses",
                columns: new[] { "AttackTypeId", "DefenderTypeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TMAttacks",
                table: "TMAttacks",
                columns: new[] { "PokemonSpeciesId", "AttackId" });

            migrationBuilder.CreateIndex(
                name: "IX_TypesAttackBonuses_DefenderTypeId",
                table: "TypesAttackBonuses",
                column: "DefenderTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TMAttacks_AttackId",
                table: "TMAttacks",
                column: "AttackId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bags_Items_ItemId",
                table: "Bags",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bags_Trainers_TrainerId",
                table: "Bags",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "Id");
        }
    }
}
