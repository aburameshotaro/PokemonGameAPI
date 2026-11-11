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
        Task<List<PokemonSpecies>> GetAllAsync();
    }
}
