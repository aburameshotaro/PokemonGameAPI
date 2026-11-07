using Microsoft.EntityFrameworkCore;

namespace PokemonGame.Entities
{
    public class CatchingItem
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }
        public int CatchingRate { get; set; }
    }
}
