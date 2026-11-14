using PokemonGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Tests.Common.Builders
{
    public class PokemonBuilder
    {
        private PokemonSpecies _species =
            new PokemonSpeciesBuilder().Build();

        private Attack _attack =
            new AttackBuilder().Build();

        private int _level = 10;

        public Pokemon Build()
        {
            return new Pokemon(
                _species,
                _level,
                _attack);
        }

        public PokemonBuilder WithSpecies(
            PokemonSpecies species)
        {
            _species = species;
            return this;
        }

        public PokemonBuilder WithAttack(
            Attack attack)
        {
            _attack = attack;
            return this;
        }

        public PokemonBuilder WithLevel(
            int level)
        {
            _level = level;
            return this;
        }
    }
}
