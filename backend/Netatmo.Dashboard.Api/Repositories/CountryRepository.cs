using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Netatmo.Dashboard.Api.Models;

namespace Netatmo.Dashboard.Api.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly NetatmoDbContext db;

        public CountryRepository(NetatmoDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<Country[]> GetAll()
        {
            return await db.Countries.ToArrayAsync();
        }

        public async Task<Country> GetOne(string alpha2Code)
        {
            return await db.Countries.SingleOrDefaultAsync(p => p.Code == alpha2Code);
        }
    }
}
