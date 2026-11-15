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
    public class AttackRepository : IAttackRepository
    {
        private readonly PokemonDbContext _context;

        public AttackRepository(PokemonDbContext context)
        {
            _context = context;
        }

        public async Task<Attack?> GetByIdAsync(int id)
        {
            return await _context.Attacks.FindAsync(id);
        }

        public async Task<Attack?> GetByNameAsync(string name)
        {
            return await _context.Attacks.FirstOrDefaultAsync(a => a.Name == name);
        }

        public async Task<List<Attack>> GetAllAsync()
        {
            return await _context.Attacks.ToListAsync();
        }

        
    }
}
