using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Domain.Interfaces
{
    public interface IStatusRepository
    {
        Task<int?> FindIdByDescription(string description);
        Task<int?> FindIdByVerb(string verb);
    }
}
