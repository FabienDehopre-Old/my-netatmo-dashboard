using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Netatmo.Dashboard.Api.Models;

namespace Netatmo.Dashboard.Api.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly NetatmoDbContext db;
        private readonly IHttpContextAccessor httpContextAccessor;

        public DeviceRepository(NetatmoDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task<Device[]> GetAll(int stationId)
        {
            return await db.Devices.Where(d => d.StationId == stationId).ToArrayAsync();
        }

        public async Task<Device> GetOne(string deviceId)
        {
            return await db.Devices.SingleOrDefaultAsync(d => d.Id == deviceId);
        }
    }
}
