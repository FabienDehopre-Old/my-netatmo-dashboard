using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Netatmo.Dashboard.Domain.Entities;

namespace Netatmo.Dashboard.Persistence.Configurations
{
    public class DeviceConfiguration : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).IsRequired().IsUnicode().HasMaxLength(17);
            builder.Property(e => e.Name).IsRequired().IsUnicode().HasMaxLength(256);
            builder.Property(e => e.Firmware).IsRequired();
            builder.HasOne(e => e.Station).WithMany(e => e.Devices).HasForeignKey(e => e.StationId);
            builder.HasDiscriminator<string>("module_type").HasValue<MainDevice>("main").HasValue<ModuleDevice>("module");
        }
    }

    public class MainDeviceConfiguration : IEntityTypeConfiguration<MainDevice>
    {
        public void Configure(EntityTypeBuilder<MainDevice> builder)
        {
            builder.HasBaseType<Device>().Property(e => e.WifiStatus).IsRequired();
        }
    }

    public class ModuleDeviceConfiguration : IEntityTypeConfiguration<ModuleDevice>
    {
        public void Configure(EntityTypeBuilder<ModuleDevice> builder)
        {
            builder.HasBaseType<Device>().Property(e => e.RfStatus).IsRequired();
            builder.HasBaseType<Device>().Property(b => b.BatteryPercent).IsRequired();
            builder.HasBaseType<Device>().Property(b => b.BatteryVp).IsRequired();
            builder.HasBaseType<Device>().Property(e => e.Type).IsRequired();
        }
    }
}
