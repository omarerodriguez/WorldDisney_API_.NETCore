
using MundoDeDisney.MundoDeDisney.Core.Entities;
using MundoDeDisney.MundoDeDisney.Core.Exceptions;
using MundoDeDisney.MundoDeDisney.Core.Interfaces;

namespace MundoDeDisney.MundoDeDisney.Core.Services
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenreService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Genre> GetGenres()
        {
            return _unitOfWork.GenreRepository.GetAll();
        }
        public async Task<Genre> GetGenre(int id)
        {
            var genre = await _unitOfWork.GenreRepository.GetById(id);
            if (genre == null) { throw new BusinessExceptions($"Genre Id:{id} not found"); }
            return genre;
        }
        public async Task AddGenre(Genre genre)
        {
            var genreExist = await _unitOfWork.GenreRepository.GetGenreByName(genre.Name);
            if (genreExist is not null) { throw new BusinessExceptions("Genre name already exist"); }
            await _unitOfWork.GenreRepository.Add(genre);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<bool> Update(Genre genre)
        {
            await _unitOfWork.GenreRepository.Update(genre);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(int id)
        {
            await _unitOfWork.GenreRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
