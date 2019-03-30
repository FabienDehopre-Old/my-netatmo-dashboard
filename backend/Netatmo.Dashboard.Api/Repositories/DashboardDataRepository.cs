using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Netatmo.Dashboard.Api.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Netatmo.Dashboard.Api.Repositories
{
    public class DashboardDataRepository : BaseRepository, IDashboardDataRepository
    {
        public DashboardDataRepository(NetatmoDbContext db, IHttpContextAccessor httpContextAccessor)
            : base(db, httpContextAccessor)
        {
        }

        public async Task<DashboardData[]> GetAll(string deviceId)
        {
            var uid = GetCurrentUserId();
            return await db.DashboardData.Where(dd => dd.DeviceId == deviceId && dd.Device.Station.User.Uid == uid).ToArrayAsync();
        }
    }
}
