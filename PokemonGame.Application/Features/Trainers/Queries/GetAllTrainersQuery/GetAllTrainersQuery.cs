using PokemonGame.Application.Abstractions;
using PokemonGame.Application.Features.Trainers.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Application.Features.Trainers.Queries.GetAllTrainersQuery
{
    public sealed record GetAllTrainersQuery() : IQuery<List<TrainerDto>>;
}
