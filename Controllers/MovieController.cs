using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MundoDeDisney.Core.Entities;
using MundoDeDisney.Core.Interfaces;
using MundoDeDisney.Infraestructure.Data;

namespace MundoDeDisney.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository movieRepository;
        private readonly DisneyDbContext db;

        public MovieController(IMovieRepository movieRepository, DisneyDbContext db)
        {
            this.movieRepository = movieRepository;
            this.db = db;
        }
        [Route("movies")]
        [HttpGet]
        public async Task<ActionResult<Movie>> GetAllMovie()
        {
            var movie = await movieRepository.GetAllMovies();
            var list = movie.Select(x=> new {x.Title,x.CreationDate,x.Image}).ToList();
            return Ok(list);
        }
        [Route("id")]
        [HttpGet]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            try
            {
                var movieId = await movieRepository.GetMovieById(id);
                if (movieId == null)
                {
                    return NotFound();
                }
                var movie = await db.Movies.Select(m => new
                {
                    MovieId = m.MovieID,
                    Titulo = m.Title,
                    Date = m.CreationDate,
                    Calification = m.Calification,
                    Picture = m.Image,
                    Gender = m.Gender.GenderID,
                    m.Gender.Name,
                    m.Gender.Image,
                    CharacterMovie = m.CharactersMovies.Select(cm => new 
                    { cm.CharacterID, cm.Character.Name,cm.Character.Image,cm.Character.Age,cm.Character.Weight,cm.Character.History })

                }).FirstOrDefaultAsync(g => g.MovieId == id);
               
                return Ok(movie);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [Authorize(Roles = "admin")]
        [Route("Create")]
        [HttpPost]
        public async Task<ActionResult<Movie>> CreateMovie(Movie movie)
        {
            try
            {
                if (movie == null)
                {
                    return BadRequest();
                }
                
                var movieResult = await movieRepository.CreateMovie(movie);
                return CreatedAtAction(nameof(CreateMovie), new { Id = movieResult.MovieID }, movieResult);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new movie record");
            }
        }
        [Authorize(Roles = "admin")]
        [Route("Update")]
        [HttpPut]
        public async Task<ActionResult<Movie>> UpdateMovie(int id, Movie movie)
        {
            try
            {
                if (id != movie.MovieID)
                {
                    return BadRequest("movie Id mismatch");
                }
                var movieResult = await movieRepository.UpdateMovie(movie);
                if (movieResult == null)
                {
                    return NotFound($"Movie with Id = {id} not found");
                }
                return await movieRepository.UpdateMovie(movie);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Update movie record");
            }
        }
        [Authorize(Roles = "admin")]
        [Route("Delete")]
        [HttpDelete]
        public async Task<ActionResult<Movie>> DeleteMovie(int id)
        {
            try
            {
                var movieDelete = await movieRepository.GetMovieById(id);
                if (movieDelete == null)
                {
                    return NotFound($"Movie with Id = {id} not found");
                }
                await movieRepository.DeleteMovie(id);
                return Ok($"Movie with Id = {id} deleted");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error delete movie record");
            }
        }
        [Route("OrderA")]
        [HttpGet]
        public async Task<ActionResult<Movie>> OrderAsc(string ASC)
        {
            try
            {
                var date = await movieRepository.GetMovieByDateOrderASC(ASC);
                var list = date.Select(x => new { x.MovieID, x.Title, x.CreationDate, x.Calification, x.Image, x.GenderID });
                return Ok(list);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
           
        }
        [Route("OrderD")]
        [HttpGet]
        public async Task<ActionResult<Movie>> OrderDesc(string DESC)
        {
            try
            {
                var date = await movieRepository.GetMovieByDateOrderDESC(DESC);
                var list = date.Select(x => new { x.MovieID, x.Title, x.CreationDate, x.Calification, x.Image, x.GenderID });
                return Ok(list);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [Route("idGender")]
        [HttpGet]
        public async Task<ActionResult<Movie>> GetMovieIdGender(int idGender)
        {
            try
            {
                var character = await movieRepository.GetMovieByGender(idGender);
                if (character == null)
                {
                    return NotFound();
                }
                var movie = await db.Movies.Select(m => new
                {
                    Gender = m.Gender.GenderID,
                    m.Gender.Name,
                    m.Gender.Image,
                    MovieId = m.MovieID,
                    Titulo = m.Title,
                    Date = m.CreationDate,
                    Calification = m.Calification,
                    Picture = m.Image,
                    CharacterMovie = m.CharactersMovies.Select(cm => new
                    { cm.CharacterID, cm.Character.Name, cm.Character.Image, cm.Character.Age, cm.Character.Weight, cm.Character.History })

                }).FirstOrDefaultAsync(g => g.MovieId == idGender);
                return Ok(movie);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [Route("Tittle")]
        [HttpGet]
        public async Task<ActionResult<Movie>> GetMovieTitle(string title)
        {
            try
            {
                var movie = await db.Movies.Select(m => new
                {
                    Tittle = m.Title,
                    Picture = m.Image,
                    MovieId = m.MovieID,
                    CreationDate = m.CreationDate,
                    Calification = m.Calification,
                    Gender = m.Gender.GenderID,
                    m.Gender.Name,
                    m.Gender.Image,
                    CharacterMovie = m.CharactersMovies.Select(cm => new
                    { cm.CharacterID, cm.Character.Name, cm.Character.Image, cm.Character.Age, cm.Character.Weight, cm.Character.History })

                }).FirstOrDefaultAsync(t => t.Tittle == title);

                return Ok(movie);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
    }
}
