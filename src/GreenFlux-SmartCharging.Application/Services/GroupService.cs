using GreenFlux_SmartCharging.Application.Common.Exceptions;
using GreenFlux_SmartCharging.Application.Common.Extensions;
using GreenFlux_SmartCharging.Application.Common.Interfaces;
using GreenFlux_SmartCharging.Application.Dto;
using GreenFlux_SmartCharging.Domain.Common;
using GreenFlux_SmartCharging.Domain.Common.Exceptions;
using GreenFlux_SmartCharging.Domain.Common.Repositories;

namespace GreenFlux_SmartCharging.Application.Services;

public class GroupService : IGroupService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGroupRepository _groupRepository;
    public GroupService(IUnitOfWork unitOfWork, IGroupRepository groupRepository)
    {
        _unitOfWork = unitOfWork;
        _groupRepository = groupRepository;
    }
    public async Task<GroupDto> GetByIdAsync(Guid id)
    {
        var group = await _groupRepository.GetByIdAsync(id);
        if (group == null)
        {
            throw new NotFoundException("No group found for that id");
        }
        return group.ToGroupDto();
    }

    public async Task<IEnumerable<GroupDto>> GetAllAsync()
    {
        var groups = await _groupRepository.GetAllAsync();
        if (groups == null || groups.Count() == 0)
        {
            throw new NotFoundException("No groups found");
        }
        return groups.ToGroupDtoList();
    }

    public async Task AddAsync(GroupDto groupDto)
    {
        ValidateForAddAsync(groupDto);
        var group = groupDto.ToGroup();
        await _groupRepository.AddAsync(group);
        await _unitOfWork.CommitAsync();
    }

    public async Task UpdateAsync(GroupDto groupDto)
    {
        await ValidateForUpdateAsync(groupDto);
        var group = groupDto.ToGroup();
        _groupRepository.Update(group);
        await _unitOfWork.CommitAsync();
    }

    public async Task RemoveAsync(Guid id)
    {
        var group = await _groupRepository.GetByIdAsync(id);
        if (group == null)
        {
            throw new NotFoundException("No charge station found for that Id");
        }
        _groupRepository.Remove(group);
        await _unitOfWork.CommitAsync();
    }

    public int AvailableCapacity(GroupDto groupDto)
    {
        return groupDto.Capacity - groupDto.CalculateCurrentCapacity();
    }

    public int AvailableCapacityExceptOfChargeStation(GroupDto groupDto, Guid chargeStation)
    {
        return groupDto.Capacity - groupDto.CalculateCurrentCapacityExceptOfChargeStation(chargeStation);
    }

    public void ValidateForAddAsync(GroupDto groupDto)
    {
        if (!(groupDto.ChargeStations.Count == 0 || groupDto.ChargeStations.Count == 1))
        {
            throw new DomainValidationException("Only one charge station can be added in one call");

        }
    }

    public async Task ValidateForUpdateAsync(GroupDto groupDto)
    {
        var group = await _groupRepository.GetByIdAsync(groupDto.Id);
        if (group == null)
        {
            throw new NotFoundException("This Group is not exist");
        }

        var currentChargeStationsCount = group.ChargeStations?.Count ?? 0;
        var newChargeStationsCount = groupDto.ChargeStations.Count;
        if (!(newChargeStationsCount == currentChargeStationsCount
              || newChargeStationsCount == currentChargeStationsCount + 1))
        {
            throw new DomainValidationException("Only one charge station can be added in one call");

        }
    }
}

