namespace PokemonGame.Domain.Entities
{
    public class Type
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<PokemonSpecies> PokemonSpeciesType1 { get; set; }
        public virtual List<PokemonSpecies> PokemonSpeciesType2 { get; set; }
        public virtual List<Attack> Attacks { get; set; }
        public virtual List<TypeAttackBonus> TypeAttackBonuses { get; set; }
        public virtual List<TypeAttackBonus> TypeDefendBonuses { get; set; }
    }
}
