using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Netatmo.Dashboard.Api.Models;

namespace Netatmo.Dashboard.Api
{
    public class NetatmoDbContext : DbContext
    {
        public NetatmoDbContext() : base()
        {
        }

        public NetatmoDbContext(DbContextOptions options)
            : base(options)
        {
            // these are mutually exclusive, migrations cannot be used with EnsureCreated()
            // Database.EnsureCreated();
            Database.Migrate();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<DashboardData> DashboardData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureUserModel(modelBuilder);
            ConfigureStationModel(modelBuilder);
            ConfigureDeviceModel(modelBuilder);
            ConfigureDashboardDataModel(modelBuilder);
        }

        private void ConfigureUserModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(e => e.Id);
            modelBuilder.Entity<User>().Property(e => e.Id).UseSqlServerIdentityColumn();
            modelBuilder.Entity<User>().Property(e => e.Uid).IsRequired().IsUnicode().HasMaxLength(64);
            modelBuilder.Entity<User>().Property(e => e.Enabled).IsRequired().HasDefaultValue(false);
            modelBuilder.Entity<User>().Property(e => e.AccessToken).IsUnicode().HasMaxLength(64);
            modelBuilder.Entity<User>().Property(e => e.ExpiresAt).IsRequired(false);
            modelBuilder.Entity<User>().Property(e => e.RefreshToken).IsUnicode().HasMaxLength(64);
            modelBuilder.Entity<User>().Property(e => e.UpdateJobId).IsUnicode().HasMaxLength(100).IsRequired(false);
            modelBuilder.Entity<User>().Property(e => e.FeelLike).IsRequired(false);
            modelBuilder.Entity<User>().Property(e => e.PressureUnit).IsRequired(false);
            modelBuilder.Entity<User>().Property(e => e.Unit).IsRequired(false);
            modelBuilder.Entity<User>().Property(e => e.WindUnit).IsRequired(false);
            modelBuilder.Entity<User>().HasIndex(e => e.Uid).IsUnique();
        }

