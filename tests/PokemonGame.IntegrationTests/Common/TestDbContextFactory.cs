using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PokemonGame.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.IntegrationTests.Common
{
    public class TestDbContextFactory
    {
        public static PokemonDbContext Create(SqliteConnection connection)
        {
            var options = new DbContextOptionsBuilder<PokemonDbContext>()
                .UseSqlite(connection)
                .Options;

            var context = new PokemonDbContext(options);

            context.Database.EnsureCreated();

            return context;
        }
    }
}
