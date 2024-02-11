namespace GreenFlux_SmartCharging.Domain.Entities;

public class ChargeStation
{
    public ChargeStation()
    {
            
    }
    public ChargeStation(Guid id, string name, Guid groupId , IEnumerable<Connector> connectors)
    {
        Id = id;
        Name = name;
        GroupId = groupId;
        Connectors = connectors.ToList();
    }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid GroupId { get; set; }
    public ICollection<Connector>? Connectors { get; set; }
   
}

