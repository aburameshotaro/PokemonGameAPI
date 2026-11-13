using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Domain.Interfaces
{
    public interface IStatusRepository
    {
        Task<int?> GetIdByDescriptionAsync(string description);
        Task<int?> GetIdByVerbAsync(string verb);
    }
}
