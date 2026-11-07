using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PokemonGame.Entities;
using PokemonGame.Repos;

namespace PokemonGame.Infrastructure.Persistence.Seeders
{
    public class AttacksSeeder : Seeder
    {
        public readonly string _allAttacksUrl = "https://pokeapi.co/api/v2/move?limit=10000";
        private IStatusRepo _statusRepo;

        public AttacksSeeder(PokemonDbContext pokemonDb, IStatusRepo statusRepo ) : base(pokemonDb)
        {
            _statusRepo = statusRepo;
        }

        public async override Task Seed()
        {
            if (!_dbContext.Attacks.Any())
            {
                var attacks = await GetAttacks();
                _dbContext.Attacks.AddRange(attacks);
                _dbContext.SaveChanges();
            }

        }

        private async Task<IEnumerable<Attack>> GetAttacks()
        {
            List<Attack> attacks = new List<Attack>();
            using HttpClient client = new();
            var json = await client.GetStringAsync(_allAttacksUrl);
            JObject allAtacks = JObject.Parse(json);
            foreach (var attack in allAtacks["results"]!)
            {
                var attackJson = await client.GetStringAsync(attack["url"]!.Value<string>()!);
                JObject attackDetails = JObject.Parse(attackJson);
                var englishEffect = attackDetails["effect_entries"]?.FirstOrDefault(e => e["language"]?["name"]?.Value<string>() == "en");
                string? effectDescription = englishEffect?["effect"]?.Value<string>();
                int? inflictedStatusId = await _statusRepo.FindIdByDescription(effectDescription);
                int chanceToInflictStatus = attackDetails["effect_chance"]?.Value<int?>() ?? 0;
                
                Attack attackDB = new Attack()
                {
                    Id = attackDetails["id"]!.Value<int>(),
                    Name = attackDetails["name"]!.Value<string>()!,
                    Accuracy = attackDetails["accuracy"].Value<int?>() ?? 100,
                    DamageClass = attackDetails["damage_class"]["name"].Value<string>(),
                    Power = attackDetails["power"].Value<int?>() ?? 0,
                    Priority = attackDetails["priority"]!.Value<int>(),
                    TypeId = int.Parse(attackDetails["type"]["url"].Value<string>().TrimEnd('/').Split("/").Last()),
                    InflictsStatusId = inflictedStatusId.HasValue ? inflictedStatusId : null,
                    ChancesToInflictStatus = inflictedStatusId.HasValue ? chanceToInflictStatus : null,
                };
                if (!_dbContext.Types.Any(t => t.Id == attackDB.TypeId))
                {
                    continue;
                }
                attacks.Add(attackDB);
            }

            return attacks;
        }
    }
}
