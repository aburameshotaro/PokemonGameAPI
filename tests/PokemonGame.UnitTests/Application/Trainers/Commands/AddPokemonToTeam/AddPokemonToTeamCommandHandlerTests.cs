using Moq;
using PokemonGame.Application.Features.Trainers.Command.AddPokemonToTeam;
using PokemonGame.Domain.Interfaces;
using PokemonGame.Tests.Common.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.UnitTests.Application.Trainers.Commands.AddPokemonToTeam
{
    public class AddPokemonToTeamCommandHandlerTests
    {
        private readonly Mock<ITrainerRepository> _trainerRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        private readonly AddPokemonToTeamCommandHandler _handler;

        public AddPokemonToTeamCommandHandlerTests()
        {
            _trainerRepositoryMock = new();
            _unitOfWorkMock = new();

            _handler = new AddPokemonToTeamCommandHandler(
                _trainerRepositoryMock.Object,
                _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handle_AddsPokemonToTeam()
        {
            var trainer = new TrainerBuilder()
                .Build();

            var pokemon = new PokemonBuilder()
                .WithId(1)
                .Build();

            trainer.CatchPokemon(pokemon);

            _trainerRepositoryMock
                .Setup(x => x.GetByIdWithTeamAsync(1))
                .ReturnsAsync(trainer);

            await _handler.Handle(
                new AddPokemonToTeamCommand(trainer.Id, pokemon.Id),
                CancellationToken.None);

            Assert.Contains(pokemon, trainer.GetTeam());

            _unitOfWorkMock.Verify(
                x => x.SaveChangesAsync(
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}
