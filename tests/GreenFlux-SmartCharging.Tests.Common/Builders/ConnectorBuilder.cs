using GreenFlux_SmartCharging.Domain.Entities;

namespace GreenFlux_SmartCharging.Tests.Common.Builders
{
    public class ConnectorBuilder
    {
        private int _id ;
        private int _maxCurrent ;
        private Guid _chargeStationId = Guid.Empty;

        public ConnectorBuilder WithId(int id)
        {
            _id = id;
            return this;
        }
        public ConnectorBuilder WithMaxCurrent(int maxCurrent)
        {
            _maxCurrent = maxCurrent;
            return this;
        }
        public ConnectorBuilder WithChargeStationI(Guid chargeStationI)
        {
            _chargeStationId = chargeStationI;
            return this;
        }
        public Connector Build()
        {
            return new Connector(_id,_maxCurrent, _chargeStationId);
        }
    }
}
