using PokemonGame.Application.Features.Evolutions.DTOs;
using PokemonGame.Application.Features.Types.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Application.Features.PokemonSpecies.DTOs
{
    public sealed class PokemonSpeciesDetailsDto
    {
        public int Id { get; init; }

        public string Name { get; init; } = string.Empty;

        public string Description { get; init; } = string.Empty;

        public int Height { get; init; }

        public int Weight { get; init; }

        public int BasicHp { get; init; }

        public int BasicAttack { get; init; }

        public int BasicDefense { get; init; }

        public int BasicSpecialAttack { get; init; }

        public int BasicSpecialDefense { get; init; }

        public int BasicSpeed { get; init; }

        public PokemonTypeDto Type1 { get; init; } = null!;

        public PokemonTypeDto? Type2 { get; init; }

        public List<EvolutionDto> Evolutions { get; init; } = [];
    }
}
