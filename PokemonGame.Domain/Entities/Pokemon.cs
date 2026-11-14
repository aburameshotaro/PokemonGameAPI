using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonGame.Domain.Entities
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
        public int CurrentHp { get; private set; }
        public int? StatusId { get; set; }
        public virtual Status? Status { get; set; }
        public int? TrainerId { get; set; }
        public virtual Trainer? Trainer { get; set; }
        public int? NumberOnTeam { get; set; }
        public int AttackId1 { get; set; }
        public int? AttackId2 { get; set; }
        public int? AttackId3 { get; set; }
        public int? AttackId4 { get; set; }
        public virtual Attack Attack1 { get; set; }
        public virtual Attack? Attack2 { get; set; }
        public virtual Attack? Attack3 { get; set; }
        public virtual Attack? Attack4 { get; set; }
        public int PokemonSpeciesId { get; set; }
        public virtual PokemonSpecies PokemonSpecies { get; set; }

        public bool IsFainted => CurrentHp == 0;

        private Pokemon()
        {
            // For EF Core
        }

        public Pokemon(
        PokemonSpecies species,
        int level, Attack attack1, Attack attack2 = null, Attack attack3 = null, Attack attack4 = null, string name="")
        {
            ArgumentNullException.ThrowIfNull(species);
            ArgumentNullException.ThrowIfNull(attack1);

            if (level < 1 || level > 100)
                throw new ArgumentOutOfRangeException(nameof(level));

            PokemonSpecies = species;
            PokemonSpeciesId = species.Id;

            Name = string.IsNullOrWhiteSpace(name) ? species.Name : name;
            Level = level;

            MaxHp = species.BasicHp;
            CurrentHp = MaxHp;
            Attack = species.BasicAttack;
            Defense = species.BasicDefense;
            SpecialAttack = species.BasicSpecialAttack;
            SpecialDefense = species.BasicSpecialDefense;
            Speed = species.BasicSpeed;
            AttackId1 = attack1.Id;
            Attack1 = attack1;
            AttackId2 = attack2?.Id;
            Attack2 = attack2;
            AttackId3 = attack3?.Id;
            Attack3 = attack3;
            AttackId4 = attack4?.Id;
            Attack4 = attack4;
        }

        public void Heal(int hpRestored)
        {
            if (hpRestored == 0) return;
            if (hpRestored < 0)
            {
                throw new ArgumentException("hpRestored cannot be negative");
            }
            if (IsFainted)
            {
                throw new InvalidOperationException("Cannot heal a fainted Pokemon");
            }
            CurrentHp += Math.Min(hpRestored, MaxHp - CurrentHp);
        }

        public void TakeDamage(int damage)
        {
            if (damage < 0)
            {
                throw new ArgumentException("Damage cannot be negative");
            }
            CurrentHp = Math.Max(CurrentHp - damage, 0);
        }

        public void Revive(int hpRestored)
        {
            if (!IsFainted)
            {
                throw new InvalidOperationException("Cannot revive a Pokemon that is not fainted");
            }
            CurrentHp = Math.Min(hpRestored, MaxHp);
        }

        public void GainExp(int expPoints)
        {
            if (expPoints < 0)
            {
                throw new ArgumentException("Exp points cannot be negative");
            }
            Exp += expPoints;
        }

    }
}