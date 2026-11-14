using PokemonGame.Domain.Entities;
using PokemonGame.Infrastructure.Persistence;
using PokemonGame.Tests.Common.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.IntegrationTests.Common
{
    public static class TestDataSeeder
    {
        public static async Task<(Domain.Entities.Type Type, PokemonSpecies Species, Attack Attack)>
            SeedBasicPokemonData(PokemonDbContext context)
        {
            var type = new TypeBuilder().Build();

            var species = new PokemonSpeciesBuilder()
                .Build();

            var attack = new AttackBuilder()
                .Build();

            context.Types.Add(type);
            context.PokemonSpecies.Add(species);
            context.Attacks.Add(attack);

            await context.SaveChangesAsync();

            return (type, species, attack);
        }
    }
}
