using GreenFlux_SmartCharging.Domain.Common.Repositories;
using GreenFlux_SmartCharging.Domain.Entities;
using GreenFlux_SmartCharging.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GreenFlux_SmartCharging.Infrastructure.Repositories;

public class ChargeStationRepository : Repository<ChargeStation>, IChargeStationRepository
{
    public ChargeStationRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
    public override async Task<IEnumerable<ChargeStation>> GetAllAsync()
    {
        if (DbContext.ChargeStations != null)
            return await DbContext.ChargeStations.
                Include(cs => cs.Connectors)!
                .AsNoTracking().ToListAsync();
        return null;
    }
    public async Task<IEnumerable<ChargeStation>?> GetChargeStationsByGroupIdAsync(Guid groupId)
    {
        if (DbContext.ChargeStations != null)
            return await DbContext.ChargeStations
            .Where(a => a.GroupId == groupId)
            .Include(a => a.Connectors)
            .AsNoTracking().ToListAsync();
        return null;
    }
    public async Task<ChargeStation?> GetByIdAsync(Guid id)
    {
        if (DbContext.ChargeStations != null) 
            return await DbContext.ChargeStations
                .Include(cs=>cs.Connectors).AsNoTracking()
                .SingleOrDefaultAsync(cs =>cs.Id==id);
        return null;
    }
}

