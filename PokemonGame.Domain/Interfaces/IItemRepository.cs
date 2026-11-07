using PokemonGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Domain.Interfaces
{
    public interface IItemRepository
    {
        Task<Item?> GetByName(string name);
        Task<Item?> Create(Item item);
    }
}
