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
    public class TrainerRepository : ITrainerRepository
    {
        private readonly PokemonDbContext _context;

        public TrainerRepository(PokemonDbContext context)
        {
            _context = context;
        }
        public async Task<Trainer> AddAsync(Trainer trainer)
        {
            await _context.Trainers.AddAsync(trainer);
            return trainer;
        }

        public void Delete(Trainer trainer)
        {
            _context.Trainers.Remove(trainer);
        }

        public async Task<List<Trainer>> GetAllAsync()
        {
            return await _context.Trainers.ToListAsync();
        }

        public async Task<Trainer?> GetByIdAsync(int id)
        {
            return await _context.Trainers.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Trainer?> GetByIdWithTeamAsync(int id)
        {
            return await _context.Trainers
                .Include(t => t.Pokemons.Where(p => p.NumberOnTeam != null).OrderBy(p => p.NumberOnTeam))
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Trainer?> GetByUsernameAsync(string username)
        {
            return await _context.Trainers.FirstOrDefaultAsync(p => p.Username == username);
        }

        public void Update(Trainer trainer)
        {
            _context.Trainers.Update(trainer);
        }
    }
}
