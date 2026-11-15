using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Application.Features.Pokemons.DTOs
{
    public sealed class PokemonDto
    {
        public int Id { get; init; }

        public string Name { get; init; } = string.Empty;

        public int Level { get; init; }

        public int CurrentHp { get; init; }

        public int MaxHp { get; init; }

        public string SpeciesName { get; init; } = string.Empty;
    }
}
