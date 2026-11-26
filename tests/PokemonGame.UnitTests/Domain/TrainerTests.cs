using PokemonGame.Domain.Entities;
using PokemonGame.Tests.Common.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.UnitTests.Domain
{
    public class TrainerTests
    {
        [Fact]
        public void AddPokemonToTeam_WhenTeamHasLessThanSixPokemon_AddPokemon()
        {
            // Arrange
            var trainer = new TrainerBuilder().Build();
            var pokemon = new PokemonBuilder().WithId(1).Build();
            trainer.CatchPokemon(pokemon);
            // Act
            trainer.AddPokemonToTeam(pokemon.Id);
            // Assert
            Assert.Contains(pokemon, trainer.GetTeam());
        }

        [Theory]
        [InlineData(2)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        public void AddPokemonToTeam_WhenTeamHasLessThanSixPokemon_AddManyPokemons(int numberOfPokemons)
        {
            // Arrange
            var trainer = new TrainerBuilder().Build();
            for (int i = 1; i <= numberOfPokemons; i++)
            {
                var pokemon = new PokemonBuilder().WithId(i).Build();
                trainer.CatchPokemon(pokemon);
                trainer.AddPokemonToTeam(pokemon.Id);
            }

            // Assert
            Assert.Equal(numberOfPokemons, trainer.GetTeam().Count);
        }

        [Fact]
        public void AddPokemonToTeam_WhenTeamHasSixPokemon_ThrowsException()
        {
            // Arrange
            var trainer = new TrainerBuilder().Build();
            for (int i = 1; i <= 6; i++)
            {
                var pokemon = new PokemonBuilder().WithId(i).Build();
                trainer.CatchPokemon(pokemon);
                trainer.AddPokemonToTeam(pokemon.Id);
            }
            var pokemonToBeAdded = new PokemonBuilder().WithId(7).Build();

            // Assert
            Assert.Throws<InvalidOperationException>(
                () => trainer.AddPokemonToTeam(pokemonToBeAdded.Id));
        }

        [Fact]
        public void AddPokemonToTeam_WhenPokemonIsAlreadyInTeam_ThrowsException()
        {
            // Arrange
            var trainer = new TrainerBuilder().Build();
            var pokemon = new PokemonBuilder().WithId(1).Build();
            trainer.CatchPokemon(pokemon);
            trainer.AddPokemonToTeam(pokemon.Id);

            // Assert
            Assert.Throws<InvalidOperationException>(
                () => trainer.AddPokemonToTeam(pokemon.Id));
        }

        [Fact]
        public void RemovePokemonFromTeam_WhenPokemonInTeam_RemovePokemon()
        {
            // Arrange
            var trainer = new TrainerBuilder().Build();

            var pokemon1 = new PokemonBuilder()
                .WithId(1)
                .Build();

            var pokemon2 = new PokemonBuilder()
                .WithId(2)
                .Build();
            trainer.CatchPokemon(pokemon1);
            trainer.CatchPokemon(pokemon2);
            trainer.AddPokemonToTeam(pokemon1.Id);
            trainer.AddPokemonToTeam(pokemon2.Id);

            // Act
            trainer.RemovePokemonFromTeam(pokemon1.Id);

            // Assert
            Assert.Null(pokemon1.NumberOnTeam);
            Assert.Equal(1, trainer.TeamSize);
        }

        [Fact]
        public void RemovePokemonFromTeam_WhenPokemonNotInTeam_ThrowsException()
        {
            // Arrange
            var trainer = new TrainerBuilder().Build();

            var pokemon1 = new PokemonBuilder()
                .WithId(1)
                .Build();

            var pokemon2 = new PokemonBuilder()
                .WithId(2)
                .Build();

            trainer.CatchPokemon(pokemon1);
            trainer.CatchPokemon(pokemon2);
            trainer.AddPokemonToTeam(pokemon1.Id);

            // Assert
            Assert.Throws<InvalidOperationException>(
                () => trainer.RemovePokemonFromTeam(pokemon2.Id));
        }

        [Fact]
        public void RemovePokemonFromTeam_WhenPokemonDoesntBelongToTrainer_ThrowsException()
        {
            // Arrange
            var trainer = new TrainerBuilder().Build();

            var pokemon1 = new PokemonBuilder()
                .WithId(1)
                .Build();

            var pokemon2 = new PokemonBuilder()
                .WithId(2)
                .Build();

            trainer.CatchPokemon(pokemon1);
            trainer.AddPokemonToTeam(pokemon1.Id);

            // Assert
            Assert.Throws<InvalidOperationException>(
                () => trainer.RemovePokemonFromTeam(pokemon2.Id));
        }

        [Fact]
        public void RemovePokemonFromTeam_WhenPokemonIsOnlyOneInTeam_ThrowsException()
        {
            // Arrange
            var trainer = new TrainerBuilder().Build();

            var pokemon = new PokemonBuilder()
                .WithId(1)
                .Build();

            trainer.CatchPokemon(pokemon);
            trainer.AddPokemonToTeam(pokemon.Id);

            // Assert
            Assert.Throws<InvalidOperationException>(
                () => trainer.RemovePokemonFromTeam(pokemon.Id));
        }

        [Fact]
        public void RemovePokemonFromTeam_WhenPokemonIsInMiddleOfTeam_ChangesTeamNumbersOfPokemonBehindIt()
        {
            // Arrange
            var trainer = new TrainerBuilder().Build();
            for (int i = 1; i <= 6; i++)
            {
                var pokemon = new PokemonBuilder().WithId(i).Build();
                trainer.CatchPokemon(pokemon);
                trainer.AddPokemonToTeam(pokemon.Id);
            }
            // Act
            trainer.RemovePokemonFromTeam(2);

            // Assert
            Assert.Equal(5, trainer.TeamSize);
            Assert.Equal(1, trainer.GetTeam().First(p=>p.Id == 1).NumberOnTeam);
            Assert.Equal(2, trainer.GetTeam().First(p => p.Id == 3).NumberOnTeam);
            Assert.Equal(3, trainer.GetTeam().First(p => p.Id == 4).NumberOnTeam);
            Assert.Equal(4, trainer.GetTeam().First(p => p.Id == 5).NumberOnTeam);
            Assert.Equal(5, trainer.GetTeam().First(p => p.Id == 6).NumberOnTeam);
            Assert.DoesNotContain(trainer.GetTeam(), p => p.Id == 2);
            Assert.DoesNotContain(trainer.GetTeam(), p => p.NumberOnTeam == 6);
        }

        [Fact]
        public void ChangePokemonTeamPosition_When2PokemonsInTeam_SwapPositionOfPokemons()
        {
            // Arrange
            var trainer = new TrainerBuilder().Build();

            var pokemon1 = new PokemonBuilder()
                .WithId(1)
                .Build();

            var pokemon2 = new PokemonBuilder()
                .WithId(2)
                .Build();
            trainer.CatchPokemon(pokemon1);
            trainer.CatchPokemon(pokemon2);
            trainer.AddPokemonToTeam(pokemon1.Id);
            trainer.AddPokemonToTeam(pokemon2.Id);

            // Act
            trainer.ChangePokemonTeamPosition(pokemon1.Id, 2);

            // Assert
            Assert.Equal(2, trainer.TeamSize);
            Assert.Equal(1, pokemon2.NumberOnTeam);
            Assert.Equal(2, pokemon1.NumberOnTeam);
        }

        [Fact]
        public void ChangePokemonTeamPosition_WhenTooFewPokemonsInTeam_ThrowsException()
        {
            // Arrange
            var trainer = new TrainerBuilder().Build();

            var pokemon1 = new PokemonBuilder()
                .WithId(1)
                .Build();

            var pokemon2 = new PokemonBuilder()
                .WithId(2)
                .Build();
            trainer.CatchPokemon(pokemon1);
            trainer.CatchPokemon(pokemon2);
            trainer.AddPokemonToTeam(pokemon1.Id);
            trainer.AddPokemonToTeam(pokemon2.Id);

            // Act
            
            Assert.Throws<InvalidOperationException>(
                () => trainer.ChangePokemonTeamPosition(pokemon1.Id, 5));
        }




    }
}
