using GreenFlux_SmartCharging.Domain.Common.Repositories;
using GreenFlux_SmartCharging.Domain.Entities;
using GreenFlux_SmartCharging.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GreenFlux_SmartCharging.Infrastructure.Repositories;

public class ConnectorRepository : Repository<Connector>, IConnectorRepository
{
    public ConnectorRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<IEnumerable<Connector>> GetAllAsync()
    {
        if (DbContext.Connectors != null)
            return await DbContext.Connectors.AsNoTracking().ToListAsync();
        return null;
    }

    public async Task<Connector?> GetByIdAsync(int id)
    {
        if (DbContext.Connectors != null)
            return await DbContext.Connectors.AsNoTracking().SingleOrDefaultAsync(x=>x.Id ==id); 
        return null;
    }
}

