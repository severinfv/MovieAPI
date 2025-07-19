using System.Linq.Expressions;

namespace Movies.Core.Repositories
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll(bool trackChanges = false);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false);
        Task<bool> EntityExistsAsync(Guid id);
        Task<T?> GetEntityByIdAsync(Guid id, bool trackChanges = false);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
