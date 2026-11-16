using AutoMapper;
using PokemonGame.Application.Features.Types.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Application.Features.Types.Mappings
{
    public class TypeMappings : Profile
    {
        public TypeMappings() 
        {
            CreateMap<Domain.Entities.Type, PokemonTypeDto>();
        }
    }
}
