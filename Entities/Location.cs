namespace PokemonGame.Entities
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<ProbabilityAppearance> ProbabilityAppearances { get; set; }
    }
}
