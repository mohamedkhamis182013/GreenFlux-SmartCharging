using GreenFlux_SmartCharging.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenFlux_SmartCharging.Infrastructure.Persistence.Configurations;

public class ConnectorEntityConfiguration : IEntityTypeConfiguration<Connector>
{
    public void Configure(EntityTypeBuilder<Connector> builder)
    {
        builder.ToTable("Connector").HasKey(cs => cs.Id);

        builder.Property(cs => cs.Id).UseIdentityColumn().ValueGeneratedOnAdd();
    }
}

