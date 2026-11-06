using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonGame.Entities
{
    public class Type
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("TypeId1")]
        public virtual List<PokemonSpecies> PokemonSpeciesType1 { get; set; }
        [ForeignKey("TypeId2")]
        public virtual List<PokemonSpecies> PokemonSpeciesType2 { get; set; }
        public virtual List<Attack> Attacks { get; set; }
        public virtual List<TypeAttackBonus> TypeAttackBonuses { get; set; }
        public virtual List<TypeAttackBonus> TypeDefendBonuses { get; set; }
    }
}
