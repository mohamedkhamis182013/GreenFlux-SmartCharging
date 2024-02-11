using GreenFlux_SmartCharging.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenFlux_SmartCharging.Infrastructure.Persistence.Configurations;
public class ChargeStationEntityConfiguration : IEntityTypeConfiguration<ChargeStation>
{
    public void Configure(EntityTypeBuilder<ChargeStation> builder)
    {
        builder.ToTable("ChargeStation").HasKey(cs => cs.Id);
        builder.Property(cs => cs.Id).HasDefaultValueSql("NEWID()");
        builder.Property(e => e.Name).IsRequired();
        builder.HasMany(cs => cs.Connectors)
            .WithOne().HasForeignKey(c => c.ChargeStationId);
    }
}

