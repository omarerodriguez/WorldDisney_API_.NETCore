using Microsoft.EntityFrameworkCore;
using MundoDeDisney.MundoDeDisney.Core.Entities;
using MundoDeDisney.MundoDeDisney.Core.Interfaces;
using MundoDeDisney.MundoDeDisney.Infrastructure.Data;

namespace MundoDeDisney.MundoDeDisney.Infrastructure.Repositories
{
    public class GenreRepository : BaseRepository<Genre>, IGenreRepository
    {
        public GenreRepository(ApplicationDbContext context) : base(context) { }
        public async Task<IEnumerable<Genre>> GetGenreByName(string name)
        {
            return await _entities.Where(g => g.Name == name).ToListAsync();
        }
    }
}
