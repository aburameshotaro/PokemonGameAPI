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

    public class ItemRepository : IItemRepository
    {
        private readonly PokemonDbContext _context;

        public ItemRepository(PokemonDbContext context)
        {
            _context = context;
        }

        public async Task<Item?> GetByNameAsync(string name)
        {
            return await _context.Items.FirstOrDefaultAsync(i => i.Name == name); ;
        }

        public async Task<Item?> AddAsync(Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

    }
}
