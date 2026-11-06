using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using PokemonGame.Entities;
using PokemonGame.Repos;

namespace PokemonGame.Seeders
{
    public class PokemonSpeciesSeeder : Seeder
    {
        public readonly string _allPokemonSpeciesUrl = "https://pokeapi.co/api/v2/pokemon?limit=151";
        public PokemonSpeciesSeeder(PokemonDbContext pokemonDb) : base(pokemonDb)
        {
        }

        public async override Task Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.PokemonSpecies.Any())
                {
                    var pokemonSpecies = await GetPokemonSpecies();
                    _dbContext.PokemonSpecies.AddRange(pokemonSpecies);
                    _dbContext.SaveChanges();
                }
            }
        }

        private async Task<IEnumerable<PokemonSpecies>> GetPokemonSpecies()
        {
            List<PokemonSpecies> pokemonSpecies = new List<PokemonSpecies>();
            using HttpClient client = new();
            var json = await client.GetStringAsync(_allPokemonSpeciesUrl);
            dynamic allPokemonSpecies = JObject.Parse(json);
            foreach (var pokemon in allPokemonSpecies.results)
            {
                PokemonSpecies pokemonSpeciesToBeAdded = new PokemonSpecies();
                var pokemonJson = await client.GetStringAsync(pokemon.url.ToString());
                dynamic pokemonDetails = JObject.Parse(pokemonJson);
                pokemonJson = await client.GetStringAsync(pokemonDetails.forms[0].url.ToString());
                dynamic pokemonMoreDetails = JObject.Parse(pokemonJson);
                Console.WriteLine(pokemonDetails.ToString());
                pokemonSpeciesToBeAdded.Id = pokemonDetails.id;
                pokemonSpeciesToBeAdded.Name = pokemonDetails.name;
                pokemonSpeciesToBeAdded.Description = pokemonMoreDetails.flavor_text_entries[0].flavor_text;
                pokemonSpeciesToBeAdded.Height = pokemonDetails.height;
                pokemonSpeciesToBeAdded.Weight = pokemonDetails.weight;
                pokemonSpeciesToBeAdded.BasicHp = pokemonDetails.stats[0].base_stat;
                pokemonSpeciesToBeAdded.BasicAttack = pokemonDetails.stats[1].base_stat;
                pokemonSpeciesToBeAdded.BasicDefense = pokemonDetails.stats[2].base_stat;
                pokemonSpeciesToBeAdded.BasicSpecialAttack = pokemonDetails.stats[3].base_stat;
                pokemonSpeciesToBeAdded.BasicSpecialDefense = pokemonDetails.stats[4].base_stat;
                pokemonSpeciesToBeAdded.BasicSpeed = pokemonDetails.stats[5].base_stat;
                pokemonSpeciesToBeAdded.ExperienceSpeed = int.Parse(pokemonMoreDetails.growth_rate.url.ToString().Split("/")[6]);
                pokemonSpeciesToBeAdded.BasicExperienceGivenWhenDefeated = pokemonDetails.base_experience;
                pokemonSpeciesToBeAdded.Type1Id = int.Parse(pokemonDetails.types[0].type.url.ToString().Split("/")[6]);
                pokemonSpeciesToBeAdded.Type2Id = pokemonDetails.types.Length > 1 ? int.Parse(pokemonDetails.types[1].type.url.ToString().Split("/")[6]) : null;


                pokemonSpecies.Add(pokemonSpeciesToBeAdded);
            }

            return pokemonSpecies;
        }
    }
}
