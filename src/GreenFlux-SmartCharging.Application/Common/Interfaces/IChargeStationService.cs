using GreenFlux_SmartCharging.Application.Dto;

namespace GreenFlux_SmartCharging.Application.Common.Interfaces;
public interface IChargeStationService
{
    Task<IEnumerable<ChargeStationDto>> GetByGroupIdAsync(Guid groupId);
    Task<ChargeStationDto> GetByIdAsync(Guid id);
    Task<IEnumerable<ChargeStationDto>> GetAllAsync();
    Task AddAsync(ChargeStationDto chargeStationDto);
    Task UpdateAsync(ChargeStationDto chargeStationDto);
    Task RemoveAsync(Guid id);
    int GetChargeStationConnectorsCapacity(ChargeStationDto chargeStation);
    Task ValidateForAddAsync(ChargeStationDto chargeStationDto);
    Task ValidateForUpdateAsync(ChargeStationDto chargeStationDto);
}

