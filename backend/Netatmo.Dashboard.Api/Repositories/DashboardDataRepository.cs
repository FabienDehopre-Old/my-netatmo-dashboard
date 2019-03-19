using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Netatmo.Dashboard.Api.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Netatmo.Dashboard.Api.Repositories
{
    public class DashboardDataRepository : IDashboardDataRepository
    {
        private readonly NetatmoDbContext db;
        private readonly IHttpContextAccessor httpContextAccessor;

        public DashboardDataRepository(NetatmoDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task<DashboardData[]> GetAll(string deviceId)
        {
            return await db.DashboardData.Where(dd => dd.DeviceId == deviceId).ToArrayAsync();
        }
    }
}
