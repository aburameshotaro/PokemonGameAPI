using AutoMapper;
using PokemonGame.Application.Features.Pokemons.DTOs;
using PokemonGame.Application.Features.Trainers.DTOs;
using PokemonGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Application.Features.Trainers.Mappings
{
    public class TrainerMappings : Profile
    {
        public TrainerMappings()
        {
            CreateMap<Trainer, TrainerDto>();
            CreateMap<Trainer, TrainerDetailsDto>();
        }
    }
}
