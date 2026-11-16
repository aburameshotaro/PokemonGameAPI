using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Application.Features.Evolutions.DTOs
{
    public class EvolutionDto
    {
        public int SpeciesId { get; init; }

        public string Name { get; init; } = string.Empty;

        public int? Level { get; init; }
    }
}
