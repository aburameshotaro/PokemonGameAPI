using PokemonGame.Application.Features.Types.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Application.Features.PokemonSpecies.DTOs
{
    public sealed class PokemonSpeciesDto
    {
        public int Id { get; init; }

        public string Name { get; init; } = string.Empty;

        public string Description { get; init; } = string.Empty;

        public PokemonTypeDto Type1 { get; init; } = null!;

        public PokemonTypeDto? Type2 { get; init; }
    }
}
