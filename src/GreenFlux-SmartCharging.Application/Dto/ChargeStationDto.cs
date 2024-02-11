namespace GreenFlux_SmartCharging.Application.Dto;

public class ChargeStationDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid GroupId { get; set; }

    public ICollection<ConnectorDto> Connectors { get; set; }

    public ChargeStationDto(Guid id, string name, Guid groupId,ICollection <ConnectorDto> connectors)
    {
        Id = id;
        Name = name;
        GroupId = groupId;
        Connectors = connectors;
    }
}

