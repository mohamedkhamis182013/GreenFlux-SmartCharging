using GreenFlux_SmartCharging.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenFlux_SmartCharging.Infrastructure.Persistence.Configurations;

public class GroupEntityConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.ToTable("Group").HasKey(g => g.Id);
        builder.Property(g => g.Id).HasDefaultValueSql("NEWID()");
        builder.Property(g => g.Name).IsRequired();
        builder.Property(g => g.Capacity).IsRequired();
        builder.HasMany(g => g.ChargeStations)
            .WithOne().HasForeignKey(cs => cs.GroupId);
    }
}

