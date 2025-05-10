using System.Linq.Expressions;

namespace App.Application.Contracts.Persistence;

    public interface IGenericRepository<T, Tid> where T : class where Tid : struct
{
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllPagedAsync(int pageNum, int pageSize);
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        ValueTask<T?> GetByIdAsync(int id);
        ValueTask AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> AnyAsync(Tid id);
}