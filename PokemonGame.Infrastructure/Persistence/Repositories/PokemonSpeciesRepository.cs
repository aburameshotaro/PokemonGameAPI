using Microsoft.EntityFrameworkCore;
using PokemonGame.Domain.Entities;
using PokemonGame.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Infrastructure.Persistence.Repositories
{
    public class PokemonSpeciesRepository : IPokemonSpeciesRepository
    {
        private readonly PokemonDbContext _context;
        public PokemonSpeciesRepository(PokemonDbContext pokemonDbContext)
        {
            _context = pokemonDbContext;
        }

        public async Task<List<PokemonSpecies>> GetAllAsync()
        {
            return await _context.PokemonSpecies.ToListAsync();
        }
    }

}
