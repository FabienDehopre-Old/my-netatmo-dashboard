using Microsoft.EntityFrameworkCore;
using Netatmo.Dashboard.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Netatmo.Dashboard.Application.Interfaces
{
    public interface INetatmoDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Station> Stations { get; set; }
        DbSet<Device> Devices { get; set; }
        DbSet<DashboardData> DashboardData { get; set; }
        DbSet<Country> Countries { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
