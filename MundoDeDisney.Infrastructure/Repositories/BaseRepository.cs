using Microsoft.EntityFrameworkCore;
using MundoDeDisney.MundoDeDisney.Core.Entities;
using MundoDeDisney.MundoDeDisney.Core.Interfaces;
using MundoDeDisney.MundoDeDisney.Infrastructure.Data;

namespace MundoDeDisney.MundoDeDisney.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _entities;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return _entities.AsEnumerable();
        }
        public async Task<T> GetById(int id)
        {
            return await _entities.FindAsync(id);
        }
        public async Task Add(T entity)
        {
            await _entities.AddAsync(entity);
        }
        public async Task Update(T entity)
        {
             _entities.Update(entity);
        }
        public async Task Delete(int id)
        {
           T entity = await GetById(id);
            _entities.Remove(entity);
        }
    }
}
