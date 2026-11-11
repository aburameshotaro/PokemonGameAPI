using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PokemonGame.Domain.Interfaces;

namespace PokemonGame.Infrastructure.Persistence.Repositories
{

    public class StatusRepository : IStatusRepository
    {
        private readonly PokemonDbContext _context;

        public StatusRepository(PokemonDbContext context) 
        { 
            _context = context;
        }


        public async Task<int?> FindIdByDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
            {
                return null;
            }
            var statuses = await _context.Status.ToListAsync();
            foreach (var status in statuses)
            {
                if (description.ToLower().Contains(status.Verb))
                {
                    return status.Id;
                }
            }
            return null;
        }
        public async Task<int?> FindIdByVerb(string verb)
        {
            var status = await _context.Status.FirstOrDefaultAsync(s => s.Verb == verb);
            if (status == null) { return null; }
            return status.Id;
        }
    }
}
