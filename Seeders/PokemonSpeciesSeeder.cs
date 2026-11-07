using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using PokemonGame.Entities;
using PokemonGame.Repos;

namespace PokemonGame.Seeders
{
    public class PokemonSpeciesSeeder : Seeder
    {
        public readonly string _allPokemonSpeciesUrl = "https://pokeapi.co/api/v2/pokemon?limit=151";
        public readonly string _getPokemonDetails = @"https://pokeapi.co/api/v2/pokemon-species/";
        public PokemonSpeciesSeeder(PokemonDbContext pokemonDb) : base(pokemonDb)
        {
        }

        public async override Task Seed()
        {
            if (!_dbContext.PokemonSpecies.Any())
            {
                var pokemonSpecies = await GetPokemonSpecies();
                _dbContext.PokemonSpecies.AddRange(pokemonSpecies);
                _dbContext.SaveChanges();
            }
        }

        private async Task<IEnumerable<PokemonSpecies>> GetPokemonSpecies()
        {
            List<PokemonSpecies> pokemonSpecies = new List<PokemonSpecies>();
            using HttpClient client = new();
            var json = await client.GetStringAsync(_allPokemonSpeciesUrl);
            JObject allPokemonSpecies = JObject.Parse(json);
            foreach (var pokemon in allPokemonSpecies["results"]!)
            {
                PokemonSpecies pokemonSpeciesToBeAdded = new PokemonSpecies();
                var pokemonJson = await client.GetStringAsync(pokemon["url"]!.Value<string>());
                JObject pokemonDetails = JObject.Parse(pokemonJson);
                pokemonJson = await client.GetStringAsync($"{_getPokemonDetails}{pokemonDetails["id"]}/");
                JObject pokemonMoreDetails = JObject.Parse(pokemonJson);
                Console.WriteLine(pokemonDetails.ToString());
                pokemonSpeciesToBeAdded.Id = pokemonDetails["id"]!.Value<int>();
                pokemonSpeciesToBeAdded.Name = pokemonDetails["name"]!.Value<string>()!;
                var englishEntry = pokemonMoreDetails["flavor_text_entries"].FirstOrDefault(x => x["language"]?["name"]?.Value<string>() == "en");
                pokemonSpeciesToBeAdded.Description = englishEntry?["flavor_text"]?.Value<string>() ?? "";
                pokemonSpeciesToBeAdded.Height = pokemonDetails["height"]!.Value<int>();
                pokemonSpeciesToBeAdded.Weight = pokemonDetails["weight"]!.Value<int>();
                pokemonSpeciesToBeAdded.BasicHp = pokemonDetails["stats"]![0]!["base_stat"]!.Value<int>();
                pokemonSpeciesToBeAdded.BasicAttack = pokemonDetails["stats"]![1]!["base_stat"]!.Value<int>();
                pokemonSpeciesToBeAdded.BasicDefense = pokemonDetails["stats"]![2]!["base_stat"]!.Value<int>();
                pokemonSpeciesToBeAdded.BasicSpecialAttack = pokemonDetails["stats"]![3]!["base_stat"]!.Value<int>();
                pokemonSpeciesToBeAdded.BasicSpecialDefense = pokemonDetails["stats"]![4]!["base_stat"]!.Value<int>();
                pokemonSpeciesToBeAdded.BasicSpeed = pokemonDetails["stats"]![5]!["base_stat"]!.Value<int>();
                var growthRateUrl = pokemonMoreDetails["growth_rate"]!["url"]!.Value<string>()!;
                pokemonSpeciesToBeAdded.ExperienceSpeed = ExtractIdFromUrl(growthRateUrl);
                pokemonSpeciesToBeAdded.BasicExperienceGivenWhenDefeated = pokemonDetails["base_experience"]!.Value<int>();
                var type1Url = pokemonDetails["types"]![0]!["type"]!["url"]!.Value<string>()!;
                pokemonSpeciesToBeAdded.Type1Id = ExtractIdFromUrl(type1Url);
                pokemonSpeciesToBeAdded.Type2Id = null;
                if (pokemonDetails["types"]!.Count() > 1)
                {
                    var type2Url = pokemonDetails["types"]![1]!["type"]!["url"]!.Value<string>()!;
                    pokemonSpeciesToBeAdded.Type2Id = ExtractIdFromUrl(type2Url);
                }


                pokemonSpecies.Add(pokemonSpeciesToBeAdded);
            }

            return pokemonSpecies;
        }

        private static int ExtractIdFromUrl(string url)
        {
            return int.Parse(url.TrimEnd('/').Split('/').Last());
        }
    }
}
