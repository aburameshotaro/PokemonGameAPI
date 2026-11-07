namespace PokemonGame.Domain.Entities
{
    public class TypeAttackBonus
    {
        public int Multiplier { get; set; }
        public int AttackTypeId { get; set; }
        public virtual Type AttackType { get; set; }
        public int DefenderTypeId { get; set; }
        public virtual Type DefenderType { get; set;  }
    }
}
