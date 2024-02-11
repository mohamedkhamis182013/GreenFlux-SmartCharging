using GreenFlux_SmartCharging.Domain.Entities;

namespace GreenFlux_SmartCharging.Domain.Common.Repositories;
public interface IGroupRepository : IRepository<Group>
{
    Task<Group?> GetByIdAsync(Guid id);
}

