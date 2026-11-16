using PokemonGame.Application.Abstractions;
using PokemonGame.Application.Features.PokemonSpecies.DTOs;
using PokemonGame.Application.Features.Types.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Application.Features.Types.Queries
{
    public sealed record GetAllTypesQuery() : IQuery<List<PokemonTypeDto>>;
}
