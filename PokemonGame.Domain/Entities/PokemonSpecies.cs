using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonGame.Domain.Entities
{
    public class PokemonSpecies
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public int BasicHp { get; set; }
        public int BasicAttack { get; set; }
        public int BasicDefense { get; set; }
        public int BasicSpecialAttack { get; set; }
        public int BasicSpecialDefense { get;set; }
        public int BasicSpeed { get; set; }
        public int ExperienceSpeed { get; set; }
        public int BasicExperienceGivenWhenDefeated { get; set; }
        public virtual List<LevelUpAttack> LevelUpAttacks { get; set; }
        public virtual List<TmAttack> TmAttacks { get; set; }
        public int Type1Id { get; set; }
        public int? Type2Id { get; set; }
        public virtual Type Type1 { get; set; }
        public virtual Type? Type2 { get; set; }
        public List<ProbabilityAppearance> ProbabilityAppearances { get; set; }
        public virtual List<Evolution> EvolvesFrom { get; set; }
        public virtual List<Evolution> EvolvesInto { get; set; }
        public virtual List<Pokemon> Pokemons { get; set; }
    }
}
