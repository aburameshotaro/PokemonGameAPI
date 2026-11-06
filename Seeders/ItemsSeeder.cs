using Newtonsoft.Json;
using PokemonGame.Entities;

namespace PokemonGame.Seeders
{
    public class ItemsSeeder : Seeder
    {
        private readonly string _itemsJsonPath = Path.Combine(AppContext.BaseDirectory, "Data", "items.json");
        private readonly string _catchingItemsJsonPath = Path.Combine(AppContext.BaseDirectory, "Data", "catching_items.json");
        private readonly string _healingItemsJsonPath = Path.Combine(AppContext.BaseDirectory, "Data", "healing_items.json");
        public ItemsSeeder(PokemonDbContext pokemonDb) : base(pokemonDb)
        {
        }

        public override Task Seed()
        {
            if (!_dbContext.Items.Any())
            {
                (var items, var catchingItems, var healingItems) = GetItems();
                _dbContext.Items.AddRange(items);
                _dbContext.CatchingItems.AddRange(catchingItems);
                _dbContext.HealingItems.AddRange(healingItems);
                _dbContext.SaveChanges();
            }
            return Task.CompletedTask;

        }

        private (IEnumerable<Item>, IEnumerable<CatchingItem>, IEnumerable<HealingItem>) GetItems()
        {
            string jsonItems = File.ReadAllText(_itemsJsonPath);
            var items = JsonConvert.DeserializeObject<List<Item>>(jsonItems) ?? new List<Item>();
            string jsonCatchingItems = File.ReadAllText(_catchingItemsJsonPath);
            var catchingItems = JsonConvert.DeserializeObject<List<CatchingItem>>(jsonCatchingItems);
            string jsonHealingItems = File.ReadAllText(_healingItemsJsonPath);
            var healingItems = JsonConvert.DeserializeObject<List<HealingItem>>(jsonHealingItems);
            return (items, catchingItems, healingItems);
        }
    }
}
