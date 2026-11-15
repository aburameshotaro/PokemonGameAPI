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
    public class LocationRepository : ILocationRepository
    {
        private readonly PokemonDbContext _context;

        public LocationRepository(PokemonDbContext context)
        {
            _context = context;
        }
        public async Task<List<Location>> GetAllAsync()
        {
            return await _context.Locations.ToListAsync();
        }

        public async Task<Location?> GetByIdAsync(int id)
        {
            return await _context.Locations.FindAsync(id);
        }

        public async Task<Location?> GetByNameAsync(string name)
        {
            return await _context.Locations.FirstOrDefaultAsync(l => l.Name == name);
        }
    }
}
