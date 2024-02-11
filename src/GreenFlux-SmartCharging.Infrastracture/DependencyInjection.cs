using GreenFlux_SmartCharging.Domain.Common;
using GreenFlux_SmartCharging.Domain.Common.Repositories;
using GreenFlux_SmartCharging.Infrastructure.Persistence;
using GreenFlux_SmartCharging.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GreenFlux_SmartCharging.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(o => o.UseSqlServer(configuration["ConnectionStrings:DB"]));
        //services.AddDbContext<AppDbContext>(o => o.UseInMemoryDatabase(configuration["ConnectionStrings:DB"]));
        services.AddScoped<IGroupRepository, GroupRepository>();
        services.AddScoped<IConnectorRepository, ConnectorRepository>();
        services.AddScoped<IChargeStationRepository, ChargeStationRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}

