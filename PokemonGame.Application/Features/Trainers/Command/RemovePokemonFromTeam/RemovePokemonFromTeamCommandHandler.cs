using PokemonGame.Application.Abstractions;
using PokemonGame.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Application.Features.Trainers.Command.RemovePokemonFromTeam
{
    public class RemovePokemonFromTeamCommandHandler : ICommandHandler<RemovePokemonFromTeamCommand>
    {
        private readonly ITrainerRepository _trainerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemovePokemonFromTeamCommandHandler(
            ITrainerRepository trainerRepository,
            IUnitOfWork unitOfWork)
        {
            _trainerRepository = trainerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(
            RemovePokemonFromTeamCommand command,
            CancellationToken cancellationToken)
        {
            var trainer = await _trainerRepository
                .GetByIdWithTeamAsync(command.TrainerId);

            if (trainer is null)
            {
                throw new InvalidOperationException(
                    "Trainer not found.");
            }

            trainer.RemovePokemonFromTeam(
                command.PokemonId);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);
        }
    }
}
