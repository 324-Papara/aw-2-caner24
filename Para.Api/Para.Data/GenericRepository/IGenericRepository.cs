using System.Linq.Expressions;

namespace Para.Data.GenericRepository;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task Save();
    Task<TEntity?> GetById(Expression<Func<TEntity, bool>> filter);
    Task Insert(TEntity entity);
    Task Update(TEntity entity);
    Task Delete(TEntity entity);
    Task Delete(long Id);
    Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> filter = null, string includeFields = "");
}