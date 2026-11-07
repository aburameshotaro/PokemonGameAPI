using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PokemonGame.Entities;

namespace PokemonGame.Seeders
{
    public class TypesSeeder : Seeder
    {
        private readonly string _typesJsonPath = Path.Combine(AppContext.BaseDirectory, "Data", "types.json");
        public TypesSeeder(PokemonDbContext pokemonDb) : base(pokemonDb)
        {
        }

        public override Task Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Types.Any())
                {
                    var types = GetTypes();
                    _dbContext.Types.AddRange(types);
                    _dbContext.SaveChanges();
                }
            }
            return Task.CompletedTask;
        }

        private IEnumerable<Entities.Type> GetTypes()
        {
            string jsonTypes = File.ReadAllText(_typesJsonPath);
            var types = JsonConvert.DeserializeObject<List<Entities.Type>>(jsonTypes);
            return types;
        }
    }
}
