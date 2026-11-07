using Newtonsoft.Json;
using PokemonGame.Domain.Entities;

namespace PokemonGame.Infrastructure.Persistence.Seeders
{
    public class LocationSeeder : Seeder
    {
        private readonly string _locationsJsonPath = Path.Combine(AppContext.BaseDirectory, "Data", "locations.json");
        public LocationSeeder(PokemonDbContext dbContext) : base(dbContext) { }
        public override Task Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Locations.Any())
                {
                    var locations = GetLocations();
                    _dbContext.Locations.AddRange(locations);
                    _dbContext.SaveChanges();
                }
            }
            return Task.CompletedTask;
        }

        private IEnumerable<Location> GetLocations()
        {
            string jsonLocations = File.ReadAllText(_locationsJsonPath);
            var locations = JsonConvert.DeserializeObject<List<Location>>(jsonLocations);
            return locations;
        }
    }
}
