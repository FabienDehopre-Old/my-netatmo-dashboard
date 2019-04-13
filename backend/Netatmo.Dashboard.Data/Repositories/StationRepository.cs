using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Netatmo.Dashboard.Core.Data;
using Netatmo.Dashboard.Core.Models;

namespace Netatmo.Dashboard.Data.Repositories
{
    public class StationRepository : BaseRepository, IStationRepository
    {
        public StationRepository(NetatmoDbContext db, IHttpContextAccessor httpContextAccessor)
            : base(db, httpContextAccessor)
        {
        }

        public async Task<Station[]> GetAll()
        {
            var uid = GetCurrentUserId();
            return await db.Stations.Where(s => s.User.Uid == uid).ToArrayAsync();
        }

        public async Task<Station[]> GetAll(string countryCode)
        {
            var uid = GetCurrentUserId();
            return await db.Stations.Where(s => s.User.Uid == uid && s.CountryCode == countryCode).ToArrayAsync();
        }

        public async Task<Station> GetOne(int id)
        {
            var uid = GetCurrentUserId();
            return await db.Stations.SingleOrDefaultAsync(s => s.Id == id && s.User.Uid == uid);
        }
    }
}
