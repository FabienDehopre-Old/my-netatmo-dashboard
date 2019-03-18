using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Netatmo.Dashboard.Api.Models;

namespace Netatmo.Dashboard.Api.Repositories
{
    public class StationRepository : IStationRepository
    {
        private readonly NetatmoDbContext db;
        private readonly IHttpContextAccessor httpContextAccessor;

        public StationRepository(NetatmoDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task<Station[]> GetAll()
        {
            var uid = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await db.Stations.Where(s => s.User.Uid == uid).ToArrayAsync();
        }

        public async Task<Station[]> GetAll(string countryCode)
        {
            var uid = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await db.Stations.Where(s => s.User.Uid == uid && s.CountryCode == countryCode).ToArrayAsync();
        }

        public async Task<Station> GetOne(int id)
        {
            var uid = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await db.Stations.SingleOrDefaultAsync(s => s.Id == id && s.User.Uid == uid);
        }
    }
}
