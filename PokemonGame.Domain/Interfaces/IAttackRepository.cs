using PokemonGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Domain.Interfaces
{
    public interface IAttackRepository
    {
        Task<Attack?> GetByIdAsync(int id);
        Task<Attack?> GetByNameAsync(string name);
        Task<List<Attack>> GetAllAsync();
        Task<List<Attack>> GetMovesForLevelAsync(int pokemonSpeciesId, int level);
    }
}
