using Microsoft.AspNetCore.Http;
using System;

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
    }
}
