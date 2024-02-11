
using GreenFlux_SmartCharging.Application.Common.Interfaces;
using GreenFlux_SmartCharging.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GreenFlux_SmartCharging.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IChargeStationService, ChargeStationService>();
        services.AddScoped<IConnectorService, ConnectorService>();
        services.AddScoped<IGroupService, GroupService>();
        return services;
    }
}

