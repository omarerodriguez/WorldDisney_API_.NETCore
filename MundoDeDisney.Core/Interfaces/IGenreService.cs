using MundoDeDisney.MundoDeDisney.Core.Entities;

namespace MundoDeDisney.MundoDeDisney.Core.Interfaces
{
    public interface IGenreService
    {
        IEnumerable<Genre> GetGenres();
        Task<Genre> GetGenre(int id);
        Task AddGenre(Genre genre);
        Task<bool> Update(Genre genre);
        Task<bool> Delete(int id);
    }
}