using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonGame.Entities
{
    public class Status
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Verb { get; set; }
        public virtual List<Pokemon> Pokemons { get; set; }
        public virtual List<Attack> Attacks { get; set; }
    }
}