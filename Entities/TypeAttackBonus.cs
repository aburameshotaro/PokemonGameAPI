using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PokemonGame.Entities
{
    [PrimaryKey(nameof(AttackTypeId), nameof(DefenderTypeId))]
    public class TypeAttackBonus
    {
        public int Multiplier { get; set; }
        [Key]
        [Column(Order = 1)]
        public int AttackTypeId { get; set; }
        public virtual Type AttackType { get; set; }
        [Key]
        [Column(Order = 2)]
        public int DefenderTypeId { get; set; }
        public virtual Type DefenderType { get; set;  }
    }
}
