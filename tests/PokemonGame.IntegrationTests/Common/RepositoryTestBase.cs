using PokemonGame.Domain.Entities;
using PokemonGame.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.IntegrationTests.Common
{
    public abstract class RepositoryTestBase : IClassFixture<DatabaseFixture>
    {
        protected readonly DatabaseFixture Fixture;

        protected RepositoryTestBase(DatabaseFixture fixture)
        {
            Fixture = fixture;
        }

        protected PokemonDbContext CreateContext()
        {
            var context = TestDbContextFactory.Create(Fixture.Connection);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return context;
        }

        protected async Task<(PokemonSpecies Species, Attack Attack)> SeedPokemonDataAsync(PokemonDbContext context)
        {
            var (_, species, attack) =
                await TestDataSeeder.SeedBasicPokemonData(context);

            return (species, attack);
        }
    }
}
