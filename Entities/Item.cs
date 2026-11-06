using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonGame.Entities
{
    public class Item
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int CostForBuy { get; set; }
        public int CostForSell { get; set; }
        public string Description { get; set; }
        public virtual HealingItem HealingItem { get; set; }
        public virtual CatchingItem CatchingItem { get; set; }
        public virtual List<Bag> Bags { get; set; }
    }
}
