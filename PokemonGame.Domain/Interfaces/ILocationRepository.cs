using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Domain.Interfaces
{
    public interface ILocationRepository
    {
        Task<Domain.Entities.Location?> GetByIdAsync(int id);
        Task<Domain.Entities.Location?> GetByNameAsync(string name);
        Task<List<Domain.Entities.Location>> GetAllAsync();
    }
}
