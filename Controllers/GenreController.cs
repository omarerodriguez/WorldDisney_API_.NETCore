using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MundoDeDisney.MundoDeDisney.Core.DTOs;
using MundoDeDisney.MundoDeDisney.Core.Entities;
using MundoDeDisney.MundoDeDisney.Core.Interfaces;
using MundoDeDisney.MundoDeDisney.Core.Services;
using MundoDeDisney.MundoDeDisney.Infrastructure.Repositories;

namespace MundoDeDisney.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;

        public GenreController(IGenreService genreService, IMapper mapper)
        {
            _genreService = genreService;
            _mapper = mapper;

        }
        [HttpGet]
        public async Task<ActionResult<GenreDTO>> GetAll()
        {
            var genres = _genreService.GetGenres();
            var genreDTO = _mapper.Map<IEnumerable<GenreDTO>>(genres);
            return Ok(genreDTO);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<GenreShowDetailsDTO>>GetGenre(int id)
        {
            var genre = await _genreService.GetGenre(id);
            return _mapper.Map<GenreShowDetailsDTO>(genre);

        }
        [HttpPost]
        public async Task<ActionResult>Add(GenreCreationDTO genreCreationDTO)
        {
            var genre = _mapper.Map<Genre>(genreCreationDTO);
            await _genreService.AddGenre(genre);
            genreCreationDTO = _mapper.Map<GenreCreationDTO>(genre);
            return Ok(genreCreationDTO);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult>Put(int id, GenrePutDTO genrePutDTO)
        {
            var genreExist = _genreService.GetGenre(id);
            if (genreExist == null) { return NotFound($"the genre id: {id} doesn't exist");}
            var genre = _mapper.Map<Genre>(genrePutDTO);
            genre.Id = id;
            await _genreService.Update(genre);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult>Delete(int id)
        {
            var genreDelete = await _genreService.Delete(id);
            return NoContent();
        }
    }
}
