using Microsoft.EntityFrameworkCore;
using Para.Base.Entity;
using Para.Data.Context;
using System.Linq.Expressions;

namespace Para.Data.GenericRepository;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    private readonly ParaSqlDbContext dbContext;

    public GenericRepository(ParaSqlDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task Save()
    {
        await dbContext.SaveChangesAsync();
    }

    public async Task<TEntity> GetById(Expression<Func<TEntity, bool>> filter)
    {
        return await dbContext.Set<TEntity>().Where(filter).FirstOrDefaultAsync();
    }

    public async Task Insert(TEntity entity)
    {
        entity.IsActive = true;
        entity.InsertDate = DateTime.UtcNow;
        entity.InsertUser = "System";
        await dbContext.Set<TEntity>().AddAsync(entity);
    }

    public async Task Update(TEntity entity)
    {
        dbContext.Set<TEntity>().Update(entity);
    }

    public async Task Delete(TEntity entity)
    {
        dbContext.Set<TEntity>().Remove(entity);
    }

    public async Task Delete(long Id)
    {
        var entity = await dbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == Id);
        dbContext.Set<TEntity>().Remove(entity);
    }

    public async Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> filter = null, string includeFields = "")
    {
        var query = filter == null ? dbContext.Set<TEntity>().AsNoTracking() : dbContext.Set<TEntity>().Where(filter).AsNoTracking();
        if (includeFields != "")
        {
            foreach (var includeProperty in includeFields.Split
                                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
        }
        return await query.ToListAsync();

    }
}