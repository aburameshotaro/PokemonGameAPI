using PokemonGame.Application.Abstractions;
using PokemonGame.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Application.Features.Trainers.Command.ChangePokemonTeamPosition
{
    public class ChangePokemonTeamPositionCommandHandler : ICommandHandler<ChangePokemonTeamPositionCommand>
    {
        private readonly ITrainerRepository _trainerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ChangePokemonTeamPositionCommandHandler(
            ITrainerRepository trainerRepository,
            IUnitOfWork unitOfWork)
        {
            _trainerRepository = trainerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(
    ChangePokemonTeamPositionCommand command,
    CancellationToken cancellationToken)
        {
            var trainer = await _trainerRepository
                .GetByIdWithTeamAsync(command.TrainerId);

            if (trainer is null)
            {
                throw new InvalidOperationException(
                    "Trainer not found.");
            }

            trainer.ChangePokemonTeamPosition(
                command.PokemonId,
                command.Position);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);
        }
    }
}
