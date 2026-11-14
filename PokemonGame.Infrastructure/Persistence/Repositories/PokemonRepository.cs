using Microsoft.EntityFrameworkCore;
using PokemonGame.Domain.Entities;
using PokemonGame.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Infrastructure.Persistence.Repositories
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly PokemonDbContext _context;

        public PokemonRepository(PokemonDbContext context)
        {
            _context = context;
        }
        public async Task<Pokemon> AddAsync(Pokemon pokemon)
        {
            await _context.Pokemons.AddAsync(pokemon);
            return pokemon;
        }

        public void Delete(Pokemon pokemon)
        {
            _context.Pokemons.Remove(pokemon);
        }

        public async Task<List<Pokemon>> GetAllAsync()
        {
            return await _context.Pokemons.ToListAsync();
        }

        public async Task<Pokemon?> GetByIdAsync(int id)
        {
            return await _context.Pokemons.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Pokemon>> GetByTrainerIdAsync(int trainerId)
        {
            return await _context.Pokemons.Where(p => p.TrainerId == trainerId).ToListAsync();
        }

        public void Update(Pokemon pokemon)
        {
            _context.Pokemons.Update(pokemon);
        }
    }
}
