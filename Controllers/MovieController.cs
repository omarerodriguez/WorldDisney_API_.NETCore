using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MundoDeDisney.MundoDeDisney.Core.DTOs;
using MundoDeDisney.MundoDeDisney.Core.Entities;
using MundoDeDisney.MundoDeDisney.Core.Exceptions;
using MundoDeDisney.MundoDeDisney.Core.Interfaces;
using MundoDeDisney.MundoDeDisney.Core.QueryFilters;
using MundoDeDisney.MundoDeDisney.Infrastructure.Data;

namespace MundoDeDisney.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MovieController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMovieService _movieService;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MovieController(IUnitOfWork unitOfWork, ApplicationDbContext context,
            IMapper mapper, IMovieService movieService)
        {
            _unitOfWork = unitOfWork;
            _context = context;
            _mapper = mapper;
            _movieService = movieService;
        }
        [HttpGet]
        public async Task<ActionResult<MovieDTO>> GetAll([FromQuery]MovieQueryfilter filters) 
        {
            var movies =  _movieService.GetMovies(filters);
            var movieDTO = _mapper.Map<IEnumerable<MovieDTO>>(movies);
            return Ok(movieDTO);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieShowDetailsDTO>> GetMovie(int id)
        {
            var movie = await _context.Movies.Select(m =>
             new
             {
                 Id=m.Id,
                 Title = m.Title,
                 Genres = m.Genres.OrderByDescending(n=>n.Name).Select(g=>g.Name).ToList(),
                 Characters = m.MoviesCharacters.Select(mc => new
                 {
                     CharacterId = mc.CharacterId,
                     Name = mc.Character.Name
                 }),
             }).FirstOrDefaultAsync(mc=>mc.Id == id) ??  throw new BusinessExceptions($"Character Id:{id} not found");
            return Ok(movie);
        }
        [HttpPost]
        public async Task<ActionResult> Add(MovieCreationDTO movieCreationDTO)
        {
            var movie = _mapper.Map<Movie>(movieCreationDTO);
            if (movie.Genres is not null)
            { foreach (var genre in movie.Genres)
                { _context.Entry(genre).State = EntityState.Unchanged;}
            }
            if(movie.MoviesCharacters is not null)
            {
                for (int i = 0; i < movie.MoviesCharacters.Count; i++)
                {
                    movie.MoviesCharacters[i].Order = i+1;
                }
            }
            await _unitOfWork.MovieRepository.Add(movie);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id,[FromForm] MovieCreationDTO moviePutDTO)
        {
            var movieExist = await _unitOfWork.MovieRepository.GetById(id) ?? throw new BusinessExceptions($"Character Id:{id} not found");
            var movie = _mapper.Map<Movie>(moviePutDTO);
            movie.Id= id;
            await _unitOfWork.MovieRepository.Update(movie);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
           
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _unitOfWork.MovieRepository.GetById(id) ?? throw new BusinessExceptions($"Character Id:{id} not found");
            await _unitOfWork.MovieRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return Ok();

        }
    }
}
