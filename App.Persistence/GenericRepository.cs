using App.Application.Contracts.Persistence;
using App.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace App.Persistence
{
    public class GenericRepository<T, Tid>(AppDbContext dbContext) : IGenericRepository<T, Tid> where T : BaseEntity<Tid> where Tid : struct
    {
        protected AppDbContext DbContext { get; } = dbContext;

        private readonly DbSet<T> _dbSet = dbContext.Set<T>();

        public async ValueTask AddAsync(T entity) => await _dbSet.AddAsync(entity);

        public void Delete(T entity) => _dbSet.Remove(entity);

        public ValueTask<T?> GetByIdAsync(int id) => _dbSet.FindAsync(id);

        public void Update(T entity) => _dbSet.Update(entity);

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate) => _dbSet.Where(predicate).AsQueryable().AsNoTracking();

        public Task<bool> AnyAsync(Tid id) => _dbSet.AnyAsync(x=> x.Id.Equals(id));

        public Task<List<T>> GetAllAsync()
        {
            return _dbSet.ToListAsync();
        }

        public Task<List<T>> GetAllPagedAsync(int pageNum, int pageSize)
        {
            return _dbSet
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.AnyAsync(predicate);
        }
    }
}
