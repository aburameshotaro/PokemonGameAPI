using AutoMapper;
using PokemonGame.Application.Abstractions;
using PokemonGame.Application.Features.Trainers.DTOs;
using PokemonGame.Application.Features.Trainers.Queries.GetTrainerById;
using PokemonGame.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Application.Features.Trainers.Queries.GetAllTrainersQuery
{
    public sealed class GetAllTrainersQueryHandler
: IQueryHandler<GetAllTrainersQuery, List<TrainerDto>>
    {
        private readonly ITrainerRepository _trainerRepository;
        private readonly IMapper _mapper;

        public GetAllTrainersQueryHandler(
            ITrainerRepository trainerRepository,
            IMapper mapper)
        {
            _trainerRepository = trainerRepository;
            _mapper = mapper;
        }

        public async Task<List<TrainerDto>> Handle(
            GetAllTrainersQuery request,
            CancellationToken cancellationToken)
        {
            var trainers = await _trainerRepository
                .GetAllAsync();

            return _mapper.Map<List<TrainerDto>>(trainers);
        }
    }
}
