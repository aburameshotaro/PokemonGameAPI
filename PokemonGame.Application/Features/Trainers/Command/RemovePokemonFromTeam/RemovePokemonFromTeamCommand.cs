using PokemonGame.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Application.Features.Trainers.Command.RemovePokemonFromTeam
{
    public sealed record RemovePokemonFromTeamCommand(int TrainerId, int PokemonId) : ICommand;
}
