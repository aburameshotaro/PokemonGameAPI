using MediatR;
using Microsoft.AspNetCore.Mvc;
using PokemonGame.Api.Contracts.Trainers;
using PokemonGame.Application.Features.Pokemons.Command.AddPokemonToTeam;
using PokemonGame.Application.Features.Pokemons.Command.ChangePokemonTeamPosition;
using PokemonGame.Application.Features.Pokemons.Command.RemovePokemonFromTeam;

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
    }
}
