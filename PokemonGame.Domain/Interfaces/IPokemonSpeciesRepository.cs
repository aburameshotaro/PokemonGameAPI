using PokemonGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Domain.Interfaces
{
    public interface IPokemonSpeciesRepository
    {
        Task<PokemonSpecies?> GetByIdAsync(int id);
        Task<PokemonSpecies?> GetByNameAsync(string name);
        Task<List<PokemonSpecies>> GetAllAsync();
        Task<List<PokemonSpecies>> GetAllStartersAsync();
    }
}
