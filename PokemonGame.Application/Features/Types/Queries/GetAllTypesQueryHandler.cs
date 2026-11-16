using AutoMapper;
using PokemonGame.Application.Abstractions;
using PokemonGame.Application.Features.Pokemons.DTOs;
using PokemonGame.Application.Features.Pokemons.Queries.GetPokemonById;
using PokemonGame.Application.Features.PokemonSpecies.DTOs;
using PokemonGame.Application.Features.PokemonSpecies.Queries.GetAllPokemonSpecies;
using PokemonGame.Application.Features.Types.DTOs;
using PokemonGame.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Application.Features.Types.Queries
{
    public class GetAllTypesQueryHandler : IQueryHandler<GetAllTypesQuery, List<PokemonTypeDto>>
    {
        private readonly ITypeRepository _typeRepository;
        private readonly IMapper _mapper;

        public GetAllTypesQueryHandler(
            ITypeRepository typeRepository,
            IMapper mapper)
        {
            _typeRepository = typeRepository;
            _mapper = mapper;
        }
        public async Task<List<PokemonTypeDto>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
        {
            var types = await _typeRepository.GetAllAsync();

            if (types is null)
            {
                return null;
            }

            return _mapper.Map<List<PokemonTypeDto>>(types);
        }
    }

    
    }
