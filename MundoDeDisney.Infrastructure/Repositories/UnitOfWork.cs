using MundoDeDisney.MundoDeDisney.Core.Entities;
using MundoDeDisney.MundoDeDisney.Core.Interfaces;
using MundoDeDisney.MundoDeDisney.Infrastructure.Data;

namespace MundoDeDisney.MundoDeDisney.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        IGenreRepository genreRepository { get; }
        IBaseRepository<Movie> movieRepository { get; }
        ICharacterRepository characterRepository { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IGenreRepository GenreRepository => genreRepository ?? new GenreRepository(_context);

        public IBaseRepository<Movie> MovieRepository => movieRepository ?? new BaseRepository<Movie>(_context);

        public ICharacterRepository CharacterRepository => characterRepository ?? new CharacterRepository(_context);

        public void Dispose()
        {
            if(_context!= null) { _context.Dispose();}
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}
