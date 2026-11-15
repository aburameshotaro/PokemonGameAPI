using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Domain.Interfaces
{
    public interface ITypeRepository
    {
        Task<Domain.Entities.Type?> GetByIdAsync(int id);
        Task<Domain.Entities.Type?> GetByNameAsync(string name);
        Task<List<Domain.Entities.Type>> GetAllAsync();
    }
}
