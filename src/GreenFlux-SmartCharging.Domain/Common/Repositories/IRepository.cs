namespace GreenFlux_SmartCharging.Domain.Common.Repositories;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    T Update(T entity);
    void Remove(T entity);
}

