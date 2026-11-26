using Microsoft.Extensions.Logging;
using PokemonGame.Application.Abstractions;
using PokemonGame.Domain.Entities;
using PokemonGame.Domain.Interfaces;
using PokemonGame.Domain.Interfaces.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Application.Features.Trainers.Command.CreateTrainer
{
    public sealed class CreateTrainerCommandHandler : ICommandHandler<CreateTrainerCommand, int>
    {
        private readonly ITrainerRepository _trainerRepository;
        private readonly IPokemonSpeciesRepository _speciesRepository;
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IAttackRepository _attackRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ILogger<CreateTrainerCommandHandler> _logger;

        public CreateTrainerCommandHandler(
            ITrainerRepository trainerRepository,
            IPokemonSpeciesRepository speciesRepository,
            IPokemonRepository pokemonRepository,
            IAttackRepository attackRepository,
            IPasswordHasher passwordHasher,
            IUnitOfWork unitOfWork,
            ILogger<CreateTrainerCommandHandler> logger)
        {
            _trainerRepository = trainerRepository;
            _speciesRepository = speciesRepository;
            _pokemonRepository = pokemonRepository;
            _attackRepository = attackRepository;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<int> Handle(
            CreateTrainerCommand command,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating trainer {Username}", command.Username);

            if (await _trainerRepository.GetByUsernameAsync(command.Username) is not null)
            {
                throw new InvalidOperationException(
                    "Username already exists.");
            }
            if (await _trainerRepository.ExistsByEmailAsync(command.Email))
            {
                throw new InvalidOperationException(
                    "Email already exists.");
            }

            var passwordHash = _passwordHasher.Hash(command.Password);

            var trainer = new Trainer(
                command.Username,
                passwordHash,
                command.Email);
            trainer.IsAdmin = false;
            trainer.IsNpc = false;

            var starterSpecies = await _speciesRepository.GetByIdAsync(command.StarterSpeciesId);

            if (starterSpecies is null)
            {
                throw new InvalidOperationException(
                    "Starter species not found.");
            }
            if(!starterSpecies.IsStarter)
            {
                throw new InvalidOperationException(
                    "Selected species is not a valid starter.");
            }

            var starterMoves = await _attackRepository.GetMovesForLevelAsync(starterSpecies.Id, 5);

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

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            _logger.LogInformation("Trainer {TrainerId} created with starter {Species}", trainer.Id, starterSpecies.Name);

            return trainer.Id;
        }
    }
}
