using Microsoft.Extensions.Hosting;
using MundoDeDisney.MundoDeDisney.Core.CustomEntities;
using MundoDeDisney.MundoDeDisney.Core.Entities;
using MundoDeDisney.MundoDeDisney.Core.QueryFilters;

namespace MundoDeDisney.MundoDeDisney.Core.Interfaces
{
    public interface ICharacterService
    {
        PagedList<Character> GetCharacters(CharacterQueryFilter filters);
        Task<Character> GetCharacter(int id);
        Task AddCharacter(Character character);
        Task<bool>UpdateCharacter(Character character);
        Task<bool>RemoveCharacter(int id);
    }
}
