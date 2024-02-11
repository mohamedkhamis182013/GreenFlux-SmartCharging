using GreenFlux_SmartCharging.Application.Dto;
using GreenFlux_SmartCharging.Domain.Entities;

namespace GreenFlux_SmartCharging.Application.Common.Extensions;

public static class ConnectorExtension
{
    public static IEnumerable<ConnectorDto> ToConnectorDtoList(this IEnumerable<Connector> connectorList)
    {
        var connectorDtoList = new List<ConnectorDto>();
        foreach (var connector in connectorList)
        {
            connectorDtoList.Add(new ConnectorDto(connector.Id,connector.MaxCurrent,connector.ChargeStationId));
        }
        return connectorDtoList;
    }

    public static ConnectorDto ToConnectorDto(this Connector connector)
    {
        return new ConnectorDto(connector.Id, connector.MaxCurrent, connector.ChargeStationId);
    }
    public static IEnumerable<Connector> ToConnectorList(this IEnumerable<ConnectorDto> connectorDtoList)
    {
        var connectorList = new List<Connector>();
        foreach (var connectorDto in connectorDtoList)
        {
            connectorList.Add(new Connector(connectorDto.Id, connectorDto.MaxCurrent, connectorDto.ChargeStationId));
        }
        return connectorList;
    }

    public static Connector ToConnector(this ConnectorDto connector)
    {
        return new Connector(connector.Id, connector.MaxCurrent, connector.ChargeStationId);
    }

}

