using GreenFlux_SmartCharging.Domain.Entities;

namespace GreenFlux_SmartCharging.Domain.Common.Repositories;

public interface IChargeStationRepository: IRepository<ChargeStation>
{
    Task<ChargeStation?> GetByIdAsync(Guid id);
    Task<IEnumerable<ChargeStation>?> GetChargeStationsByGroupIdAsync(Guid groupId);
    
}
