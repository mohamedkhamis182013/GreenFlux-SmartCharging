namespace GreenFlux_SmartCharging.Application.Dto;

public class GroupDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Capacity { get; set; }
    public ICollection<ChargeStationDto> ChargeStations { get; set; }

    public GroupDto(Guid id, string name,
        int capacity, ICollection<ChargeStationDto> chargeStations)
    {
        Id = id;
        Name = name;
        ChargeStations = chargeStations;
        Capacity = capacity;
    }

    public int CalculateCurrentCapacity()
    {
        int currentSumCapacity = 0;
        
        foreach (var chargeStation in ChargeStations)
        {
            currentSumCapacity += chargeStation.Connectors.Sum(
                x => x.MaxCurrent);
        }

        return currentSumCapacity;
    }
    public int CalculateCurrentCapacityExceptOfChargeStation(Guid chargeStationId)
    {
        int currentSumCapacity = 0;
        var chargeStationsExclude =
            ChargeStations.Where(x => x.Id != chargeStationId).ToList();
        foreach (var chargeStation in chargeStationsExclude)
        {
            currentSumCapacity += chargeStation.Connectors.Sum(
                x => x.MaxCurrent);
        }

        return currentSumCapacity;
    }

}

