using MundoDeDisney.MundoDeDisney.Core.Entities;

namespace MundoDeDisney.MundoDeDisney.Core.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        Task<T> GetById(int id);
        Task Add (T entity);
        Task Update (T entity);
        Task Delete (int id);
    }
}
