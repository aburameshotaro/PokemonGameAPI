using PokemonGame.Domain.Entities;

namespace PokemonGame.Infrastructure.Persistence.Seeders
{
    public abstract class Seeder
    {
        protected readonly PokemonDbContext _dbContext;
        public Seeder(PokemonDbContext pokemonDb) 
        {
            _dbContext = pokemonDb;
        }
        public abstract Task Seed();
    }
}
