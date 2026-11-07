using Microsoft.EntityFrameworkCore;

namespace PokemonGame.Entities
{
    public class HealingItem
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }
        public int HpHealed { get; set; }
        public bool StatusHealed { get; set; }
        public bool Revive { get; set; }
    }
}
