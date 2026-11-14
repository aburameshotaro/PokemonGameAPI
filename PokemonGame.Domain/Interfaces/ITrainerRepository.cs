using PokemonGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Domain.Interfaces
{
    public interface ITrainerRepository
    {
        Task<Trainer?> GetByIdAsync(int id);
        Task<Trainer?> GetByUsernameAsync(string username);
        Task<Trainer?> GetByIdWithTeamAsync(int id);
        Task<List<Trainer>> GetAllAsync();
        Task<Trainer> AddAsync(Trainer trainer);
        void Update(Trainer trainer);
        void Delete(Trainer trainer);
    }
}
