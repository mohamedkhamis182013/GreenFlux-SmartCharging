namespace GreenFlux_SmartCharging.Domain.Entities;
public class Connector
{
    public Connector() { }
    public Connector(int id, int maxCurrent, Guid chargeStationId)
    {
        Id = id;
        MaxCurrent = maxCurrent;
        ChargeStationId = chargeStationId;
    }
    public int Id { get; set; }
    public int MaxCurrent { get; set; }
    public Guid ChargeStationId { get; set; }
}

