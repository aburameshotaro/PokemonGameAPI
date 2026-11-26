using Microsoft.Extensions.Logging;
using Moq;
using PokemonGame.Application.Features.Trainers.Command.CreateTrainer;
using PokemonGame.Domain.Entities;
using PokemonGame.Domain.Interfaces;
using PokemonGame.Domain.Interfaces.Security;
using PokemonGame.Tests.Common.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.UnitTests.Application.Trainers.Commands.CreateTrainer
{
    public class CreateTrainerCommandHandlerTests
    {
        private readonly Mock<ITrainerRepository> _trainerRepositoryMock = new();
        private readonly Mock<IPokemonSpeciesRepository> _speciesRepositoryMock = new();
        private readonly Mock<IAttackRepository> _attackRepositoryMock = new();
        private readonly Mock<IPokemonRepository> _pokemonRepositoryMock = new();
        private readonly Mock<IPasswordHasher> _passwordHasherMock = new();
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
        private readonly Mock<ILogger<CreateTrainerCommandHandler>> _loggerMock = new();

        private CreateTrainerCommandHandler CreateHandler()
        {
            return new CreateTrainerCommandHandler(
                _trainerRepositoryMock.Object,
                _speciesRepositoryMock.Object,
                _pokemonRepositoryMock.Object,
                _attackRepositoryMock.Object,
                _passwordHasherMock.Object,
                _unitOfWorkMock.Object,
                _loggerMock.Object);
        }

        [Fact]
        public async Task Handle_CreatesTrainerWithStarterPokemon()
        {
            // Arrange
            var starterSpecies = new PokemonSpeciesBuilder()
                .WithId(1)
                .WithStarter(true)
                .Build();

            var tackle = new AttackBuilder()
                .WithId(1)
                .WithName("Tackle")
                .Build();

            _speciesRepositoryMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(starterSpecies);

            _attackRepositoryMock
                .Setup(x => x.GetMovesForLevelAsync(1, 5))
                .ReturnsAsync(new List<Attack>
                {
                tackle
                });

            _passwordHasherMock
                .Setup(x => x.Hash("Password123"))
                .Returns("HASHED_PASSWORD");

            Trainer? createdTrainer = null;

            _trainerRepositoryMock
                .Setup(x => x.AddAsync(It.IsAny<Trainer>()))
                .Callback<Trainer>(t => createdTrainer = t)
                .ReturnsAsync((Trainer trainer) => trainer);

            var handler = CreateHandler();

            var command = new CreateTrainerCommand(
                "Ash",
                "Password123",
                "ash@test.com",
                1);

            // Act
            var result = await handler.Handle(
                command,
                CancellationToken.None);

            // Assert
            Assert.NotNull(createdTrainer);

            Assert.Single(createdTrainer!.Pokemons);

            var starterPokemon =
                createdTrainer.Pokemons.Single();

            Assert.Equal(
                starterSpecies.Id,
                starterPokemon.PokemonSpeciesId);

            Assert.Contains(
                starterPokemon,
                createdTrainer.GetTeam());

            _trainerRepositoryMock.Verify(
                x => x.AddAsync(It.IsAny<Trainer>()),
                Times.Once);

            _unitOfWorkMock.Verify(
                x => x.SaveChangesAsync(
                    It.IsAny<CancellationToken>()),
                Times.Once);

            _passwordHasherMock.Verify(
                x => x.Hash("Password123"),
                Times.Once);
        }

        [Fact]
        public async Task Handle_WhenStarterSpeciesNotFound_Throws()
        {
            _speciesRepositoryMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync((PokemonSpecies?)null);

            var handler = CreateHandler();

            var command = new CreateTrainerCommand(
                "Ash",
                "Password123",
                "ash@test.com",
                1);

            await Assert.ThrowsAsync<InvalidOperationException>(
                () => handler.Handle(
                    command,
                    CancellationToken.None));
        }

        [Fact]
        public async Task Handle_WhenSpeciesIsNotStarter_Throws()
        {
            var species = new PokemonSpeciesBuilder()
                .WithStarter(false)
                .Build();

            _speciesRepositoryMock
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(species);

            var handler = CreateHandler();

            await Assert.ThrowsAsync<InvalidOperationException>(
                () => handler.Handle(
                    new CreateTrainerCommand(
                        "Ash",
                        "Password123",
                        "ash@test.com",
                        species.Id),
                    CancellationToken.None));
        }

        [Fact]
        public async Task Handle_WhenStarterHasNoMoves_Throws()
        {
            var species = new PokemonSpeciesBuilder()
                .WithStarter(true)
                .Build();

            _speciesRepositoryMock
                .Setup(x => x.GetByIdAsync(species.Id))
                .ReturnsAsync(species);

            _attackRepositoryMock
                .Setup(x => x.GetMovesForLevelAsync(species.Id, 5))
                .ReturnsAsync(new List<Attack>());

            var handler = CreateHandler();

            await Assert.ThrowsAsync<InvalidOperationException>(
                () => handler.Handle(
                    new CreateTrainerCommand(
                        "Ash",
                        "Password123",
                        "ash@test.com",
                        species.Id),
                    CancellationToken.None));
        }

    }
}
