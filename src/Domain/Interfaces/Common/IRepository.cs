using System.Linq.Expressions;

namespace Domain.Interfaces.Common;

public interface IRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> Query();
    void Add(TEntity entity);
    void AddRange(IEnumerable<TEntity> entities);
    Task AddAsync(TEntity entity);
    Task AddRangeAsync(IEnumerable<TEntity> entities);
    void Update(TEntity entity);
    void UpdateRange(IEnumerable<TEntity> entities);
    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);
    Task<TEntity?> FindAsync(params object[] keyValues);
    IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
    TEntity? FirstOrDefault();
    TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity?> FirstOrDefaultAsync();
    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
}
