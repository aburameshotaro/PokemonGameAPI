using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PokemonGame.Entities
{
    [PrimaryKey(nameof(PokemonSpeciesId), nameof(LocationId))]
    public class ProbabilityAppearance
    {
        public int Probability { get; set; }
        [Key]
        [Column(Order = 1)]
        public int PokemonSpeciesId { get; set; }
        public virtual PokemonSpecies PokemonSpecies { get; set; }
        [Key]
        [Column(Order = 2)]
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }
    }
}
