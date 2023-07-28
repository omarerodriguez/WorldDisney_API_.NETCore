using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MundoDeDisney.MundoDeDisney.Core.CustomEntities;
using MundoDeDisney.MundoDeDisney.Core.DTOs;
using MundoDeDisney.MundoDeDisney.Core.Entities;
using MundoDeDisney.MundoDeDisney.Core.Interfaces;
using MundoDeDisney.MundoDeDisney.Core.QueryFilters;
using MundoDeDisney.MundoDeDisney.Infrastructure.Data;
using MundoDeDisney.Response;

namespace MundoDeDisney.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public CharacterController(ICharacterService characterService, IMapper mapper, ApplicationDbContext context)
        {
            this._characterService = characterService;
            this._mapper = mapper;
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<CharacterDTO>> GetAll([FromQuery] CharacterQueryFilter filters)
        {
            var characters = _characterService.GetCharacters(filters);
            var characterDTO = _mapper.Map<IEnumerable<CharacterDTO>>(characters);
            var metadata = new Metadata
            {
                TotalCount = characters.TotalCount,
                PageSize = characters.PageSize,
                CurrentPage = characters.CurrentPage,
                TotalPages = characters.TotalPages,
                HasNextPage = characters.HasNextPage,
                HasPreviousPage = characters.HasPreviousPage
            };
            var response = new ApiResponse<IEnumerable<CharacterDTO>>(characterDTO) { Meta = metadata };
            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterShowDetailsDTO>> GetGenre(int id)
        {
            var character = await _context.Characters.Select(c =>new
             {
                 Id=c.Id,
                 Name =c.Name, Age = c.Age, Weight = c.Weigth, History=c.History,Image=c.Image,
                 Movies = c.MoviesCharacters.Select(mc => new{ MovieId = mc.MovieId,Title = mc.Movie.Title}),
             }).FirstOrDefaultAsync(c=>c.Id == id);
            return Ok(character);
        }
        [HttpPost]
        public async Task<ActionResult> Add([FromForm]CharacterCreationDTO characterCreationDTO)
        {
            var character = _mapper.Map<Character>(characterCreationDTO);
            await _characterService.AddCharacter(character);
            characterCreationDTO = _mapper.Map<CharacterCreationDTO>(character);
            return Ok(characterCreationDTO);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, CharacterPutDTO characterPutDTO)
        {
            var characterExist = _characterService.GetCharacter(id);
            if (characterExist == null) { return NotFound($"the genre id: {id} doesn't exist"); }
            var character = _mapper.Map<Character>(characterPutDTO);
            await _characterService.UpdateCharacter(character);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var characterDelete = await _characterService.RemoveCharacter(id);
            return Ok(characterDelete);
        }
    }
}
