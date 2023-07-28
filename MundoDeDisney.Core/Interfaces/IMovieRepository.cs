using MundoDeDisney.MundoDeDisney.Core.DTOs;
using MundoDeDisney.MundoDeDisney.Core.Entities;

namespace MundoDeDisney.MundoDeDisney.Core.Interfaces
{
    public interface IMovieRepository:IBaseRepository<Movie>
    {
        IEnumerable<Movie> GetMovies();
    }
}