using PokemonGame.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Application.Features.Trainers.Command.CreateTrainer
{
    public sealed record CreateTrainerCommand(
    string Username,
    string Password,
    string Email,
    int StarterSpeciesId)
    : ICommand<int>;

}
