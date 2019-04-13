using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Netatmo.Dashboard.Core.Data;
using Netatmo.Dashboard.Core.Models;

namespace Netatmo.Dashboard.Data.Repositories
{
    public class DeviceRepository : BaseRepository, IDeviceRepository
    {
        public DeviceRepository(NetatmoDbContext db, IHttpContextAccessor httpContextAccessor)
            : base(db, httpContextAccessor)
        {
        }

        public async Task<Device[]> GetAll(int stationId)
        {
            var uid = GetCurrentUserId();
            return await db.Devices.Where(d => d.StationId == stationId && d.Station.User.Uid == uid).ToArrayAsync();
        }

        public async Task<Device> GetOne(string deviceId)
        {
            var uid = GetCurrentUserId();
            return await db.Devices.SingleOrDefaultAsync(d => d.Id == deviceId && d.Station.User.Uid == uid);
        }
    }
}
