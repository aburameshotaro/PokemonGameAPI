using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonGame.Entities
{
    [PrimaryKey(nameof(PokemonSpeciesId), nameof(AttackId))]
    public class LevelUpAttack
    {
        public int Level { get; set; }
        public int PokemonSpeciesId { get; set; }
        public virtual PokemonSpecies PokemonSpecies { get; set; }
        public int AttackId { get; set; }
        public virtual Attack Attack { get; set; }
    }
}
