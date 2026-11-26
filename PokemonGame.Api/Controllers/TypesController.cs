using MediatR;
using Microsoft.AspNetCore.Mvc;
using PokemonGame.Application.Features.PokemonSpecies.Queries.GetAllPokemonSpecies;
using PokemonGame.Application.Features.Types.Queries;

namespace PokemonGame.Api.Controllers
{
    [ApiController]
    [Route("api/types")]
    public class TypesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(
                new GetAllTypesQuery());

            return Ok(result);
        }
    }
}
