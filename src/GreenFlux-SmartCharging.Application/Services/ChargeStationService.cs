using GreenFlux_SmartCharging.Application.Common.Exceptions;
using GreenFlux_SmartCharging.Application.Common.Extensions;
using GreenFlux_SmartCharging.Application.Common.Interfaces;
using GreenFlux_SmartCharging.Application.Dto;
using GreenFlux_SmartCharging.Domain.Common;
using GreenFlux_SmartCharging.Domain.Common.Exceptions;
using GreenFlux_SmartCharging.Domain.Common.Repositories;

namespace GreenFlux_SmartCharging.Application.Services;

public class ChargeStationService : IChargeStationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IChargeStationRepository _chargeStationRepository;
    private readonly IGroupService _groupService;
    public ChargeStationService(IUnitOfWork unitOfWork, 
        IChargeStationRepository chargeStationRepository, IGroupService groupService)
    {
        _unitOfWork = unitOfWork;
        _chargeStationRepository = chargeStationRepository;
        _groupService = groupService;
    }
    public async Task<IEnumerable<ChargeStationDto>> GetByGroupIdAsync(Guid groupId)
    {

        var chargeStations = await _chargeStationRepository.GetChargeStationsByGroupIdAsync(groupId);
        if (chargeStations == null || chargeStations.Count() == 0)
        {
            throw new NotFoundException("No charge stations found for that group id");
        }
        return chargeStations.ToChargeStationDtoList();
    }

    public async Task<ChargeStationDto> GetByIdAsync(Guid id)
    {
        var chargeStation = await _chargeStationRepository.GetByIdAsync(id);
        if (chargeStation == null)
        {
            throw new NotFoundException("No charge station found for that Id");
        }

        return chargeStation.ToChargeStationDto();
    }

    public async Task<IEnumerable<ChargeStationDto>> GetAllAsync()
    {
        var chargeStations = await _chargeStationRepository.GetAllAsync();
        if (chargeStations == null || chargeStations.Count() == 0)
        {
            throw new NotFoundException("No charge stations found");
        }
        return chargeStations.ToChargeStationDtoList();
    }

    public async Task AddAsync(ChargeStationDto chargeStationDto)
    {
        await ValidateForAddAsync(chargeStationDto);
        var chargeStation = chargeStationDto.ToChargeStation();
        await _chargeStationRepository.AddAsync(chargeStation);
        await _unitOfWork.CommitAsync();
    }

    public async Task UpdateAsync(ChargeStationDto chargeStationDto)
    {
        await ValidateForUpdateAsync(chargeStationDto);
        var chargeStation = chargeStationDto.ToChargeStation();
        _chargeStationRepository.Update(chargeStation);
        await _unitOfWork.CommitAsync();
    }

    public async Task RemoveAsync(Guid id)
    {
        var chargeStation =  await _chargeStationRepository.GetByIdAsync(id);
        if (chargeStation == null)
        {
            throw new NotFoundException("No charge station found for that Id");
        }
        _chargeStationRepository.Remove(chargeStation);
        await _unitOfWork.CommitAsync();
    }

    public int GetChargeStationConnectorsCapacity(ChargeStationDto chargeStationDto)
    {
        return chargeStationDto.Connectors.Sum(x => x.MaxCurrent);
    }

    public async Task ValidateForAddAsync(ChargeStationDto chargeStationDto)
    {
        var groupDto = await _groupService.GetByIdAsync(chargeStationDto.GroupId);
        var groupAvailableCapacity = _groupService.AvailableCapacity(groupDto);
        var chargeStationCapacity = GetChargeStationConnectorsCapacity(chargeStationDto);
        if (chargeStationCapacity > groupAvailableCapacity)
        {
            throw new DomainValidationException(
                $"charge Station capacity is more than the available capacity in the group: {groupAvailableCapacity}");
        }
    }
    public async Task ValidateForUpdateAsync(ChargeStationDto chargeStationDto)
    {
        var groupDto = await _groupService.GetByIdAsync(chargeStationDto.GroupId);
        var groupAvailableCapacity = _groupService.AvailableCapacityExceptOfChargeStation(groupDto, chargeStationDto.Id);
        var newChargeStationCapacity = GetChargeStationConnectorsCapacity(chargeStationDto);
        if (newChargeStationCapacity > groupAvailableCapacity)
        {
            throw new DomainValidationException(
                $"charge Station capacity is more than the available capacity in the group: {groupAvailableCapacity}");
        }
    }

}

