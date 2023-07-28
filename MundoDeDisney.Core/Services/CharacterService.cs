using Microsoft.Extensions.Options;
using MundoDeDisney.MundoDeDisney.Core.CustomEntities;
using MundoDeDisney.MundoDeDisney.Core.Entities;
using MundoDeDisney.MundoDeDisney.Core.Exceptions;
using MundoDeDisney.MundoDeDisney.Core.Interfaces;
using MundoDeDisney.MundoDeDisney.Core.Options;
using MundoDeDisney.MundoDeDisney.Core.QueryFilters;

namespace MundoDeDisney.MundoDeDisney.Core.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICharacterRepository _characterRepository;
        private readonly PaginationOptions _paginationOptions;

        public CharacterService(IUnitOfWork unitOfWork, ICharacterRepository characterRepository, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _characterRepository = characterRepository;
            _paginationOptions = options.Value;
        }
        public PagedList<Character> GetCharacters(CharacterQueryFilter filters)
        {
            var character = _characterRepository.GetCharacters();
            if(filters.Name != null) {character = character.Where(x => x.Name.ToLower().Contains(filters.Name.ToLower()));}
            if (filters.Age != null) { character = character.Where(x => x.Age == filters.Age);}
            if (filters.MovieID != null) {character = character.Where(m=>m.MoviesCharacters.Any(m=>m.MovieId==filters.MovieID));}

            filters.PageNumber = filters.PageNumber == 0 ?_paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

            var pagedCharacters = PagedList<Character>.Create(character, filters.PageSize, filters.PageNumber);
            return pagedCharacters;
        }
        public async Task<Character> GetCharacter(int id)
        {
            var character = await _unitOfWork.CharacterRepository.GetById(id);
            if(character == null) { throw new BusinessExceptions($"Character Id:{id} not found");}
            return character;
        }
        public async Task AddCharacter(Character character)
        {
            var characterExist = await _unitOfWork.CharacterRepository.GetCharacterByName(character.Name);
            if (characterExist is null) { throw new BusinessExceptions("Character name already exist"); }
            await _unitOfWork.CharacterRepository.Add(character);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<bool> UpdateCharacter(Character character)
        {
             await _unitOfWork.CharacterRepository.Update(character);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        public async Task<bool> RemoveCharacter(int id)
        {
            await _unitOfWork.CharacterRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
