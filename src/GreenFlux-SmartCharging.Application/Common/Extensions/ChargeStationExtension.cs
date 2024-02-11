using GreenFlux_SmartCharging.Application.Dto;
using GreenFlux_SmartCharging.Domain.Entities;

namespace GreenFlux_SmartCharging.Application.Common.Extensions;

public static class ChargeStationExtension
{
    public static IEnumerable<ChargeStationDto> ToChargeStationDtoList(this IEnumerable<ChargeStation> chargeStationList)
    {
        var chargeStationDtoList = new List<ChargeStationDto>();
        foreach (var chargeStation in chargeStationList)
        {
            if (chargeStation.Connectors == null || chargeStation.Connectors?.Count == 0)
            {
                chargeStation.Connectors = new List<Connector>();
            }
            chargeStationDtoList.Add(
                new ChargeStationDto(
                    chargeStation.Id
                    , chargeStation.Name
                    , chargeStation.GroupId
                    , chargeStation.Connectors.ToConnectorDtoList().ToList()));
        }
        return chargeStationDtoList;

    }
    public static ChargeStationDto ToChargeStationDto(this ChargeStation chargeStation)
    {
        if (chargeStation.Connectors == null || chargeStation.Connectors?.Count == 0)
        {
            chargeStation.Connectors = new List<Connector>();
        }
        return new ChargeStationDto(
             chargeStation.Id
            ,chargeStation.Name
            ,chargeStation.GroupId
            ,chargeStation.Connectors.ToConnectorDtoList().ToList());
    }
    public static IEnumerable<ChargeStation> ToChargeStationList(this IEnumerable<ChargeStationDto> chargeStationDtoList)
    {
        var chargeStationList = new List<ChargeStation>();
        foreach (var chargeStationDto in chargeStationDtoList)
        {
            chargeStationList.Add(
                new ChargeStation(
                        chargeStationDto.Id,
                        chargeStationDto.Name,
                        chargeStationDto.GroupId,
                        chargeStationDto.Connectors.ToConnectorList()));
        }
        return chargeStationList;
    }
    public static ChargeStation ToChargeStation(this ChargeStationDto chargeStationDto)
    {
        return new ChargeStation(
            chargeStationDto.Id
            , chargeStationDto.Name
            , chargeStationDto.GroupId
            , chargeStationDto.Connectors.ToConnectorList());
    }
    
}

