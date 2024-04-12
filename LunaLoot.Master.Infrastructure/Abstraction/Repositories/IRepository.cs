using System.Linq.Expressions;

namespace LunaLoot.Master.Infrastructure.Abstraction;

public interface IRepository<TEntity, in TPKey> where TEntity: class where TPKey: notnull
{
   IEnumerable<TEntity> GetAll();
   
   TEntity? GetById(TPKey id);

   IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
   
   void Add(TEntity entity);
   
   void Remove(TPKey id);
   
   void RemoveRange(IEnumerable<TEntity> entities);
}