namespace PokemonGame.Domain.Entities
{
    public class TmAttack
    {
        public int PokemonSpeciesId { get; set; }
        public virtual PokemonSpecies PokemonSpecies { get; set;}
        public int AttackId { get; set; }
        public virtual Attack Attack { get; set; }
    }
}
