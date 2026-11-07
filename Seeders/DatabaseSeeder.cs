using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Seeders
{
    public class DatabaseSeeder
    {
        private readonly IEnumerable<Seeder> _seeders;

        public DatabaseSeeder(IEnumerable<Seeder> seeders)
        {
            _seeders = seeders;
        }

        public async Task Seed()
        {
            foreach (var seeder in _seeders)
            {
                await seeder.Seed();
            }
        }
    }
}
