using PokemonGame.Application.Abstractions;
using PokemonGame.Application.Features.Pokemons.DTOs;
using PokemonGame.Application.Features.PokemonSpecies.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Application.Features.PokemonSpecies.Queries.GetAllPokemonSpecies
{
    public sealed record GetAllPokemonSpeciesQuery() : IQuery<List<PokemonSpeciesDto>>;
}
