using MundoDeDisney.MundoDeDisney.Core.Entities;
using MundoDeDisney.MundoDeDisney.Infrastructure.Repositories;

namespace MundoDeDisney.MundoDeDisney.Core.Interfaces
{
    public interface ICharacterRepository:IBaseRepository<Character>
    {
        IEnumerable<Character> GetCharacters();
        Task<IEnumerable<Character>> GetCharacterByName(string name);
    }
}