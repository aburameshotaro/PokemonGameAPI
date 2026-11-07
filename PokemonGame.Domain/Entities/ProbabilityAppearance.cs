namespace PokemonGame.Domain.Entities
{
    public class ProbabilityAppearance
    {
        public int Probability { get; set; }
        public int PokemonSpeciesId { get; set; }
        public virtual PokemonSpecies PokemonSpecies { get; set; }
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }
    }
}
