using GreenFlux_SmartCharging.Domain.Entities;

namespace GreenFlux_SmartCharging.Tests.Common.Builders
{
    public class ChargeStationBuilder
    {
        private List<Connector> _connectors = 
            new List<Connector> { new Connector(0,1,Guid.Empty) };
        private Guid _id = Guid.Empty;
        private string _name = String.Empty;
        private Guid _groupId = Guid.Empty;
        
        public ChargeStationBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }
        public ChargeStationBuilder WithName(string name)
        {
            _name = name;
            return this;
        }
        public ChargeStationBuilder WithGroupId(Guid groupId)
        {
            _groupId = groupId;
            return this;
        }
        public ChargeStationBuilder WithConnectors(List<Connector> connectors)
        {
            _connectors = connectors;
            return this;
        }
        public ChargeStation Build()
        {
            return new ChargeStation(_id,_name,_groupId,_connectors);
        }
    }
}
