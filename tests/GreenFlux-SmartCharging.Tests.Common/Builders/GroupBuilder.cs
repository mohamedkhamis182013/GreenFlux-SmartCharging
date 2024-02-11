using GreenFlux_SmartCharging.Domain.Entities;

namespace GreenFlux_SmartCharging.Tests.Common.Builders
{
    public class GroupBuilder
    {
        private List<ChargeStation> _chargeStation = new List<ChargeStation>{};
        private Guid _id = Guid.Empty;
        private string _name = String.Empty;
        private int _capacity ;

        public GroupBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }
        public GroupBuilder WithName(string name)
        {
            _name = name;
            return this;
        }
        public GroupBuilder WithCapacity(int capacity)
        {
            _capacity = capacity;
            return this;
        }

        public GroupBuilder WithChargeStations(List<ChargeStation> chargeStations)
        {
            _chargeStation = chargeStations;
            return this;
        }
        public Group Build()
        {
            return new Group(_id,_name,_capacity,_chargeStation);
        }
    }
}
