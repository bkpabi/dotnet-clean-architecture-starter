using CleanArch.Domain.SeedWork;
using CleanArch.Infrastructure.Data.DBContext;
using CleanArch.Infrastructure.Data.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Infrastructure.Data.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    private readonly CleanArchContext _dbContext;
    internal DbSet<TEntity> _set;

    public GenericRepository(CleanArchContext dbContext)
    {
        _dbContext = dbContext;
        _set = _dbContext.Set<TEntity>();
    }

    public List<TEntity> GetAll()
    {
        return _set.ToList();
    }
    public async Task<bool> Add(TEntity entity)
    {
        await _set.AddAsync(entity);
        return true;
    }

    public async Task<bool> AddRange(IEnumerable<TEntity> entities)
    {
        await _set.AddRangeAsync(entities);
        return true;
    }

    public bool Contains(ISpecification<TEntity> specification = null)
    {
        return Count(specification) > 0 ? true : false;
    }

    public bool Contains(Expression<Func<TEntity, bool>> predicate)
    {
        return Count(predicate) > 0 ? true : false;
    }

    public int Count(ISpecification<TEntity> specification = null)
    {
        return ApplySpecification(specification).Count();
    }

    public int Count(Expression<Func<TEntity, bool>> predicate)
    {
        return _set.Where(predicate).Count();
    }

    public IEnumerable<TEntity> Find(ISpecification<TEntity> specification = null)
    {
        return ApplySpecification(specification);
    }

    public async Task<TEntity> FindById(Guid id)
    {
        var data = await _set.FindAsync(id);
        return data;
    }

    public Task Remove(TEntity entity)
    {
        _set.Remove(entity);
        return Task.CompletedTask;
    }

    public Task RemoveRange(IEnumerable<TEntity> entities)
    {
        _set.RemoveRange(entities);
        return Task.CompletedTask;
    }

    public Task Update(TEntity entity)
    {
        _set.Attach(entity);
        _dbContext.Entry(entity).State = EntityState.Modified;
        return Task.CompletedTask;
    }

    private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
    {
        return SpecificationEvaluator<TEntity>.GetQuery(_set.AsQueryable(), spec);
    }
}
