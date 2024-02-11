using GreenFlux_SmartCharging.Domain.Common.Repositories;
using GreenFlux_SmartCharging.Domain.Entities;
using GreenFlux_SmartCharging.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GreenFlux_SmartCharging.Infrastructure.Repositories;
public class GroupRepository : Repository<Group>, IGroupRepository
{
    public GroupRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<IEnumerable<Group>> GetAllAsync()
    {
        if (DbContext.Groups != null)
            return await DbContext.Groups.
            Include(x => x.ChargeStations)!
            .ThenInclude(y => y.Connectors).AsNoTracking().ToListAsync();
        return null;
    }

    public async Task<Group?> GetByIdAsync(Guid id)
    {
        if (DbContext.Groups != null)
            return await DbContext.Groups.
                 Include(x=>x.ChargeStations)!
                .ThenInclude(y=>y.Connectors).AsNoTracking().SingleOrDefaultAsync(x=>x.Id == id);
        return null;
    }
}

