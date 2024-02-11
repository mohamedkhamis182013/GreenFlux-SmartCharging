using GreenFlux_SmartCharging.Domain.Common.Repositories;
using GreenFlux_SmartCharging.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GreenFlux_SmartCharging.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext DbContext;
    public Repository(AppDbContext dbContext)
    {
        DbContext = dbContext;
    }
    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await DbContext.Set<T>().ToListAsync();
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        await DbContext.Set<T>().AddAsync(entity);
        return entity;
    }

    public virtual T Update(T entity)
    {
        DbContext.Set<T>().Update(entity);
        return entity;
    }

    public virtual void Remove(T entity)
    {
         DbContext.Set<T>().Remove(entity);
    }
}

