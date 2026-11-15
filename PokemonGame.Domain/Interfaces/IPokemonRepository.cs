using PokemonGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Domain.Interfaces
{
    public interface IPokemonRepository
    {
        Task<Pokemon?> GetByIdAsync(int id);
        Task<List<Pokemon>> GetAllAsync();
        Task<List<Pokemon>> GetByTrainerIdAsync(int trainerId);
        Task<List<Pokemon>> GetTeamAsync(int trainerId);
        Task<Pokemon?> GetFirstAlivePokemonAsync(int trainerId);
        Task<Pokemon> AddAsync(Pokemon pokemon);
        void Update(Pokemon pokemon);
        void Delete(Pokemon pokemon);
    }
}
