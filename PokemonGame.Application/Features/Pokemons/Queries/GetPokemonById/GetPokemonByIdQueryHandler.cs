using AutoMapper;
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
        private readonly IMapper _mapper;

        public GetPokemonByIdQueryHandler(
            IPokemonRepository pokemonRepository,
            IMapper mapper)
        {
            _pokemonRepository = pokemonRepository;
            _mapper = mapper;
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

            return _mapper.Map<PokemonDto>(pokemon);
        }
    }
}
