using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PokemonGame.Entities;
using PokemonGame.Repos;

namespace PokemonGame.Seeders
{
    public class AttacksSeeder : Seeder
    {
        public readonly string _allAttacksUrl = "https://pokeapi.co/api/v2/move?limit=10000";
        public AttacksSeeder(PokemonDbContext pokemonDb) : base(pokemonDb)
        {
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
            dynamic allAtacks = JObject.Parse(json);
            var statusRepo = new StatusRepo(_dbContext);
            foreach (var attack in allAtacks.results)
            {
                var attackJson = await client.GetStringAsync(attack.url.ToString());
                dynamic attackDetails = JObject.Parse(attackJson);
                int? inflictedStatusId = statusRepo.FindIdByDescription(attackDetails.effect_entries[0].effect.ToString());
                int? chanceToInflictStatus = attackDetails.effect_chance ? attackDetails.effect_chance : 100;
                Attack attackDB = new Attack()
                {
                    Id = attackDetails.id,
                    Name = attackDetails.name,
                    Accuracy = attackDetails.accuracy,
                    DamageClass = attackDetails.damage_class.name,
                    Power = attackDetails.power,
                    Priority = attackDetails.priority,
                    TypeId = int.Parse(attackDetails.type.url.ToString().Split("/")[6]),
                    InflictsStatusId = inflictedStatusId.HasValue ? inflictedStatusId : null,
                    ChancesToInflictStatus = inflictedStatusId.HasValue ? chanceToInflictStatus : null,
                };

                attacks.Add(attackDB);
            }

            return attacks;
        }
    }
}
