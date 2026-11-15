using Microsoft.EntityFrameworkCore;
using PokemonGame.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Infrastructure.Persistence.Repositories
{
    public class TypeRepository : ITypeRepository
    {
        private readonly PokemonDbContext _context;

        public TypeRepository(PokemonDbContext context)
        {
            _context = context;
        }
        public async Task<List<Domain.Entities.Type>> GetAllAsync()
        {
            return await _context.Types.ToListAsync();
        }

        public async Task<Domain.Entities.Type?> GetByIdAsync(int id)
        {
            return await _context.Types.FindAsync(id);
        }

        public async Task<Domain.Entities.Type?> GetByNameAsync(string name)
        {
            return await _context.Types.FirstOrDefaultAsync(t => t.Name == name);
        }
    }
}
