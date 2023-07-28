using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MundoDeDisney.MundoDeDisney.Core.DTOs;
using MundoDeDisney.MundoDeDisney.Core.Entities;
using MundoDeDisney.MundoDeDisney.Core.Interfaces;
using MundoDeDisney.MundoDeDisney.Infrastructure.Data;

namespace MundoDeDisney.MundoDeDisney.Infrastructure.Repositories
{
    public class MovieRepository :BaseRepository<Movie>, IMovieRepository
    {
        private readonly ApplicationDbContext _context;
        public MovieRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
        public IEnumerable<Movie> GetMovies() => _context.Movies
        .Include(p => p.Genres)
        .Include(p => p.MoviesCharacters)
        .AsEnumerable();

        //public async Task  GetByIdWithDetail(int id)
        //{
        //      await _context.Movies.Select(m =>
        //     new
        //     {
        //         Id = m.Id,
        //         Title = m.Title,
        //         Genres = m.Genres.OrderByDescending(n => n.Name).Select(g => g.Name).ToList(),
        //         Characters = m.MoviesCharacters.Select(mc => new
        //         {
        //             CharacterId = mc.CharacterId,
        //             Name = mc.Character.Name
        //         }),
        //     }).FirstOrDefaultAsync(mc => mc.Id == id);
        //}
    }
}
