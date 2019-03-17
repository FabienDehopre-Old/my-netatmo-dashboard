using System;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Netatmo.Dashboard.Api.Options;

namespace Netatmo.Dashboard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly NetatmoDbContext db;
        private readonly HttpClient httpClient;
        private readonly Auth0Options options;

        public UserController(NetatmoDbContext db, HttpClient httpClient, IOptionsMonitor<Auth0Options> options)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.options = (options ?? throw new ArgumentNullException(nameof(options))).CurrentValue;
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

        [HttpPost("verification-email")]
        public async Task ResendVerificationEmail()
        {
            var uid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var body = new { client_id = "", user_id = uid };
            // TODO: Get acces token for Auth0 Management API v2
            using (var response = await httpClient.PostAsJsonAsync($"https://{options.Domain}/api/v2/jobs/verification-email", body))
            {
                response.EnsureSuccessStatusCode();
            }
        }
    }
}