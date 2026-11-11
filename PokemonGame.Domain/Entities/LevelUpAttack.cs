namespace PokemonGame.Domain.Entities
{
    public class LevelUpAttack
    {
        public int Level { get; set; }
        public int PokemonSpeciesId { get; set; }
        public virtual PokemonSpecies PokemonSpecies { get; set; }
        public int AttackId { get; set; }
        public virtual Attack Attack { get; set; }
    }
}
