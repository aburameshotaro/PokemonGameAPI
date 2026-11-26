using PokemonGame.Application.Abstractions;
using PokemonGame.Application.Features.Pokemons.DTOs;
using PokemonGame.Application.Features.Trainers.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Application.Features.Trainers.Queries.GetTrainerById
{
    public sealed record GetTrainerByIdQuery(int TrainerId) : IQuery<TrainerDetailsDto?>;
}
