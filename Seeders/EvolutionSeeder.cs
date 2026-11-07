using Newtonsoft.Json.Linq;
using PokemonGame.Entities;
using PokemonGame.Repos;

namespace PokemonGame.Seeders
{
    public class EvolutionSeeder : Seeder
    {
        public readonly string _getPokemonDetails = @"https://pokeapi.co/api/v2/pokemon-species?limit=151";
        private IItemRepo _itemRepo;

        public EvolutionSeeder(PokemonDbContext pokemonDb, IItemRepo itemRepo) : base(pokemonDb)
        {
            _itemRepo = itemRepo;
        }

        public async override Task Seed()
        {
            if (!_dbContext.Evolutions.Any())
            {
                var evolutions = await GetEvolutions();
                _dbContext.Evolutions.AddRange(evolutions);
                _dbContext.SaveChanges();
            }
        }

        private async Task<IEnumerable<Evolution>> GetEvolutions()
        {
            var evolutions = new List<Evolution>();

            using HttpClient client = new();

            var speciesLookup = _dbContext.PokemonSpecies
                .ToDictionary(
                    p => p.Name,
                    p => p.Id,
                    StringComparer.OrdinalIgnoreCase);

            var processedChains = new HashSet<int>();

            var json = await client.GetStringAsync(_getPokemonDetails);

            JObject allSpecies = JObject.Parse(json);

            foreach (var species in allSpecies["results"]!)
            {
                var speciesJson = await client.GetStringAsync(species["url"]!.Value<string>()!);

                JObject speciesDetails = JObject.Parse(speciesJson);

                var evolutionChainUrl = speciesDetails["evolution_chain"]?["url"]?.Value<string>();

                if (string.IsNullOrEmpty(evolutionChainUrl))
                    continue;

                int chainId = int.Parse(evolutionChainUrl.TrimEnd('/').Split('/').Last());

                if (!processedChains.Add(chainId))
                    continue;

                var evolutionJson = await client.GetStringAsync(evolutionChainUrl);

                JObject evolutionDetails =
                    JObject.Parse(evolutionJson);

                await ParseEvolutionChain(
                    evolutionDetails["chain"]!,
                    speciesLookup,
                    evolutions);
            }

            return evolutions;
        }

        private async Task ParseEvolutionChain(JToken currentNode, Dictionary<string, int> speciesLookup, List<Evolution> evolutions)
        {
            var fromName =
                currentNode["species"]!["name"]!.Value<string>()!;

            var evolvesTo =
                currentNode["evolves_to"] as JArray;

            if (evolvesTo == null)
                return;

            foreach (var nextNode in evolvesTo)
            {
                var toName =
                    nextNode["species"]!["name"]!.Value<string>()!;

                if (!speciesLookup.ContainsKey(fromName) ||
                    !speciesLookup.ContainsKey(toName))
                {
                    continue;
                }

                var details =
                    nextNode["evolution_details"]?.FirstOrDefault();

                int? level = details?["min_level"]?.Value<int?>();

                Item item = null;
                var itemObject = details["item"];
                if (itemObject.HasValues)
                {
                    var itemUrl = itemObject["url"]?.Value<string>();
                    var itemName = itemObject["name"]!?.Value<string>()!;
                    itemName = itemName.Replace("-", " ").ToLowerInvariant();
                    var itemId = int.Parse(itemUrl.TrimEnd('/').Split('/').Last());
                    item = await _itemRepo.GetByName(itemName);

                    if(item == null)
                    {
                        await _itemRepo.Create(new Item() {Id=itemId, CostForBuy = 1000, Description="Evolution item", Name=itemName, CostForSell=500});
                    }
                }

                bool byTrade = details?["trigger"]?["name"].Value<string>() == "trade";

                evolutions.Add(new Evolution
                {
                    FromPokemonSpeciesId = speciesLookup[fromName],
                    ToPokemonSpeciesId = speciesLookup[toName],
                    EvolutionByTrade = byTrade,
                    Level = level,
                    ItemId = item?.Id
                });

                await ParseEvolutionChain(
                    nextNode,
                    speciesLookup,
                    evolutions);
            }
        }

    }
}
