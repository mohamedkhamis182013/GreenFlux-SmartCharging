using GreenFlux_SmartCharging.Application.Dto;

namespace GreenFlux_SmartCharging.Application.Common.Interfaces;
public interface IGroupService
{
    Task<GroupDto> GetByIdAsync(Guid id);
    Task<IEnumerable<GroupDto>> GetAllAsync();
    Task AddAsync(GroupDto groupDto);
    Task UpdateAsync(GroupDto groupDto);
    Task RemoveAsync(Guid id);
    int AvailableCapacity(GroupDto groupDto);
    int AvailableCapacityExceptOfChargeStation(GroupDto groupDto, Guid chargeStation);
    void ValidateForAddAsync(GroupDto groupDto);
    Task ValidateForUpdateAsync(GroupDto groupDto);
}

