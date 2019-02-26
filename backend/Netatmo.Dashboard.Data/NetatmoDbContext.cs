using Microsoft.EntityFrameworkCore;
using Netatmo.Dashboard.Core.Models;

namespace Netatmo.Dashboard.Data
{
    public class NetatmoDbContext : DbContext
    {
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
    }
}
