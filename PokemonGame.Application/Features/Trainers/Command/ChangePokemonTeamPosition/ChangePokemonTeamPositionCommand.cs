using PokemonGame.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Application.Features.Trainers.Command.ChangePokemonTeamPosition
{
    public sealed record ChangePokemonTeamPositionCommand(int TrainerId, int PokemonId, int Position) : ICommand;
}
