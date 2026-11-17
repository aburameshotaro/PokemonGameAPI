using AutoMapper;
using PokemonGame.Application.Abstractions;
using PokemonGame.Application.Features.Trainers.DTOs;
using PokemonGame.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Application.Features.Trainers.Queries.GetTrainerById
{
    public sealed class GetTrainerByIdQueryHandler
    : IQueryHandler<GetTrainerByIdQuery, TrainerDetailsDto?>
    {
        private readonly ITrainerRepository _trainerRepository;
        private readonly IMapper _mapper;

        public GetTrainerByIdQueryHandler(
            ITrainerRepository trainerRepository,
            IMapper mapper)
        {
            _trainerRepository = trainerRepository;
            _mapper = mapper;
        }

        public async Task<TrainerDetailsDto?> Handle(
            GetTrainerByIdQuery request,
            CancellationToken cancellationToken)
        {
            var trainer = await _trainerRepository
                .GetByIdWithTeamAsync(request.TrainerId);

            return trainer is null
                ? null
                : _mapper.Map<TrainerDetailsDto>(trainer);
        }
    }
}
