using MundoDeDisney.MundoDeDisney.Core.Entities;

namespace MundoDeDisney.MundoDeDisney.Core.Interfaces
{
    public interface IGenreRepository : IBaseRepository<Genre>
    {
        Task<IEnumerable<Genre>> GetGenreByName(string name);
    }
}