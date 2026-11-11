using Newtonsoft.Json;
using PokemonGame.Domain.Entities;

namespace PokemonGame.Infrastructure.Persistence.Seeders
{
    public class StatusesSeeder : Seeder
    {
        private readonly string _statusesJsonPath = Path.Combine(AppContext.BaseDirectory, "Data", "statuses.json");
        public StatusesSeeder(PokemonDbContext pokemonDb) : base(pokemonDb)
        {
        }

        public override Task Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Status.Any())
                {
                    var statuses = GetStatuses();
                    _dbContext.Status.AddRange(statuses);
                    _dbContext.SaveChanges();
                }
            }
            return Task.CompletedTask;
        }

        private IEnumerable<Status> GetStatuses()
        {
            string jsonStatuses = File.ReadAllText(_statusesJsonPath);
            var types = JsonConvert.DeserializeObject<List<Status>>(jsonStatuses);
            return types;
        }
    }
}
