using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MundoDeDisney.Core.DTOs;
using MundoDeDisney.Core.Entities;
using MundoDeDisney.Core.Interfaces;

namespace MundoDeDisney.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IPeliculaRepositorio peliculaRepositorio;
        private readonly IMapper mapper;
        public MovieController(IPeliculaRepositorio peliculaRepositorio, IMapper mapper)
        {
            this.peliculaRepositorio = peliculaRepositorio;
            this.mapper = mapper;
        }
        [Route("/api/movies")]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<PeliculaDto>>> GetAllMovies()
        {
            var moviesList = await peliculaRepositorio.GetAllMovies();
            return Ok(mapper.Map<IReadOnlyList<PeliculaDto>>(moviesList));
        }
        [Route("/ap/movie/id")]
        [HttpGet]
        public async Task<ActionResult<Pelicula>> GetMovie(int id)
        {
            try
            {
                var getMovie = await peliculaRepositorio.GetMovieById(id);
                if (getMovie == null)
                {
                    return NotFound();
                }
                return getMovie;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al recuperar datos de la base de datos");
            }
        }
        [Route("/api/movie/name")]
        [HttpGet]
        public async Task<ActionResult<Pelicula>> GetMovieByMovie(string titulo)
        {
            try
            {
                var getMovie = await peliculaRepositorio.GetMovieByTitulo(titulo);
                if (getMovie == null)
                {
                    return NotFound();
                }
                return getMovie;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al recuperar datos de la base de datos");
            }
        }
        [Route("/api/movie/idGenero")]
        [HttpGet]
        public async Task<ActionResult<Pelicula>> GetMovieByidGenero(int idGenero)
        {
            try
            {
                var getMovie = await peliculaRepositorio.GetMovieByGenero(idGenero);
                if (getMovie == null)
                {
                    return NotFound();
                }
                return getMovie;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al recuperar datos de la base de datos");
            }
        }
        [Route("/api/movie/orderA")]
        [HttpGet]
        public async Task<ActionResult<Pelicula>> GetMovieByOrderASC(string ASC)
        {
            try
            {
                var getMovie = await peliculaRepositorio.GetMovieByDateOrderASC(ASC);
                if (getMovie == null)
                {
                    return NotFound();
                }
                return Ok(getMovie);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al recuperar datos de la base de datos");
            }
        }
        [Route("/api/movie/orderD")]
        [HttpGet]
        public async Task<ActionResult<Pelicula>> GetMovieByOrderDESC(string DESC)
        {
            try
            {
                var getMovie = await peliculaRepositorio.GetMovieByDateOrderDESC(DESC);
                if (getMovie == null)
                {
                    return NotFound();
                }
                return Ok(getMovie);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al recuperar datos de la base de datos");
            }
        }
    }
}
