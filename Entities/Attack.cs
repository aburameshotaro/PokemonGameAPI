using System.ComponentModel.DataAnnotations;

namespace PokemonGame.Entities
{
    public class Attack
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Power { get; set; }
        public int Accuracy { get; set; }
        public string DamageClass { get; set; }
        public int Priority { get; set; }
        
        public int? InflictsStatusId { get; set; }
        public virtual Status? InflictsStatus { get; set; }
        public int? ChancesToInflictStatus { get; set; }
        public int TypeId { get; set; }
        public virtual Type Type { get; set; }
        public virtual List<LevelUpAttack> LevelUpAttacks { get; set; }
        public virtual List<TmAttack> TmAttacks { get; set; }
        public virtual List<Pokemon> Pokemons1 { get; set; }
        public virtual List<Pokemon> Pokemons2 { get; set; }
        public virtual List<Pokemon> Pokemons3 { get; set; }
        public virtual List<Pokemon> Pokemons4 { get; set; }
    }
}
