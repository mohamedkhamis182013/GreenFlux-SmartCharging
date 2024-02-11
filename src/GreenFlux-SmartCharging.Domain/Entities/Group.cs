namespace GreenFlux_SmartCharging.Domain.Entities;
public class Group
{
    public Group()
    {

    }
    public Group(Guid id, string name, int capacity, IEnumerable<ChargeStation> chargeStations)
    {
        Id = id;
        Name = name;
        Capacity = capacity;
        ChargeStations = chargeStations.ToList();
    }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Capacity { get; set; }
    public ICollection<ChargeStation>? ChargeStations { get; set; }
}
