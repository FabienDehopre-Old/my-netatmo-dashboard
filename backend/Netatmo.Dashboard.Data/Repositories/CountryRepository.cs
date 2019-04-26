using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Netatmo.Dashboard.Core.Data;
using Netatmo.Dashboard.Core.Models;

namespace Netatmo.Dashboard.Data.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly NetatmoDbContext db;

        public CountryRepository(NetatmoDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<Country[]> GetAll(int? first, string afterCode, CancellationToken cancellationToken)
        {
            IQueryable<Country> query = db.Countries.OrderBy(c => c.Code);
            if (!string.IsNullOrWhiteSpace(afterCode))
            {
                query = query.Where(c => string.Compare(c.Code, afterCode) > 0);
            }

            if (first.HasValue)
            {
                query = query.Take(first.Value);
            }

            return await query.ToArrayAsync(cancellationToken);
        }

        public async Task<Country[]> GetAllReverse(int? last, string beforeCode, CancellationToken cancellationToken)
        {
            IQueryable<Country> query = db.Countries.OrderByDescending(c => c.Code);
            if (!string.IsNullOrWhiteSpace(beforeCode))
            {
                query = query.Where(c => string.Compare(c.Code, beforeCode) < 0);
            }

            if (last.HasValue)
            {
                query = query.Take(last.Value);
            }

            return await query.OrderBy(c => c.Code).ToArrayAsync(cancellationToken);
        }

        public async Task<bool> GetHasNextPage(int? first, string afterCode, CancellationToken cancellationToken)
        {
            IQueryable<Country> query = db.Countries.OrderBy(c => c.Code);
            if (!string.IsNullOrWhiteSpace(afterCode))
            {
                query = query.Where(c => string.Compare(c.Code, afterCode) > 0);
            }

            return await query.Skip(first.Value).AnyAsync(cancellationToken);
        }

        public async Task<bool> GetHasPreviousPage(int? last, string beforeCode, CancellationToken cancellationToken)
        {
            IQueryable<Country> query = db.Countries.OrderByDescending(c => c.Code);
            if (!string.IsNullOrWhiteSpace(beforeCode))
            {
                query = query.Where(c => string.Compare(c.Code, beforeCode) < 0);
            }

            return await query.Skip(last.Value).AnyAsync(cancellationToken);
        }

        public async Task<Country> GetOne(string alpha2Code, CancellationToken cancellationToken)
        {
            return await db.Countries.SingleOrDefaultAsync(p => p.Code == alpha2Code, cancellationToken);
        }

        public async Task<int> GetTotalCount(CancellationToken cancellationToken)
        {
            return await db.Countries.CountAsync(cancellationToken);
        }
    }
}
