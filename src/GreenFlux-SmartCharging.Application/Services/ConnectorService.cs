using GreenFlux_SmartCharging.Application.Common.Interfaces;
using GreenFlux_SmartCharging.Application.Dto;
using GreenFlux_SmartCharging.Domain.Common.Repositories;
using GreenFlux_SmartCharging.Domain.Common;
using GreenFlux_SmartCharging.Application.Common.Exceptions;
using GreenFlux_SmartCharging.Application.Common.Extensions;
using GreenFlux_SmartCharging.Domain.Common.Exceptions;

namespace GreenFlux_SmartCharging.Application.Services
{
    public class ConnectorService: IConnectorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConnectorRepository _connectorRepository;
        private readonly IGroupService _groupService;
        private readonly IChargeStationService _chargeStationService;
        public ConnectorService(IUnitOfWork unitOfWork,
            IConnectorRepository connectorRepository,
            IGroupService groupService,
            IChargeStationService chargeStationService)
        {
            _unitOfWork = unitOfWork;
            _connectorRepository = connectorRepository;
            _groupService = groupService;
            _chargeStationService = chargeStationService;
        }
        public async Task<ConnectorDto> GetByIdAsync(int id)
        {
            var connector = await _connectorRepository.GetByIdAsync(id);
            if (connector == null)
            {
                throw new NotFoundException("No connector found for that id");
            }
            return connector.ToConnectorDto();
        }

        public async Task<IEnumerable<ConnectorDto>> GetAllAsync()
        {
            var connectors = await _connectorRepository.GetAllAsync();
            if (connectors == null || connectors.Count() == 0)
            {
                throw new NotFoundException("No connectors found");
            }
            return connectors.ToConnectorDtoList();
        }

        public async Task AddAsync(ConnectorDto connectorDto)
        {
            await ValidateForAddAsync(connectorDto);
            var connector = connectorDto.ToConnector();
            await _connectorRepository.AddAsync(connector);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(ConnectorDto connectorDto)
        {
            await ValidateForUpdateAsync(connectorDto);
            var connector = connectorDto.ToConnector();
            _connectorRepository.Update(connector);
            await _unitOfWork.CommitAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var connector = await _connectorRepository.GetByIdAsync(id);
            if (connector == null)
            {
                throw new NotFoundException("No connector found for that Id");
            }
            _connectorRepository.Remove(connector);
            await _unitOfWork.CommitAsync();
        }

        public async Task ValidateForAddAsync(ConnectorDto connectorDto)
        {
            var chargeStationDto = await _chargeStationService.GetByIdAsync(connectorDto.ChargeStationId);
            if (chargeStationDto.Connectors.Count == 5)
            {
                throw new DomainValidationException("this charge Station already have a 5 connectors");
            }
            var groupDto = await _groupService.GetByIdAsync(chargeStationDto.GroupId);
            var groupAvailableCapacity = _groupService.AvailableCapacity(groupDto);
            if (connectorDto.MaxCurrent > groupAvailableCapacity)
            {
                throw new DomainValidationException(
                    $"this connector max current is more than the available capacity in the group: {groupAvailableCapacity}");
            }
        }
        public async Task ValidateForUpdateAsync(ConnectorDto connectorDto)
        {
            var connector = await _connectorRepository.GetByIdAsync(connectorDto.Id);
            if (connector == null)
            {
                throw new NotFoundException("This connector is not exist");
            }
            var chargeStationDto = await _chargeStationService.GetByIdAsync(connectorDto.ChargeStationId);
            int newAmount = connectorDto.MaxCurrent - connector.MaxCurrent;
            var groupDto = await _groupService.GetByIdAsync(chargeStationDto.GroupId);
            var groupAvailableCapacity = _groupService.AvailableCapacity(groupDto);
            if (newAmount > groupAvailableCapacity)
            {
                throw new DomainValidationException(
                    @$"this connector max current is more than the available capacity
                              in the group please make it not more than {connector.MaxCurrent + groupAvailableCapacity}");
            }
        }
    }
}
