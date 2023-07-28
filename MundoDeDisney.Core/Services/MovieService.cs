using AutoMapper;
using MundoDeDisney.MundoDeDisney.Core.DTOs;
using MundoDeDisney.MundoDeDisney.Core.Entities;
using MundoDeDisney.MundoDeDisney.Core.Interfaces;
using MundoDeDisney.MundoDeDisney.Core.QueryFilters;

namespace MundoDeDisney.MundoDeDisney.Core.Services
{
    public class MovieService : IMovieService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMovieRepository _movieRepository;

        public MovieService(IUnitOfWork unitOfWork, IMovieRepository movieRepository)
        {
            _unitOfWork = unitOfWork;
            _movieRepository = movieRepository;
        }

        public IEnumerable<Movie> GetMovies(MovieQueryfilter filters)
        {
            var movie = _movieRepository.GetMovies();
            if(filters.Title != null) { movie=movie.Where(t=>t.Title.ToLower().Contains(filters.Title.ToLower()));}
            if (filters.GenerID != null) { movie=movie.Where(g=>g.Genres.Any(g=>g.Id==filters.GenerID));}
            if (filters.Order != null)
            {
                if (filters.Order.ToLower() == "asc") {movie = movie.OrderBy(x => x.Title);}
                if (filters.Order.ToLower() == "desc") { movie=movie.OrderByDescending(x => x.Title); }
            }
            return movie;
        }
    }
}
