using MediatR;
using Microsoft.AspNetCore.Mvc;
using PokemonGame.Api.Contracts.Trainers;
using PokemonGame.Application.Features.Trainers.Command.AddPokemonToTeam;
using PokemonGame.Application.Features.Trainers.Command.ChangePokemonTeamPosition;
using PokemonGame.Application.Features.Trainers.Command.CreateTrainer;
using PokemonGame.Application.Features.Trainers.Command.RemovePokemonFromTeam;
using PokemonGame.Application.Features.Trainers.Queries.GetTrainerById;

namespace PokemonGame.Api.Controllers
{
    [ApiController]
    [Route("api/trainers")]
    public class TrainersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TrainersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTrainer(CreateTrainerRequest request)
        {
            var trainerId = await _mediator.Send(
                new CreateTrainerCommand(
                    request.Username,
                    request.Password,
                    request.Email,
                    request.StarterSpeciesId));

            return CreatedAtAction(
                nameof(GetById),
                new { id = trainerId },
                null);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var trainer = await _mediator.Send(new GetTrainerByIdQuery(id));

            if (trainer is null)
            {
                return NotFound();
            }

            return Ok(trainer);
        }

        [HttpPost("{trainerId:int}/team/{pokemonId:int}")]
        public async Task<IActionResult> AddPokemonToTeam(int trainerId, int pokemonId)
        {
            await _mediator.Send(
                new AddPokemonToTeamCommand(
                    trainerId,
                    pokemonId));

            return NoContent();
        }

        [HttpPut("{trainerId:int}/team/{pokemonId:int}/position/{position:int}")]
        public async Task<IActionResult> ChangeTeamPosition(
        int trainerId,
        int pokemonId,
        int position)
        {
            await _mediator.Send(
                new ChangePokemonTeamPositionCommand(
                    trainerId,
                    pokemonId,
                    position));

            return NoContent();
        }

        [HttpPut("{trainerId:int}/team/{pokemonId:int}/removefromteam")]
        public async Task<IActionResult> RemovePokemonFromTeam(
        int trainerId,
        int pokemonId,
        int position)
        {
            await _mediator.Send(
                new RemovePokemonFromTeamCommand(
                    trainerId,
                    pokemonId));

            return NoContent();
        }

    }
}
