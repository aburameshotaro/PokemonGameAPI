using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonGame.Entities
{
    public class Trainer
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool Admin { get; set; }
        public bool Bot { get; set; }
        public virtual List<Bag> Bags { get; set; }
        public virtual List<Pokemon> Pokemons { get; set; }
        
    }
}
