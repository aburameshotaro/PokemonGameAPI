using PokemonGame.Application.Abstractions;
using PokemonGame.Domain.Entities;
using PokemonGame.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Application.Features.Trainers.Command.CreateTrainer
{
    public sealed class CreateTrainerCommandHandler
    : ICommandHandler<CreateTrainerCommand, int>
    {
        private readonly ITrainerRepository _trainerRepository;
        private readonly IPokemonSpeciesRepository _speciesRepository;
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IAttackRepository _attackRepository;
        private readonly IUnitOfWork _unitOfWork;

        public async Task<int> Handle(
            CreateTrainerCommand command,
            CancellationToken cancellationToken)
        {
            var trainer = new Trainer(
                command.Username,
                command.Password,
                command.Email);

            var starterSpecies =
                await _speciesRepository.GetByIdAsync(
                    command.StarterSpeciesId);

            if (starterSpecies is null)
            {
                throw new InvalidOperationException(
                    "Starter species not found.");
            }

            var starterMoves =
                await _attackRepository.GetMovesForLevelAsync(starterSpecies.Id, 5);

            var pokemon = new Pokemon(starterSpecies, 5,
    starterMoves.ElementAtOrDefault(0)
        ?? throw new InvalidOperationException(
            "Starter has no moves."),
    starterMoves.ElementAtOrDefault(1),
    starterMoves.ElementAtOrDefault(2),
    starterMoves.ElementAtOrDefault(3));

            trainer.CatchPokemon(pokemon);

            trainer.AddPokemonToTeam(pokemon.Id);

            await _trainerRepository.AddAsync(trainer);
            await _pokemonRepository.AddAsync(pokemon);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return trainer.Id;
        }
    }
}
