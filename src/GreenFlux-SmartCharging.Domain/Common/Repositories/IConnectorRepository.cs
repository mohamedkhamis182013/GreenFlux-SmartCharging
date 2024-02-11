using GreenFlux_SmartCharging.Domain.Entities;

namespace GreenFlux_SmartCharging.Domain.Common.Repositories;

public interface IConnectorRepository : IRepository<Connector>
{
    Task<Connector?> GetByIdAsync(int id);
}
