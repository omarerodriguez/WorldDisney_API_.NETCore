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
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterRepository characterRepository;
        private readonly DisneyDbContext db;
        public CharacterController(ICharacterRepository characterRepository, DisneyDbContext db)
        {
            this.characterRepository = characterRepository;
            this.db = db;
        }

        // [Route]
        [HttpGet]
        public async Task<ActionResult<Character>> GetAllMovie()
        {
            var character = await characterRepository.GetAllCharacters();
            var list = character.Select(x => new { x.Name, x.Image }).ToList();
            return Ok(list);
        }
        [Route("id")]
        [HttpGet]
        public async Task<ActionResult<Character>> GetCharacter(int id)
        {
            try
            {
                var characterId = await db.Characters.Select(p => new
                {
                    Id = p.CharacterID,
                    Name = p.Name,
                    Image = p.Image,
                    Age = p.Age,
                    Weight = p.Weight,
                    History = p.History,
                    CharacterMovie = p.CharactersMovies.Select(cm => 
                    new
                    { cm.MovieID,cm.Movie.Title})

                }).FirstOrDefaultAsync(c=>c.Id==id);

                return Ok(characterId);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [Authorize(Roles = "admin")]
        [HttpPost("Create")]
        public async Task<ActionResult> CreateCharacter(Character character)
        {
            try
            {
                if (character == null)
                {
                    return BadRequest();
                }
                var characterResult = await characterRepository.CreateCharacter(character);
                return Ok(character); //CreatedAtAction(nameof(CreateCharacter), new { Id = characterResult.CharacterID }, characterResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);//StatusCode(StatusCodes.Status500InternalServerError,
                   // "Error creating new character record");
            }
        }
        [Authorize(Roles = "admin")]
        [HttpPut("Update")]
        public async Task<ActionResult<Character>> UpdateCharacter(int id,Character character)
        {
            try
            {
                if (id != character.CharacterID)
                {
                    return BadRequest("character Id mismatch");
                }
                var characterResult = await characterRepository.UpdateCharacter(character);
                if (characterResult == null)
                {
                    return NotFound($"Character with Id = {id} not found");
                }
                return await characterRepository.UpdateCharacter(character);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Update character record");
            }
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("Delete")]
        public async Task<ActionResult<Character>> Delete(int id)
        {
            try
            {
                var characterDelete = await characterRepository.GetCharacterById(id);
                if (characterDelete == null)
                {
                    return NotFound($"Character with Id = {id} not found");
                }
                await characterRepository.DeleteCharacter(id);
                return Ok($"Character with Id = {id} deleted");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error delete character record");
            }
        }
        [Route("name")]
        [HttpGet]
        public async Task<ActionResult<Character>> GetCharacterName(string name)
        {
            try
            {
                var characterName = await characterRepository.GetCharacterByName(name);

                if (characterName == null)
                {
                    return NotFound();
                }
                var character = await db.Characters.Select(p => new
                {
                    Name = p.Name,
                    Id = p.CharacterID,
                    Image = p.Image,
                    Age = p.Age,
                    Weight = p.Weight,
                    History = p.History,
                    CharacterMovie = p.CharactersMovies.Select(cm => new
                    { cm.MovieID, cm.Movie.Title })

                }).FirstOrDefaultAsync(c => c.Name == name);
                return Ok(character);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [Route("age")]
        [HttpGet]
        public async Task<ActionResult<Character>> GetCharacterAge(int age)
        {
            try
            {
                var characterAge = await characterRepository.GetCharacterByAge(age);
                if (characterAge == null)
                {
                    return NotFound();
                }
                var character = await db.Characters.Select(p => new
                {
                    Age = p.Age,
                    Id = p.CharacterID,
                    Name = p.Name,
                    Image = p.Image,
                    Weight = p.Weight,
                    History = p.History,
                    CharacterMovie = p.CharactersMovies.Select(cm =>
                    new
                    { cm.MovieID, cm.Movie.Title })

                }).FirstOrDefaultAsync(c => c.Age == age);
                return Ok(character);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [Route("weight")]
        [HttpGet]
        public async Task<ActionResult<Character>> GetCharacterWeight(int weight)
        {
            try
            {
                var characterWeight = await characterRepository.GetCharacterByWeight(weight);
                if (characterWeight == null)
                {
                    return NotFound();
                }
                var character = await db.Characters.Select(p => new
                {
                    Weight = p.Weight,
                    Name = p.Name,
                    Age = p.Age,
                    Id = p.CharacterID,
                    Image = p.Image,
                    History = p.History,
                    CharacterMovie = p.CharactersMovies.Select(cm =>
                    new
                    { cm.MovieID, cm.Movie.Title })

                }).FirstOrDefaultAsync(c => c.Weight == weight);
                return Ok(character);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
    }
}
