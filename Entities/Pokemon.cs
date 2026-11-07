using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonGame.Entities
{
    public class Pokemon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int MaxHp { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SpecialAttack { get; set; }
        public int SpecialDefense { get; set; }
        public int Speed { get; set; }
        public int Exp { get; set; }
        public int CurrentHp { get; set; }
        public int? StatusId { get; set; }
        public virtual Status? Status { get; set; }
        public int? TrainerId { get; set; }
        public virtual Trainer? Trainer { get; set;}
        public int? NumberOnTeam { get; set; }
        public int AttackId1 { get; set; }
        public int? AttackId2 { get; set; }
        public int? AttackId3 { get; set; }
        public int? AttackId4 { get;set; }
        public virtual Attack Attack1 { get; set; }
        public virtual Attack? Attack2 { get; set; } 
        public virtual Attack? Attack3 { get; set; }
        public virtual Attack? Attack4 { get;set; } 
        public int PokemonSpeciesId { get; set; }
        public virtual PokemonSpecies PokemonSpecies { get; set; }
    }
}
