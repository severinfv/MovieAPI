using System.Linq.Expressions;

namespace Domain.Contracts.Repositories
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll(bool trackChanges = false);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false);
        Task<bool> EntityExistsAsync(int id);
        Task<T?> GetEntityByIdAsync(int id, bool trackChanges = false);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
