using MundoDeDisney.MundoDeDisney.Core.DTOs;
using MundoDeDisney.MundoDeDisney.Core.Entities;
using MundoDeDisney.MundoDeDisney.Core.QueryFilters;

namespace MundoDeDisney.MundoDeDisney.Core.Interfaces
{
    public interface IMovieService
    {
        IEnumerable<Movie> GetMovies(MovieQueryfilter filters);
        //Task InsertMovies(MoviesForCreationDTO movie);
        //Task<bool> UpdateMovies(int id, MoviesForUpdateDTO movie);
        //Task<bool> DeleteMovies(int id);
    }
}