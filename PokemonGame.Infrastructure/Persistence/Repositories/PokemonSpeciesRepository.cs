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
    public class PokemonSpeciesRepository : IPokemonSpeciesRepository
    {
        private readonly PokemonDbContext _context;
        public PokemonSpeciesRepository(PokemonDbContext pokemonDbContext)
        {
            _context = pokemonDbContext;
        }

        public async Task<PokemonSpecies?> GetByIdAsync(int id)
        {
            return await _context.PokemonSpecies.FindAsync(id);
        }

        public async Task<PokemonSpecies?> GetByNameAsync(string name)
        {
            return await _context.PokemonSpecies.FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task<List<PokemonSpecies>> GetAllAsync()
        {
            return await _context.PokemonSpecies.Include(p => p.Type1).Include(p => p.Type2).ToListAsync();
        }

        public async Task<List<PokemonSpecies>> GetAllStartersAsync()
        {
            return await _context.PokemonSpecies.Where(p => p.IsStarter).Include(p => p.Type1).Include(p => p.Type2).ToListAsync();
        }
    }

}
