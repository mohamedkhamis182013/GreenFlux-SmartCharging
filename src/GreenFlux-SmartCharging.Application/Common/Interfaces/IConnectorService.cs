using GreenFlux_SmartCharging.Application.Dto;
using GreenFlux_SmartCharging.Domain.Entities;

namespace GreenFlux_SmartCharging.Application.Common.Interfaces;

public interface IConnectorService
{
    Task<ConnectorDto> GetByIdAsync(int id);
    Task<IEnumerable<ConnectorDto>> GetAllAsync();
    Task AddAsync(ConnectorDto connectorDto);
    Task UpdateAsync(ConnectorDto connectorDto);
    Task RemoveAsync(int id);
    Task ValidateForAddAsync(ConnectorDto connectorDto);
    Task ValidateForUpdateAsync(ConnectorDto connectorDto);

}