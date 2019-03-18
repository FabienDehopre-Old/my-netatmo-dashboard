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
        public DbSet<Country> Countries { get; set; }

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
            ConfigureCountryModel(modelBuilder);
        }

        private void ConfigureUserModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(e => e.Id);
            modelBuilder.Entity<User>().Property(e => e.Id).UseSqlServerIdentityColumn();
            modelBuilder.Entity<User>().Property(e => e.Uid).IsRequired().IsUnicode().HasMaxLength(64);
            modelBuilder.Entity<User>().Property(e => e.AccessToken).IsUnicode().HasMaxLength(64);
            modelBuilder.Entity<User>().Property(e => e.ExpiresAt).IsRequired(false);
            modelBuilder.Entity<User>().Property(e => e.RefreshToken).IsUnicode().HasMaxLength(64);
            modelBuilder.Entity<User>().Property(e => e.UpdateJobId).IsUnicode().HasMaxLength(100).IsRequired(false);
            modelBuilder.Entity<User>().Property(e => e.FeelLike).IsRequired(false);
            modelBuilder.Entity<User>().Property(e => e.PressureUnit).IsRequired(false);
            modelBuilder.Entity<User>().Property(e => e.Unit).IsRequired(false);
            modelBuilder.Entity<User>().Property(e => e.WindUnit).IsRequired(false);
            modelBuilder.Entity<User>().HasIndex(e => e.Uid).IsUnique();
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Uid = "auth0|5c3369d9b171c101904570ca",
                    AccessToken = "56102b6fc6aa42f174e5d484|a2a52b7b24acfacf1718f69bbf226620",
                    ExpiresAt = DateTimeOffset.FromUnixTimeMilliseconds(1550239201963).Date,
                    RefreshToken = "56102b6fc6aa42f174e5d484|73d5b91c1cb4b021fddf91634be0a598"
                }
            );
        }

        private void ConfigureStationModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Station>().HasKey(e => e.Id);
            modelBuilder.Entity<Station>().Property(e => e.Id).UseSqlServerIdentityColumn();
            modelBuilder.Entity<Station>().Property(e => e.Name).IsRequired().IsUnicode().HasMaxLength(256);
            modelBuilder.Entity<Station>().Property(e => e.Altitude).IsRequired();
            modelBuilder.Entity<Station>().Property(e => e.City).IsRequired().IsUnicode().HasMaxLength(256);
            modelBuilder.Entity<Station>().Property(e => e.CountryCode).IsRequired().IsUnicode().IsFixedLength().HasMaxLength(2);
            modelBuilder.Entity<Station>().Property(e => e.Latitude).IsRequired().HasColumnType("decimal(9,7)");
            modelBuilder.Entity<Station>().Property(e => e.Longitude).IsRequired().HasColumnType("decimal(10,7)");
            modelBuilder.Entity<Station>().Property(e => e.Timezone).IsRequired().IsUnicode().HasMaxLength(32);
            modelBuilder.Entity<Station>().Property(e => e.StaticMap).IsRequired(false).IsUnicode().HasMaxLength(1024);
            modelBuilder.Entity<Station>()
                .HasOne(e => e.User)
                .WithMany(e => e.Stations)
                .HasForeignKey(e => e.UserId);
            modelBuilder.Entity<Station>()
                .HasOne(e => e.Country)
                .WithMany(e => e.Stations)
                .HasForeignKey(e => e.CountryCode);
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

            modelBuilder.Entity<MainDashboardData>().HasBaseType<DashboardData>().Property(t => t.Temperature).IsRequired().HasColumnType("decimal(4,1)");
            modelBuilder.Entity<MainDashboardData>().HasBaseType<DashboardData>().Property(t => t.TemperatureTrend).IsRequired();
            modelBuilder.Entity<MainDashboardData>().HasBaseType<DashboardData>().Property(x => x.TemperatureMin).IsRequired().HasColumnType("decimal(4,1)");
            modelBuilder.Entity<MainDashboardData>().HasBaseType<DashboardData>().Property(x => x.TemperatureMinTimestamp).IsRequired();
            modelBuilder.Entity<MainDashboardData>().HasBaseType<DashboardData>().Property(x => x.TemperatureMax).IsRequired().HasColumnType("decimal(4,1)");
            modelBuilder.Entity<MainDashboardData>().HasBaseType<DashboardData>().Property(x => x.TemperatureMaxTimestamp).IsRequired();
            modelBuilder.Entity<MainDashboardData>().HasBaseType<DashboardData>().Property(e => e.Pressure).IsRequired().HasColumnType("decimal(5,1)");
            modelBuilder.Entity<MainDashboardData>().HasBaseType<DashboardData>().Property(e => e.AbsolutePressure).IsRequired().HasColumnType("decimal(5,1)");
            modelBuilder.Entity<MainDashboardData>().HasBaseType<DashboardData>().Property(e => e.PressureTrend).IsRequired();
            modelBuilder.Entity<MainDashboardData>().HasBaseType<DashboardData>().Property(e => e.CO2).IsRequired();
            modelBuilder.Entity<MainDashboardData>().HasBaseType<DashboardData>().Property(e => e.Humidity).IsRequired();
            modelBuilder.Entity<MainDashboardData>().HasBaseType<DashboardData>().Property(e => e.Noise).IsRequired();

            modelBuilder.Entity<OutdoorDashboardData>().HasBaseType<DashboardData>().Property(t => t.Temperature).IsRequired().HasColumnType("decimal(4,1)");
            modelBuilder.Entity<OutdoorDashboardData>().HasBaseType<DashboardData>().Property(t => t.TemperatureTrend).IsRequired();
            modelBuilder.Entity<OutdoorDashboardData>().HasBaseType<DashboardData>().Property(x => x.TemperatureMin).IsRequired().HasColumnType("decimal(4,1)");
            modelBuilder.Entity<OutdoorDashboardData>().HasBaseType<DashboardData>().Property(x => x.TemperatureMinTimestamp).IsRequired();
            modelBuilder.Entity<OutdoorDashboardData>().HasBaseType<DashboardData>().Property(x => x.TemperatureMax).IsRequired().HasColumnType("decimal(4,1)");
            modelBuilder.Entity<OutdoorDashboardData>().HasBaseType<DashboardData>().Property(x => x.TemperatureMaxTimestamp).IsRequired();
            modelBuilder.Entity<OutdoorDashboardData>().HasBaseType<DashboardData>().Property(e => e.Humidity).IsRequired();

            modelBuilder.Entity<WindGaugeDashboardData>().HasBaseType<DashboardData>().Property(e => e.WindStrength).IsRequired().HasColumnType("decimal(5,2)"); ;
            modelBuilder.Entity<WindGaugeDashboardData>().HasBaseType<DashboardData>().Property(e => e.WindAngle).IsRequired();
            modelBuilder.Entity<WindGaugeDashboardData>().HasBaseType<DashboardData>().Property(e => e.GustStrength).IsRequired().HasColumnType("decimal(5,2)"); ;
            modelBuilder.Entity<WindGaugeDashboardData>().HasBaseType<DashboardData>().Property(e => e.GustAngle).IsRequired();

            modelBuilder.Entity<RainGaugeDashboardData>().HasBaseType<DashboardData>().Property(e => e.Rain).IsRequired().HasColumnType("decimal(5,1)");
            modelBuilder.Entity<RainGaugeDashboardData>().HasBaseType<DashboardData>().Property(e => e.Sum1H).IsRequired().HasColumnType("decimal(5,1)");
            modelBuilder.Entity<RainGaugeDashboardData>().HasBaseType<DashboardData>().Property(e => e.Sum24H).IsRequired().HasColumnType("decimal(5,1)");

            modelBuilder.Entity<IndoorDashboardData>().HasBaseType<DashboardData>().Property(t => t.Temperature).IsRequired().HasColumnType("decimal(4,1)");
            modelBuilder.Entity<IndoorDashboardData>().HasBaseType<DashboardData>().Property(t => t.TemperatureTrend).IsRequired();
            modelBuilder.Entity<IndoorDashboardData>().HasBaseType<DashboardData>().Property(x => x.TemperatureMin).IsRequired().HasColumnType("decimal(4,1)");
            modelBuilder.Entity<IndoorDashboardData>().HasBaseType<DashboardData>().Property(x => x.TemperatureMinTimestamp).IsRequired();
            modelBuilder.Entity<IndoorDashboardData>().HasBaseType<DashboardData>().Property(x => x.TemperatureMax).IsRequired().HasColumnType("decimal(4,1)");
            modelBuilder.Entity<IndoorDashboardData>().HasBaseType<DashboardData>().Property(x => x.TemperatureMaxTimestamp).IsRequired();
            modelBuilder.Entity<IndoorDashboardData>().HasBaseType<DashboardData>().Property(e => e.CO2).IsRequired();
            modelBuilder.Entity<IndoorDashboardData>().HasBaseType<DashboardData>().Property(e => e.Humidity).IsRequired();
        }

        private void ConfigureCountryModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().HasKey(e => e.Code);
            modelBuilder.Entity<Country>().Property(e => e.Code).IsRequired().IsUnicode().IsFixedLength().HasMaxLength(2);
            modelBuilder.Entity<Country>().Property(e => e.Flag).IsRequired().IsUnicode().HasMaxLength(40);
            modelBuilder.Entity<Country>().Property(n => n.NameEN).IsRequired().IsUnicode().HasMaxLength(256);
            modelBuilder.Entity<Country>().Property(n => n.NameBR).IsRequired(false).IsUnicode().HasMaxLength(256);
            modelBuilder.Entity<Country>().Property(n => n.NamePT).IsRequired(false).IsUnicode().HasMaxLength(256);
            modelBuilder.Entity<Country>().Property(n => n.NameNL).IsRequired(false).IsUnicode().HasMaxLength(256);
            modelBuilder.Entity<Country>().Property(n => n.NameHR).IsRequired(false).IsUnicode().HasMaxLength(256);
            modelBuilder.Entity<Country>().Property(n => n.NameFA).IsRequired(false).IsUnicode().HasMaxLength(256);
            modelBuilder.Entity<Country>().Property(n => n.NameDE).IsRequired(false).IsUnicode().HasMaxLength(256);
            modelBuilder.Entity<Country>().Property(n => n.NameES).IsRequired(false).IsUnicode().HasMaxLength(256);
            modelBuilder.Entity<Country>().Property(n => n.NameFR).IsRequired(false).IsUnicode().HasMaxLength(256);
            modelBuilder.Entity<Country>().Property(n => n.NameJA).IsRequired(false).IsUnicode().HasMaxLength(256);
            modelBuilder.Entity<Country>().Property(n => n.NameIT).IsRequired(false).IsUnicode().HasMaxLength(256);
            modelBuilder.Entity<Country>().HasData(GetAllCountries().GetAwaiter().GetResult());
        }

        private async Task<Country[]> GetAllCountries()
        {
            using (var http = new HttpClient())
            {
                using (var response = await http.GetAsync("https://restcountries.eu/rest/v2/all?fields=alpha2Code;name;translations;flag"))
                {
                    response.EnsureSuccessStatusCode();
                    var data = await response.Content.ReadAsAsync<RestCountry[]>();
                    return data.Select(x => new Country
                    {
                        Code = x.alpha2Code,
                        Flag = x.flag,
                        NameEN = x.name,
                        NameBR = x.translations.br,
                        NamePT = x.translations.pt,
                        NameNL = x.translations.nl,
                        NameHR = x.translations.hr,
                        NameFA = x.translations.fa,
                        NameDE = x.translations.de,
                        NameES = x.translations.es,
                        NameFR = x.translations.fr,
                        NameJA = x.translations.ja,
                        NameIT = x.translations.it
                    }).ToArray();
                }
            }
        }

        class RestCountry
        {
            public string alpha2Code { get; set; }
            public string flag { get; set; }
            public string name { get; set; }
            public Translations translations { get; set; }
        }

        class Translations
        {
            public string br { get; set; }
            public string pt { get; set; }
            public string nl { get; set; }
            public string hr { get; set; }
            public string fa { get; set; }
            public string de { get; set; }
            public string es { get; set; }
            public string fr { get; set; }
            public string ja { get; set; }
            public string it { get; set; }
        }
    }
}
