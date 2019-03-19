using System;
using Microsoft.AspNetCore.Http;

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
    }
}
