using MediatR;
using Microsoft.AspNetCore.Mvc;
using PokemonGame.Application.Features.PokemonSpecies.Queries.GetAllPokemonSpecies;

namespace PokemonGame.Api.Controllers
{
    [ApiController]
    [Route("api/pokemonspecies")]
    public class PokemonSpeciesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PokemonSpeciesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(
                new GetAllPokemonSpeciesQuery());

            return Ok(result);
        }
    }
}
