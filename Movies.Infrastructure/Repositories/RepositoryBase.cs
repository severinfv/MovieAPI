using Domain.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using Movies.Infrastructure.Data;
using System.Linq.Expressions;

namespace Movies.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class //do EntityBase
    {
        //protected MovieContext Context { get; }
        protected DbSet<T> DbSet { get; }

        public RepositoryBase(MovieContext context)
        {
            //Context = context;
            DbSet = context.Set<T>();
        }
        public IQueryable<T> FindAll(bool trackChanges = false) =>
            !trackChanges ? DbSet.AsNoTracking() :
                            DbSet;

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false) =>
            !trackChanges ? DbSet.Where(expression).AsNoTracking() :
                            DbSet.Where(expression);


        public void Create(T entity) => DbSet.Add(entity);

        public void Update(T entity) => DbSet.Update(entity);

        public void Delete(T entity) => DbSet.Remove(entity);
    }
}
