using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Netatmo.Dashboard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly NetatmoDbContext db;

        public UserController(NetatmoDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        [HttpGet("ensure")]
        public async Task<bool> EnsureUserCreated()
        {
            var uid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await db.Users.SingleOrDefaultAsync(u => u.Uid == uid);
            if (user == null)
            {
                user = new Models.User
                {
                    Uid = uid,
                    Enabled = true,
                };
                await db.Users.AddAsync(user);
                await db.SaveChangesAsync();
            }

            return user != null && !string.IsNullOrWhiteSpace(user.AccessToken) && !string.IsNullOrWhiteSpace(user.RefreshToken) && user.ExpiresAt.HasValue;
        }
    }
}