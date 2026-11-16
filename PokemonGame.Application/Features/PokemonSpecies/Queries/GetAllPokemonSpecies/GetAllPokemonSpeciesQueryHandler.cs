using AutoMapper;
using MediatR;
using PokemonGame.Application.Abstractions;
using PokemonGame.Application.Features.Pokemons.DTOs;
using PokemonGame.Application.Features.Pokemons.Queries.GetPokemonById;
using PokemonGame.Application.Features.PokemonSpecies.DTOs;
using PokemonGame.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Application.Features.PokemonSpecies.Queries.GetAllPokemonSpecies
{
    public class GetAllPokemonSpeciesQueryHandler :
        IQueryHandler<GetAllPokemonSpeciesQuery, List<PokemonSpeciesDto>>
    {
        private readonly IPokemonSpeciesRepository _pokemonSpeciesRepository;
        private readonly IMapper _mapper;

        public GetAllPokemonSpeciesQueryHandler(
            IPokemonSpeciesRepository pokemonRepository,
            IMapper mapper)
        {
            _pokemonSpeciesRepository = pokemonRepository;
            _mapper = mapper;
        }

        public async Task<List<PokemonSpeciesDto>> Handle(GetAllPokemonSpeciesQuery request, CancellationToken cancellationToken)
        {
            var pokemonSpecies = await _pokemonSpeciesRepository.GetAllAsync();

            if (pokemonSpecies is null)
            {
                return null;
            }

            return _mapper.Map<List<PokemonSpeciesDto>>(pokemonSpecies);
        }
    }
}
