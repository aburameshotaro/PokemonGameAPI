using Newtonsoft.Json;
using PokemonGame.Domain.Entities;
using PokemonGame.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Infrastructure.Persistence.Seeders
{
    public class PokemonProbabilityAppearanceSeeder : Seeder
    {
        private readonly string _probabilitiesJsonPath = Path.Combine(AppContext.BaseDirectory, "Data", "pokemon_probabilities.json");

        public PokemonProbabilityAppearanceSeeder(PokemonDbContext pokemonDb) : base(pokemonDb)
        {
        }

        public override Task Seed()
        {
            if (!_dbContext.PokemonProbabilityAppearances.Any())
            {
                var pokemonProbabilityAppearances = GetPokemonProbabilityAppearances();
                _dbContext.PokemonProbabilityAppearances.AddRange(pokemonProbabilityAppearances);
                _dbContext.SaveChanges();
            }
            return Task.CompletedTask;
        }

        private List<ProbabilityAppearance> GetPokemonProbabilityAppearances()
        {
            string jsonTypes = File.ReadAllText(_probabilitiesJsonPath);
            var probabilities = JsonConvert.DeserializeObject<List<ProbabilityAppearance>>(jsonTypes);
            return probabilities;
        }
    }
}
