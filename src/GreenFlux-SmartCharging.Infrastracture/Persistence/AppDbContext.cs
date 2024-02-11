using GreenFlux_SmartCharging.Domain.Entities;
using GreenFlux_SmartCharging.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace GreenFlux_SmartCharging.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Group>? Groups { get; set; }   
    public DbSet<ChargeStation>? ChargeStations { get; set; }
    public DbSet<Connector>? Connectors { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new ChargeStationEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ConnectorEntityConfiguration());
        modelBuilder.ApplyConfiguration(new GroupEntityConfiguration());
    }
}

