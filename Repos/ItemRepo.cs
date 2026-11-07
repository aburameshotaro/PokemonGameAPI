using Microsoft.EntityFrameworkCore;
using PokemonGame.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Repos
{

    public interface IItemRepo
    {
        Task<Item?> GetByName(string name);
        Task<Item?> Create(Item item);
    }

    public class ItemRepo : IItemRepo
    {
        private readonly PokemonDbContext _context;

        public ItemRepo(PokemonDbContext context)
        {
            _context = context;
        }

        public async Task<Item?> GetByName(string name)
        {
            return await _context.Items.FirstOrDefaultAsync(i => i.Name == name); ;
        }

        public async Task<Item?> Create(Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

    }
}
