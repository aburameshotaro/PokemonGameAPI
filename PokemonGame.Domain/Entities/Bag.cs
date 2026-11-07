using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonGame.Domain.Entities
{
    public class Bag
    {
        public int Amount { get; set; }
        public int TrainerId { get; set; }
        public virtual Trainer Trainer { get; set; }
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }     
    }
}
