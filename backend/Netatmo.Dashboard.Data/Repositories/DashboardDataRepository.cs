using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Netatmo.Dashboard.Core.Data;
using Netatmo.Dashboard.Core.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Netatmo.Dashboard.Data.Repositories
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
