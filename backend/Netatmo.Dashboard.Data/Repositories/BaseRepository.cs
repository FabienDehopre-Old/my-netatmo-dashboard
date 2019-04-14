using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Netatmo.Dashboard.Data.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly NetatmoDbContext db;
        private readonly IHttpContextAccessor httpContextAccessor;

        protected BaseRepository(NetatmoDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        protected string GetCurrentUserId()
        {
            return httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)
#if DEBUG
                ?? "auth0|5c3369d9b171c101904570ca"
#endif
                ;
        }
    }
}