        private void ConfigureStationModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Station>().HasKey(e => e.Id);
            modelBuilder.Entity<Station>().Property(e => e.Id).UseSqlServerIdentityColumn();
            modelBuilder.Entity<Station>().Property(e => e.Name).IsRequired().IsUnicode().HasMaxLength(256);
            modelBuilder.Entity<Station>().Property(e => e.Altitude).IsRequired();
            modelBuilder.Entity<Station>().Property(e => e.City).IsRequired().IsUnicode().HasMaxLength(256);
            modelBuilder.Entity<Station>().Property(e => e.CountryCode).IsRequired().IsUnicode().IsFixedLength().HasMaxLength(2);
            modelBuilder.Entity<Station>().Property(e => e.Latitude).IsRequired();
            modelBuilder.Entity<Station>().Property(e => e.Longitude).IsRequired();
            modelBuilder.Entity<Station>().Property(e => e.Timezone).IsRequired().IsUnicode().HasMaxLength(32);
            modelBuilder.Entity<Station>().Property(e => e.StaticMap).IsRequired(false).IsUnicode().HasMaxLength(1024);
            modelBuilder.Entity<Station>()
                .HasOne(e => e.User)
                .WithMany(e => e.Stations)
                .HasForeignKey(e => e.UserId);
        }

        private void ConfigureDeviceModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>().HasKey(e => e.Id);
            modelBuilder.Entity<Device>().Property(e => e.Id).IsRequired().IsUnicode().HasMaxLength(17);
            modelBuilder.Entity<Device>().Property(e => e.Name).IsRequired().IsUnicode().HasMaxLength(256);
            modelBuilder.Entity<Device>().Property(e => e.Firmware).IsRequired();
            modelBuilder.Entity<Device>()
                .HasOne(e => e.Station)
                .WithMany(e => e.Devices)
                .HasForeignKey(e => e.StationId);
            modelBuilder.Entity<Device>()
                .HasDiscriminator<string>("module_type")
                .HasValue<MainDevice>("main")
                .HasValue<ModuleDevice>("module");
            modelBuilder.Entity<MainDevice>().HasBaseType<Device>().Property(e => e.WifiStatus).IsRequired();
            modelBuilder.Entity<ModuleDevice>().HasBaseType<Device>().Property(e => e.RfStatus).IsRequired();
            modelBuilder.Entity<ModuleDevice>().HasBaseType<Device>().Property(b => b.BatteryPercent).IsRequired();
            modelBuilder.Entity<ModuleDevice>().HasBaseType<Device>().Property(b => b.BatteryVp).IsRequired();
            modelBuilder.Entity<ModuleDevice>().HasBaseType<Device>().Property(e => e.Type).IsRequired();
        }

        private void ConfigureDashboardDataModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DashboardData>().HasKey(e => e.Id);
            modelBuilder.Entity<DashboardData>().Property(e => e.Id).UseSqlServerIdentityColumn();
            modelBuilder.Entity<DashboardData>().Property(e => e.TimeUtc).IsRequired();
            modelBuilder.Entity<DashboardData>()
                .HasOne(e => e.Device)
                .WithMany(e => e.DashboardData)
                .HasForeignKey(e => e.DeviceId);
            modelBuilder.Entity<DashboardData>()
                .HasDiscriminator<string>("type")
                .HasValue<MainDashboardData>("NAMain")
                .HasValue<OutdoorDashboardData>("NAModule1")
                .HasValue<WindGaugeDashboardData>("NAModule2")
                .HasValue<RainGaugeDashboardData>("NAModule3")
                .HasValue<IndoorDashboardData>("NAModule4");

            modelBuilder.Entity<MainDashboardData>().HasBaseType<DashboardData>().Property(t => t.Temperature).IsRequired();
            modelBuilder.Entity<MainDashboardData>().HasBaseType<DashboardData>().Property(t => t.TemperatureTrend).IsRequired();
            modelBuilder.Entity<MainDashboardData>().HasBaseType<DashboardData>().Property(x => x.TemperatureMin).IsRequired();
            modelBuilder.Entity<MainDashboardData>().HasBaseType<DashboardData>().Property(x => x.TemperatureMinTimestamp).IsRequired();
            modelBuilder.Entity<MainDashboardData>().HasBaseType<DashboardData>().Property(x => x.TemperatureMax).IsRequired();
            modelBuilder.Entity<MainDashboardData>().HasBaseType<DashboardData>().Property(x => x.TemperatureMaxTimestamp).IsRequired();
            modelBuilder.Entity<MainDashboardData>().HasBaseType<DashboardData>().Property(e => e.Pressure).IsRequired();
            modelBuilder.Entity<MainDashboardData>().HasBaseType<DashboardData>().Property(e => e.AbsolutePressure).IsRequired();
            modelBuilder.Entity<MainDashboardData>().HasBaseType<DashboardData>().Property(e => e.PressureTrend).IsRequired();
            modelBuilder.Entity<MainDashboardData>().HasBaseType<DashboardData>().Property(e => e.CO2).IsRequired();
            modelBuilder.Entity<MainDashboardData>().HasBaseType<DashboardData>().Property(e => e.Humidity).IsRequired();
            modelBuilder.Entity<MainDashboardData>().HasBaseType<DashboardData>().Property(e => e.Noise).IsRequired();

            modelBuilder.Entity<OutdoorDashboardData>().HasBaseType<DashboardData>().Property(t => t.Temperature).IsRequired();
            modelBuilder.Entity<OutdoorDashboardData>().HasBaseType<DashboardData>().Property(t => t.TemperatureTrend).IsRequired();
            modelBuilder.Entity<OutdoorDashboardData>().HasBaseType<DashboardData>().Property(x => x.TemperatureMin).IsRequired();
            modelBuilder.Entity<OutdoorDashboardData>().HasBaseType<DashboardData>().Property(x => x.TemperatureMinTimestamp).IsRequired();
            modelBuilder.Entity<OutdoorDashboardData>().HasBaseType<DashboardData>().Property(x => x.TemperatureMax).IsRequired();
            modelBuilder.Entity<OutdoorDashboardData>().HasBaseType<DashboardData>().Property(x => x.TemperatureMaxTimestamp).IsRequired();
            modelBuilder.Entity<OutdoorDashboardData>().HasBaseType<DashboardData>().Property(e => e.Humidity).IsRequired();

            modelBuilder.Entity<WindGaugeDashboardData>().HasBaseType<DashboardData>().Property(e => e.WindStrength).IsRequired();
            modelBuilder.Entity<WindGaugeDashboardData>().HasBaseType<DashboardData>().Property(e => e.WindAngle).IsRequired();
            modelBuilder.Entity<WindGaugeDashboardData>().HasBaseType<DashboardData>().Property(e => e.GustStrength).IsRequired();
            modelBuilder.Entity<WindGaugeDashboardData>().HasBaseType<DashboardData>().Property(e => e.GustAngle).IsRequired();

            modelBuilder.Entity<RainGaugeDashboardData>().HasBaseType<DashboardData>().Property(e => e.Rain).IsRequired();
            modelBuilder.Entity<RainGaugeDashboardData>().HasBaseType<DashboardData>().Property(e => e.Sum1H).IsRequired();
            modelBuilder.Entity<RainGaugeDashboardData>().HasBaseType<DashboardData>().Property(e => e.Sum24H).IsRequired();

            modelBuilder.Entity<IndoorDashboardData>().HasBaseType<DashboardData>().Property(t => t.Temperature).IsRequired();
            modelBuilder.Entity<IndoorDashboardData>().HasBaseType<DashboardData>().Property(t => t.TemperatureTrend).IsRequired();
            modelBuilder.Entity<IndoorDashboardData>().HasBaseType<DashboardData>().Property(x => x.TemperatureMin).IsRequired();
            modelBuilder.Entity<IndoorDashboardData>().HasBaseType<DashboardData>().Property(x => x.TemperatureMinTimestamp).IsRequired();
            modelBuilder.Entity<IndoorDashboardData>().HasBaseType<DashboardData>().Property(x => x.TemperatureMax).IsRequired();
            modelBuilder.Entity<IndoorDashboardData>().HasBaseType<DashboardData>().Property(x => x.TemperatureMaxTimestamp).IsRequired();
            modelBuilder.Entity<IndoorDashboardData>().HasBaseType<DashboardData>().Property(e => e.CO2).IsRequired();
            modelBuilder.Entity<IndoorDashboardData>().HasBaseType<DashboardData>().Property(e => e.Humidity).IsRequired();
        }
    }
}
