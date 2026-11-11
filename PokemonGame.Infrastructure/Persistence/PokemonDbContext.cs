using Microsoft.EntityFrameworkCore;
using PokemonGame.Domain.Entities;

namespace PokemonGame.Infrastructure.Persistence
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
        public DbSet<Domain.Entities.Type> Types { get;set; }
        public DbSet<TypeAttackBonus> TypesAttackBonuses { get;set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PokemonDbContext).Assembly);
        }


    }
}
