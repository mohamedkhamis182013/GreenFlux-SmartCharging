namespace GreenFlux_SmartCharging.Application.Dto;
public class ConnectorDto
{
    public ConnectorDto(int id, int maxCurrent, Guid chargeStationId)
    {

        Id = id;
        ChargeStationId = chargeStationId;
        MaxCurrent = maxCurrent;
    }
    public int Id { get; set; }
    public int MaxCurrent { get; set; }

    public Guid ChargeStationId { get; set; }

}
