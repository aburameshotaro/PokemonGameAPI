using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonGame.Migrations
{
    /// <inheritdoc />
    public partial class RecreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CostForBuy = table.Column<int>(type: "int", nullable: false),
                    CostForSell = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Verb = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trainers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Admin = table.Column<bool>(type: "bit", nullable: false),
                    Bot = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatchingItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    CatchingRate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatchingItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CatchingItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HealingItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    HpHealed = table.Column<int>(type: "int", nullable: false),
                    StatusHealed = table.Column<bool>(type: "bit", nullable: false),
                    Revive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealingItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealingItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bags",
                columns: table => new
                {
                    TrainerId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bags", x => new { x.TrainerId, x.ItemId });
                    table.ForeignKey(
                        name: "FK_Bags_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bags_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Attacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Power = table.Column<int>(type: "int", nullable: true),
                    Accuracy = table.Column<int>(type: "int", nullable: false),
                    DamageClass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    InflictsStatusId = table.Column<int>(type: "int", nullable: true),
                    ChancesToInflictStatus = table.Column<int>(type: "int", nullable: true),
                    TypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attacks_Status_InflictsStatusId",
                        column: x => x.InflictsStatusId,
                        principalTable: "Status",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Attacks_Types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PokemonSpecies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    BasicHp = table.Column<int>(type: "int", nullable: false),
                    BasicAttack = table.Column<int>(type: "int", nullable: false),
                    BasicDefense = table.Column<int>(type: "int", nullable: false),
                    BasicSpecialAttack = table.Column<int>(type: "int", nullable: false),
                    BasicSpecialDefense = table.Column<int>(type: "int", nullable: false),
                    BasicSpeed = table.Column<int>(type: "int", nullable: false),
                    ExperienceSpeed = table.Column<int>(type: "int", nullable: false),
                    BasicExperienceGivenWhenDefeated = table.Column<int>(type: "int", nullable: false),
                    Type1Id = table.Column<int>(type: "int", nullable: false),
                    Type2Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonSpecies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonSpecies_Types_Type1Id",
                        column: x => x.Type1Id,
                        principalTable: "Types",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PokemonSpecies_Types_Type2Id",
                        column: x => x.Type2Id,
                        principalTable: "Types",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TypesAttackBonuses",
                columns: table => new
                {
                    AttackTypeId = table.Column<int>(type: "int", nullable: false),
                    DefenderTypeId = table.Column<int>(type: "int", nullable: false),
                    Multiplier = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypesAttackBonuses", x => new { x.AttackTypeId, x.DefenderTypeId });
                    table.ForeignKey(
                        name: "FK_TypesAttackBonuses_Types_AttackTypeId",
                        column: x => x.AttackTypeId,
                        principalTable: "Types",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TypesAttackBonuses_Types_DefenderTypeId",
                        column: x => x.DefenderTypeId,
                        principalTable: "Types",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Evolutions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Level = table.Column<int>(type: "int", nullable: true),
                    ItemId = table.Column<int>(type: "int", nullable: true),
                    EvolutionByTrade = table.Column<bool>(type: "bit", nullable: false),
                    FromPokemonSpeciesId = table.Column<int>(type: "int", nullable: false),
                    ToPokemonSpeciesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evolutions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evolutions_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Evolutions_PokemonSpecies_FromPokemonSpeciesId",
                        column: x => x.FromPokemonSpeciesId,
                        principalTable: "PokemonSpecies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Evolutions_PokemonSpecies_ToPokemonSpeciesId",
                        column: x => x.ToPokemonSpeciesId,
                        principalTable: "PokemonSpecies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LevelUpAttacks",
                columns: table => new
                {
                    PokemonSpeciesId = table.Column<int>(type: "int", nullable: false),
                    AttackId = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LevelUpAttacks", x => new { x.PokemonSpeciesId, x.AttackId });
                    table.ForeignKey(
                        name: "FK_LevelUpAttacks_Attacks_AttackId",
                        column: x => x.AttackId,
                        principalTable: "Attacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LevelUpAttacks_PokemonSpecies_PokemonSpeciesId",
                        column: x => x.PokemonSpeciesId,
                        principalTable: "PokemonSpecies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PokemonProbabilityAppearances",
                columns: table => new
                {
                    PokemonSpeciesId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    Probability = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonProbabilityAppearances", x => new { x.PokemonSpeciesId, x.LocationId });
                    table.ForeignKey(
                        name: "FK_PokemonProbabilityAppearances_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonProbabilityAppearances_PokemonSpecies_PokemonSpeciesId",
                        column: x => x.PokemonSpeciesId,
                        principalTable: "PokemonSpecies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Pokemons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    MaxHp = table.Column<int>(type: "int", nullable: false),
                    Attack = table.Column<int>(type: "int", nullable: false),
                    Defense = table.Column<int>(type: "int", nullable: false),
                    SpecialAttack = table.Column<int>(type: "int", nullable: false),
                    SpecialDefense = table.Column<int>(type: "int", nullable: false),
                    Speed = table.Column<int>(type: "int", nullable: false),
                    Exp = table.Column<int>(type: "int", nullable: false),
                    CurrentHp = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: true),
                    TrainerId = table.Column<int>(type: "int", nullable: true),
                    NumberOnTeam = table.Column<int>(type: "int", nullable: true),
                    AttackId1 = table.Column<int>(type: "int", nullable: false),
                    AttackId2 = table.Column<int>(type: "int", nullable: true),
                    AttackId3 = table.Column<int>(type: "int", nullable: true),
                    AttackId4 = table.Column<int>(type: "int", nullable: true),
                    PokemonSpeciesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pokemons_Attacks_AttackId1",
                        column: x => x.AttackId1,
                        principalTable: "Attacks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pokemons_Attacks_AttackId2",
                        column: x => x.AttackId2,
                        principalTable: "Attacks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pokemons_Attacks_AttackId3",
                        column: x => x.AttackId3,
                        principalTable: "Attacks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pokemons_Attacks_AttackId4",
                        column: x => x.AttackId4,
                        principalTable: "Attacks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pokemons_PokemonSpecies_PokemonSpeciesId",
                        column: x => x.PokemonSpeciesId,
                        principalTable: "PokemonSpecies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pokemons_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pokemons_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TMAttacks",
                columns: table => new
                {
                    PokemonSpeciesId = table.Column<int>(type: "int", nullable: false),
                    AttackId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TMAttacks", x => new { x.PokemonSpeciesId, x.AttackId });
                    table.ForeignKey(
                        name: "FK_TMAttacks_Attacks_AttackId",
                        column: x => x.AttackId,
                        principalTable: "Attacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TMAttacks_PokemonSpecies_PokemonSpeciesId",
                        column: x => x.PokemonSpeciesId,
                        principalTable: "PokemonSpecies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attacks_InflictsStatusId",
                table: "Attacks",
                column: "InflictsStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Attacks_TypeId",
                table: "Attacks",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Bags_ItemId",
                table: "Bags",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CatchingItems_ItemId",
                table: "CatchingItems",
                column: "ItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Evolutions_FromPokemonSpeciesId",
                table: "Evolutions",
                column: "FromPokemonSpeciesId");

            migrationBuilder.CreateIndex(
                name: "IX_Evolutions_ItemId",
                table: "Evolutions",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Evolutions_ToPokemonSpeciesId",
                table: "Evolutions",
                column: "ToPokemonSpeciesId");

            migrationBuilder.CreateIndex(
                name: "IX_HealingItems_ItemId",
                table: "HealingItems",
                column: "ItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LevelUpAttacks_AttackId",
                table: "LevelUpAttacks",
                column: "AttackId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonProbabilityAppearances_LocationId",
                table: "PokemonProbabilityAppearances",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_AttackId1",
                table: "Pokemons",
                column: "AttackId1");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_AttackId2",
                table: "Pokemons",
                column: "AttackId2");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_AttackId3",
                table: "Pokemons",
                column: "AttackId3");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_AttackId4",
                table: "Pokemons",
                column: "AttackId4");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_PokemonSpeciesId",
                table: "Pokemons",
                column: "PokemonSpeciesId");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_StatusId",
                table: "Pokemons",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_TrainerId",
                table: "Pokemons",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonSpecies_Type1Id",
                table: "PokemonSpecies",
                column: "Type1Id");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonSpecies_Type2Id",
                table: "PokemonSpecies",
                column: "Type2Id");

            migrationBuilder.CreateIndex(
                name: "IX_TMAttacks_AttackId",
                table: "TMAttacks",
                column: "AttackId");

            migrationBuilder.CreateIndex(
                name: "IX_TypesAttackBonuses_DefenderTypeId",
                table: "TypesAttackBonuses",
                column: "DefenderTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bags");

            migrationBuilder.DropTable(
                name: "CatchingItems");

            migrationBuilder.DropTable(
                name: "Evolutions");

            migrationBuilder.DropTable(
                name: "HealingItems");

            migrationBuilder.DropTable(
                name: "LevelUpAttacks");

            migrationBuilder.DropTable(
                name: "PokemonProbabilityAppearances");

            migrationBuilder.DropTable(
                name: "Pokemons");

            migrationBuilder.DropTable(
                name: "TMAttacks");

            migrationBuilder.DropTable(
                name: "TypesAttackBonuses");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Trainers");

            migrationBuilder.DropTable(
                name: "Attacks");

            migrationBuilder.DropTable(
                name: "PokemonSpecies");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "Types");
        }
    }
}
