using PokemonGame.Application.Abstractions;
using PokemonGame.Application.Features.Pokemons.DTOs;
using PokemonGame.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Application.Features.Pokemons.Queries.GetPokemonById
{
    public sealed class GetPokemonByIdQueryHandler : IQueryHandler<GetPokemonByIdQuery, PokemonDto?>
    {
        private readonly IPokemonRepository _pokemonRepository;

        public GetPokemonByIdQueryHandler(
            IPokemonRepository pokemonRepository)
        {
            _pokemonRepository = pokemonRepository;
        }

        public async Task<PokemonDto?> Handle(
            GetPokemonByIdQuery request,
            CancellationToken cancellationToken)
        {
            var pokemon = await _pokemonRepository.GetByIdAsync(request.PokemonId);

            if (pokemon is null)
            {
                return null;
            }

            return new PokemonDto
            {
                Id = pokemon.Id,
                Name = pokemon.Name,
                Level = pokemon.Level,
                CurrentHp = pokemon.CurrentHp,
                MaxHp = pokemon.MaxHp,
                SpeciesName = pokemon.PokemonSpecies.Name
            };
        }
    }
}
