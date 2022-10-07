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
    public class CharacterController : ControllerBase
    {
        private readonly IPersonajesRepositorio personajesRepositorio;
        private readonly IMapper mapper;
        public CharacterController(IPersonajesRepositorio personajesRepositorio, IMapper mapper)
        {
            this.personajesRepositorio = personajesRepositorio;
            this.mapper = mapper;
        }
        [Route("/api/Characters")]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ListadoPjDto>>> GetAllCharacters()
        {
            var personajeList = await personajesRepositorio.GetAllCharacters();
            return Ok(mapper.Map<IReadOnlyList<ListadoPjDto>>(personajeList));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonajesDto>> GetCharacter(int id)
        {
            try
            {
                var getCharacter = await personajesRepositorio.GetCharacterById(id);
                if (getCharacter == null)
                {
                    return NotFound();
                }
                return mapper.Map<Personaje,PersonajesDto>(getCharacter);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);//StatusCode(StatusCodes.Status500InternalServerError,
                    //"Error al recuperar datos de la base de datos");
            }
        }
        [Route("/api/Personaje/age")]
        [HttpGet]
        public async Task<ActionResult<PersonajesDto>> GetCharacterByAge(int age)
        {
            try
            {
                var getCharacter = await personajesRepositorio.GetCharacterByAge(age);
                if (getCharacter == null)
                {
                    return NotFound();
                }
                return mapper.Map<Personaje, PersonajesDto>(getCharacter);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al recuperar datos de la base de datos");
            }
        }
        [Route("/api/Personaje/name")]
        [HttpGet]
        public async Task<ActionResult<PersonajesDto>> GetCharacterByName(string name)
        {
            try
            {
                var getCharacter = await personajesRepositorio.GetCharacterByName(name);
                if (getCharacter == null)
                {
                    return NotFound();
                }
                return mapper.Map<Personaje, PersonajesDto>(getCharacter);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al recuperar datos de la base de datos");
            }
        }
        [Route("/api/Personaje/weight")]
        [HttpGet]
        public async Task<ActionResult<PersonajesDto>> GetCharacterByWeight(int weight)
        {
            var getCharacter = await personajesRepositorio.GetCharacterByWeight(weight);
            return mapper.Map<Personaje, PersonajesDto>(getCharacter);
        }
        [Route("/api/Personaje/movies")]
        [HttpGet]
        public async Task<ActionResult<PersonajesDto>> GetCharacterByMovie(int IdMovie)
        {
            var getCharacter = await personajesRepositorio.GetCharacterByMovie(IdMovie);
            return mapper.Map<Personaje, PersonajesDto>(getCharacter);
        }
        [HttpPost("Crear")]
        public async Task<ActionResult<Personaje>> CreateCharacter(Personaje personaje)
        {
            try
            {
                if(personaje == null)
                {
                    return BadRequest();
                }
                var result = await personajesRepositorio.CreateCharacter(personaje);
                return CreatedAtAction(nameof(CreateCharacter), new { Id = result.PersonajeID }, result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al intentar crear un nuevo personaje");
            }
            
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Personaje>> UpdateCharacter(int Id, Personaje personaje)
        {
            try
            {
                if (Id != personaje.PersonajeID)
                {
                    return BadRequest("Id del Personaje no coincide");
                }
                var Result = await personajesRepositorio.UpdateCharacter(personaje);
                if (Result == null)
                {
                    return NotFound($"Personaje con Id = {Id} no encontrado");
                }
                 await personajesRepositorio.UpdateCharacter(personaje);
                return Ok(StatusCode(StatusCodes.Status200OK));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al actualizar el personaje guardado");
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Personaje>> DeleteCharacter(int Id)
        {
            try
            {
                var delete = await personajesRepositorio.GetCharacterById(Id);
                if (delete == null)
                {
                    return NotFound($"Personaje con Id = {Id} no encontrado");
                }
                await personajesRepositorio.DeleteCharacter(Id);
                return Ok($"Personaje con Id = {Id} ha sido borrado");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al intentar borrar un personaje guardado");
            }
        }
    }           
}
