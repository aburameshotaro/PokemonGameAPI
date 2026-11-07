using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonGame.Entities
{
    [PrimaryKey(nameof(PokemonSpeciesId), nameof(AttackId))]
    public class TmAttack
    {
        [Key]
        [Column(Order = 1)]
        public int PokemonSpeciesId { get; set; }
        public virtual PokemonSpecies PokemonSpecies { get; set;}
        [Key]
        [Column(Order = 2)]
        public int AttackId { get; set; }
        public virtual Attack Attack { get; set; }
    }
}
