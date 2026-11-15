using PokemonGame.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Application.Features.Trainers.Command.AddPokemonToTeam
{
    public sealed record AddPokemonToTeamCommand(int TrainerId, int PokemonId) : ICommand;
}
