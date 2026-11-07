using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonGame.Entities
{
    public class Evolution
    {
        public int Id { get; set; }
        public int? Level { get; set; }
        public int? ItemId { get; set; }
        public bool EvolutionByTrade { get; set; }
        public virtual Item? Item { get; set; }
        public int FromPokemonSpeciesId { get; set; }
        public int ToPokemonSpeciesId { get; set; }
        public virtual PokemonSpecies FromPokemonSpecies { get; set;}
        public virtual PokemonSpecies ToPokemonSpecies { get; set; }
    }
}
