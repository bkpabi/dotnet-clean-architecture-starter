using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Domain.SeedWork;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    List<TEntity> GetAll();
    Task<TEntity> FindById(Guid id);

    IEnumerable<TEntity> Find(ISpecification<TEntity> specification = null);

    Task<bool> Add(TEntity entity);
    Task<bool> AddRange(IEnumerable<TEntity> entities);

    Task Remove(TEntity entity);
    Task RemoveRange(IEnumerable<TEntity> entities);

    Task Update(TEntity entity);

    bool Contains(ISpecification<TEntity> specification = null);
    bool Contains(Expression<Func<TEntity, bool>> predicate);

    int Count(ISpecification<TEntity> specification = null);
    int Count(Expression<Func<TEntity, bool>> predicate);
}
