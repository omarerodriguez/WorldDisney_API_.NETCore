using Microsoft.EntityFrameworkCore;
using MundoDeDisney.MundoDeDisney.Core.Entities;
using MundoDeDisney.MundoDeDisney.Core.Interfaces;
using MundoDeDisney.MundoDeDisney.Infrastructure.Data;
using SendGrid.Helpers.Mail;

namespace MundoDeDisney.MundoDeDisney.Infrastructure.Repositories
{
    public class CharacterRepository : BaseRepository<Character>, ICharacterRepository
    {
        private readonly ApplicationDbContext _context;

        public CharacterRepository(ApplicationDbContext context) : base(context)
        {
          _context = context;
        }
        public async Task<IEnumerable<Character>> GetCharacterByName(string name)
        {
            return await _entities.Where(c => c.Name == name).ToListAsync();
        }
        public IEnumerable<Character> GetCharacters() => _context.Characters
             .Include(p => p.MoviesCharacters)
              .AsEnumerable();

    }
}
