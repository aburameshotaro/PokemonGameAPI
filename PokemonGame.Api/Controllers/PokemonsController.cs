using MediatR;
using Microsoft.AspNetCore.Mvc;
using PokemonGame.Application.Features.Pokemons.Queries.GetPokemonById;

namespace PokemonGame.Api.Controllers
{
    [ApiController]
    [Route("api/pokemon")]
    public class PokemonsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PokemonsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pokemon = await _mediator.Send(new GetPokemonByIdQuery(id));

            if (pokemon is null)
            {
                return NotFound();
            }

            return Ok(pokemon);
        }
    }
}
