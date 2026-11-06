using Microsoft.EntityFrameworkCore;

namespace PokemonGame.Entities
{
    public class PokemonDbContext : DbContext
    {
        public PokemonDbContext(DbContextOptions<PokemonDbContext> options) : base(options) { }
        public DbSet<Pokemon> Pokemons { get;set; }
        public DbSet<Attack> Attacks { get;set; }
        public DbSet<Bag> Bags { get;set; }
        public DbSet<Item> Items { get;set; }
        public DbSet<CatchingItem> CatchingItems { get; set; }
        public DbSet<HealingItem> HealingItems { get;set; }
        public DbSet<Evolution> Evolutions { get;set; }
        public DbSet<LevelUpAttack> LevelUpAttacks { get;set;}
        public DbSet<Location> Locations { get;set; }
        public DbSet<PokemonSpecies> PokemonSpecies { get;set; }
        public DbSet<ProbabilityAppearance> PokemonProbabilityAppearances { get;set; }
        public DbSet<Status> Status { get;set; }
        public DbSet<TmAttack> TMAttacks { get;set; }
        public DbSet<Trainer> Trainers { get;set; }
        public DbSet<Type> Types { get;set; }
        public DbSet<TypeAttackBonus> TypesAttackBonuses { get;set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<PokemonSpecies>()
                .HasOne(p => p.Type1)
                .WithMany(p => p.PokemonSpeciesType1)
                .HasForeignKey(p => p.Type1)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PokemonSpecies>()
                .HasOne(p => p.Type2)
                .WithMany(p => p.PokemonSpeciesType2)
                .HasForeignKey(p => p.Type2)
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<Evolution>()
                .HasOne(e => e.FromPokemonSpecies)
                .WithMany(e => e.EvolvesFrom)
                .HasForeignKey(e => e.FromPokemonSpeciesId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Evolution>()
                .HasOne(e => e.ToPokemonSpecies)
                .WithMany(e => e.EvolvesInto)
                .HasForeignKey(e => e.ToPokemonSpeciesId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TypeAttackBonus>()
                .HasOne(t => t.DefenderType)
                .WithMany(t => t.TypeDefendBonuses)
                .HasForeignKey(t => t.DefenderTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TypeAttackBonus>()
                .HasOne(t => t.AttackType)
                .WithMany(t => t.TypeAttackBonuses)
                .HasForeignKey(t => t.AttackTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Pokemon>()
                .HasOne(p => p.Attack1)
                .WithMany(p => p.Pokemons)
                .HasForeignKey(p => p.AttackId1)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Pokemon>()
                .HasOne(p => p.Attack2)
                .WithMany(p => p.Pokemons)
                .HasForeignKey(p => p.AttackId2)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Pokemon>()
                .HasOne(p => p.Attack3)
                .WithMany(p => p.Pokemons)
                .HasForeignKey(p => p.AttackId3)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Pokemon>()
                .HasOne(p => p.Attack4)
                .WithMany(p => p.Pokemons)
                .HasForeignKey(p => p.AttackId4)
                .OnDelete(DeleteBehavior.NoAction);
        }


    }
}
