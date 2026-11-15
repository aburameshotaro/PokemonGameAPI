using PokemonGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Tests.Common.Builders
{
    public class PokemonSpeciesBuilder
    {
        private readonly PokemonSpecies _species = new()
        {
            Id = 25,
            Name = "Pikachu",
            Description = "Electric mouse",
            Height = 4,
            Weight = 60,

            BasicHp = 100,
            BasicAttack = 55,
            BasicDefense = 40,
            BasicSpecialAttack = 50,
            BasicSpecialDefense = 50,
            BasicSpeed = 90,

            ExperienceSpeed = 2,
            BasicExperienceGivenWhenDefeated = 112,

            Type1Id = 13
        };

        public PokemonSpecies Build()
        {
            return _species;
        }

        public PokemonSpeciesBuilder WithId(int id)
        {
            _species.Id = id;
            return this;
        }

        public PokemonSpeciesBuilder WithName(string name)
        {
            _species.Name = name;
            return this;
        }

        public PokemonSpeciesBuilder WithType(int typeId)
        {
            _species.Type1Id = typeId;
            return this;
        }
    }
}
