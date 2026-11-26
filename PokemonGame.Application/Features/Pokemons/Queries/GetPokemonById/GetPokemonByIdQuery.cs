using PokemonGame.Application.Abstractions;
using PokemonGame.Application.Features.Pokemons.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Application.Features.Pokemons.Queries.GetPokemonById
{
    public sealed record GetPokemonByIdQuery(int PokemonId) : IQuery<PokemonDto?>;
}
