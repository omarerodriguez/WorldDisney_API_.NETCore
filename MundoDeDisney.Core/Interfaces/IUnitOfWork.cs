using MundoDeDisney.MundoDeDisney.Core.Entities;

namespace MundoDeDisney.MundoDeDisney.Core.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IGenreRepository GenreRepository { get; }
        IBaseRepository<Movie> MovieRepository { get; }
        ICharacterRepository CharacterRepository { get; }

        void SaveChanges();
        Task SaveChangesAsync();
    }
}
