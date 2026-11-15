using Microsoft.EntityFrameworkCore;
using PokemonGame.Domain.Entities;
using PokemonGame.Infrastructure.Persistence.Repositories;
using PokemonGame.IntegrationTests.Common;
using PokemonGame.Tests.Common.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.IntegrationTests.Infrastructure.Repositories
{
    public class PokemonRepositoryTests : RepositoryTestBase
    {
        public PokemonRepositoryTests(DatabaseFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task AddAsync_ShouldPersistPokemon()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new PokemonRepository(context);

            var type = new TypeBuilder().Build();

            var (species, attack) = await SeedPokemonDataAsync(context);

            var pokemon = new PokemonBuilder()
            .WithSpecies(species)
            .WithAttack(attack)
            .Build();

            // Act
            await repository.AddAsync(pokemon);

            await context.SaveChangesAsync();

            // Assert
            var savedPokemon = await context.Pokemons
                .FirstOrDefaultAsync();

            Assert.NotNull(savedPokemon);
            Assert.Equal("Pikachu", savedPokemon.Name);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnPokemon_WhenExists()
        {
            // Arrange
            using var context = CreateContext();

            var repository = new PokemonRepository(context);

            var (species, attack) = await SeedPokemonDataAsync(context);

            var pokemon = new PokemonBuilder()
                .WithSpecies(species)
                .WithAttack(attack)
                .Build();

            context.Pokemons.Add(pokemon);

            await context.SaveChangesAsync();

            // Act
            var result = await repository.GetByIdAsync(pokemon.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(pokemon.Id, result.Id);
        }

        [Fact]
        public async Task GetByTrainerIdAsync_ShouldReturnOnlyTrainersPokemon()
        {
            // Arrange
            using var context = CreateContext();

            var repository = new PokemonRepository(context);

            var trainer1 = new TrainerBuilder()
                .WithUsername("Ash")
                .Build();

            var trainer2 = new TrainerBuilder()
                .WithUsername("Misty")
                .Build();

            context.Trainers.AddRange(trainer1, trainer2);

            await context.SaveChangesAsync();

            var (_, species, attack) = await TestDataSeeder.SeedBasicPokemonData(context);

            var pokemon1 = new PokemonBuilder()
                .WithSpecies(species)
                .WithAttack(attack)
                .Build();

            var pokemon2 = new PokemonBuilder()
                .WithSpecies(species)
                .WithAttack(attack)
                .Build();

            pokemon1.TrainerId = trainer1.Id;
            pokemon2.TrainerId = trainer2.Id;

            context.Pokemons.AddRange(pokemon1, pokemon2);

            await context.SaveChangesAsync();

            // Act
            var result = await repository.GetByTrainerIdAsync(trainer1.Id);

            // Assert
            Assert.Single(result);
            Assert.Equal(trainer1.Id, result.First().TrainerId);
        }

        [Fact]
        public async Task Delete_ShouldRemovePokemon()
        {
            // Arrange
            using var context = CreateContext();

            var repository = new PokemonRepository(context);

            var (_, species, attack) = await TestDataSeeder.SeedBasicPokemonData(context);

            var pokemon = new PokemonBuilder()
                .WithSpecies(species)
                .WithAttack(attack)
                .Build();

            context.Pokemons.Add(pokemon);

            await context.SaveChangesAsync();


            // Act
            repository.Delete(pokemon);

            await context.SaveChangesAsync();

            // Assert
            Assert.Empty(context.Pokemons);
        }

    }
}
