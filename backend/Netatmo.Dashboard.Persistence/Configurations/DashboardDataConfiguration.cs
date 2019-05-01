using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Netatmo.Dashboard.Domain.Entities;

namespace Netatmo.Dashboard.Persistence.Configurations
{
    public class DashboardDataConfiguration : IEntityTypeConfiguration<DashboardData>
    {
        public void Configure(EntityTypeBuilder<DashboardData> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).UseSqlServerIdentityColumn();
            builder.Property(e => e.TimeUtc).IsRequired();
            builder.HasOne(e => e.Device).WithMany(e => e.DashboardData).HasForeignKey(e => e.DeviceId);
            builder
                .HasDiscriminator<string>("type")
                .HasValue<MainDashboardData>("NAMain")
                .HasValue<OutdoorDashboardData>("NAModule1")
                .HasValue<WindGaugeDashboardData>("NAModule2")
                .HasValue<RainGaugeDashboardData>("NAModule3")
                .HasValue<IndoorDashboardData>("NAModule4");
        }
    }

    public class MainDashboardDataConfiguration : IEntityTypeConfiguration<MainDashboardData>
    {
        public void Configure(EntityTypeBuilder<MainDashboardData> builder)
        {
            builder.HasBaseType<DashboardData>().Property(t => t.Temperature).IsRequired().HasColumnType("decimal(4,1)");
            builder.HasBaseType<DashboardData>().Property(t => t.TemperatureTrend).IsRequired();
            builder.HasBaseType<DashboardData>().Property(x => x.TemperatureMin).IsRequired().HasColumnType("decimal(4,1)");
            builder.HasBaseType<DashboardData>().Property(x => x.TemperatureMinTimestamp).IsRequired();
            builder.HasBaseType<DashboardData>().Property(x => x.TemperatureMax).IsRequired().HasColumnType("decimal(4,1)");
            builder.HasBaseType<DashboardData>().Property(x => x.TemperatureMaxTimestamp).IsRequired();
            builder.HasBaseType<DashboardData>().Property(e => e.Pressure).IsRequired().HasColumnType("decimal(5,1)");
            builder.HasBaseType<DashboardData>().Property(e => e.AbsolutePressure).IsRequired().HasColumnType("decimal(5,1)");
            builder.HasBaseType<DashboardData>().Property(e => e.PressureTrend).IsRequired();
            builder.HasBaseType<DashboardData>().Property(e => e.CO2).IsRequired();
            builder.HasBaseType<DashboardData>().Property(e => e.Humidity).IsRequired();
            builder.HasBaseType<DashboardData>().Property(e => e.Noise).IsRequired();
        }
    }

    public class OutdoorDashboardDataConfiguration : IEntityTypeConfiguration<OutdoorDashboardData>
    {
        public void Configure(EntityTypeBuilder<OutdoorDashboardData> builder)
        {
            builder.HasBaseType<DashboardData>().Property(t => t.Temperature).IsRequired().HasColumnType("decimal(4,1)");
            builder.HasBaseType<DashboardData>().Property(t => t.TemperatureTrend).IsRequired();
            builder.HasBaseType<DashboardData>().Property(x => x.TemperatureMin).IsRequired().HasColumnType("decimal(4,1)");
            builder.HasBaseType<DashboardData>().Property(x => x.TemperatureMinTimestamp).IsRequired();
            builder.HasBaseType<DashboardData>().Property(x => x.TemperatureMax).IsRequired().HasColumnType("decimal(4,1)");
            builder.HasBaseType<DashboardData>().Property(x => x.TemperatureMaxTimestamp).IsRequired();
            builder.HasBaseType<DashboardData>().Property(e => e.Humidity).IsRequired();
        }
    }

    public class WindGaugeDashboardDataConfiguration : IEntityTypeConfiguration<WindGaugeDashboardData>
    {
        public void Configure(EntityTypeBuilder<WindGaugeDashboardData> builder)
        {
            builder.HasBaseType<DashboardData>().Property(e => e.WindStrength).IsRequired().HasColumnType("decimal(5,2)"); ;
            builder.HasBaseType<DashboardData>().Property(e => e.WindAngle).IsRequired();
            builder.HasBaseType<DashboardData>().Property(e => e.GustStrength).IsRequired().HasColumnType("decimal(5,2)"); ;
            builder.HasBaseType<DashboardData>().Property(e => e.GustAngle).IsRequired();
        }
    }

    public class RainGaugeDashboardDataConfiguration : IEntityTypeConfiguration<RainGaugeDashboardData>
    {
        public void Configure(EntityTypeBuilder<RainGaugeDashboardData> builder)
        {
            builder.HasBaseType<DashboardData>().Property(e => e.Rain).IsRequired().HasColumnType("decimal(5,1)");
            builder.HasBaseType<DashboardData>().Property(e => e.Sum1H).IsRequired().HasColumnType("decimal(5,1)");
            builder.HasBaseType<DashboardData>().Property(e => e.Sum24H).IsRequired().HasColumnType("decimal(5,1)");
        }
    }

    public class IndoorDashboardDataConfiguration : IEntityTypeConfiguration<IndoorDashboardData>
    {
        public void Configure(EntityTypeBuilder<IndoorDashboardData> builder)
        {
            builder.HasBaseType<DashboardData>().Property(t => t.Temperature).IsRequired().HasColumnType("decimal(4,1)");
            builder.HasBaseType<DashboardData>().Property(t => t.TemperatureTrend).IsRequired();
            builder.HasBaseType<DashboardData>().Property(x => x.TemperatureMin).IsRequired().HasColumnType("decimal(4,1)");
            builder.HasBaseType<DashboardData>().Property(x => x.TemperatureMinTimestamp).IsRequired();
            builder.HasBaseType<DashboardData>().Property(x => x.TemperatureMax).IsRequired().HasColumnType("decimal(4,1)");
            builder.HasBaseType<DashboardData>().Property(x => x.TemperatureMaxTimestamp).IsRequired();
            builder.HasBaseType<DashboardData>().Property(e => e.CO2).IsRequired();
            builder.HasBaseType<DashboardData>().Property(e => e.Humidity).IsRequired();
        }
    }
}
