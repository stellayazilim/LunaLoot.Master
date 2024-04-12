

using System.Linq.Expressions;
using LunaLoot.Master.Infrastructure.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LunaLoot.Master.Infrastructure.Persistence.EFCore.Repositories;

public class Repository<TEntity, TPkey> : IRepository<TEntity, TPkey>
    where TEntity : class where TPkey: notnull

{
    private DbContext _dbContext { get; }
    protected readonly DbSet<TEntity> _dbSet;
    public Repository(DbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TEntity>();
    }
    
    public void Add(TEntity entity)
    {
        _dbSet.Add(entity);
    }
    
    public IEnumerable<TEntity> GetAll()
    {
        return _dbSet.ToList();
    }

    public TEntity? GetById(TPkey id)
    {
        return _dbSet.Find(id);
    }

    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression)
    {
        return _dbSet.Where(expression);
    }

    public void Remove(TPkey id)
    {
        throw new NotImplementedException();
    }

    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        throw new NotImplementedException();
    }
}