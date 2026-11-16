using AutoMapper;
using PokemonGame.Application.Features.Pokemons.DTOs;
using PokemonGame.Application.Features.PokemonSpecies.DTOs;
using PokemonGame.Application.Features.Types.DTOs;
using PokemonGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Application.Features.PokemonSpecies.Mappings
{
    public class PokemonSpeciesMappings : Profile
    {
        public PokemonSpeciesMappings()
        {
            CreateMap<Domain.Entities.PokemonSpecies, PokemonSpeciesDto>();
        }
    }
}
