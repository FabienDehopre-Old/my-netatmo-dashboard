using Microsoft.EntityFrameworkCore;
using Netatmo.Dashboard.Application.Interfaces;
using Netatmo.Dashboard.Domain.Entities;

namespace Netatmo.Dashboard.Persistence
{
    public class NetatmoDbContext : DbContext, INetatmoDbContext
    {
        public NetatmoDbContext(DbContextOptions<NetatmoDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<DashboardData> DashboardData { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NetatmoDbContext).Assembly);
        }
    }
}
