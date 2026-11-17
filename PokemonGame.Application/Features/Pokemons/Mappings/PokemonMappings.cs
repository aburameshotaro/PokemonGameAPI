using AutoMapper;
using PokemonGame.Application.Features.Pokemons.DTOs;
using PokemonGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Application.Features.Pokemons.Mappings
{
    public class PokemonMappings : Profile
    {
        public PokemonMappings()
        {
            CreateMap<Pokemon, PokemonDto>().ForMember(
                dest => dest.SpeciesName,
                opt => opt.MapFrom(
                    src => src.PokemonSpecies.Name));
        }
    }
}
