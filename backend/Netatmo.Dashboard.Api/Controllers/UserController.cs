using System;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Netatmo.Dashboard.Api.DTOs;
using Netatmo.Dashboard.Api.Hangfire;
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

        [HttpGet]
        public async Task<ActionResult> GetProfile()
        {
            var uid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await db.Users.SingleOrDefaultAsync(u => u.Uid == uid);
            if (user == null)
            {
                return NotFound();
            }

            // TODO: Create a DTO?
            return Ok(new
            {
                user.FeelLike,
                user.PressureUnit,
                user.Unit,
                user.WindUnit,
                Enabled = !string.IsNullOrWhiteSpace(user.UpdateJobId)
                // TODO: add more data to user profile
            });
        }

        [HttpPost("ensure")]
        public async Task<ActionResult<bool>> EnsureCreated()
        {
            var uid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await db.Users.SingleOrDefaultAsync(u => u.Uid == uid);
            if (user == null)
            {
                user = new Models.User
                {
                    Uid = uid,
                };
                await db.Users.AddAsync(user);
                await db.SaveChangesAsync();
                return CreatedAtAction("GetUserProfile", false);
            }

            return !string.IsNullOrWhiteSpace(user.AccessToken) && !string.IsNullOrWhiteSpace(user.RefreshToken) && user.ExpiresAt.HasValue;
        }

        [HttpPost("verification-email")]
        public async Task<ActionResult<string>> ResendVerificationEmail()
        {
            var uid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var token = await GetManagementApiAccessToken();
            var client = new ManagementApiClient(token, options.Domain);
            var job = await client.Jobs.SendVerificationEmailAsync(new VerifyEmailJobRequestWithClientId
            {
                UserId = uid,
                ClientId = options.ClientId
            });
            return job.Id;
        }

        [HttpPost("toggle-update-job")]
        public async Task<ActionResult<bool>> ToggleFetchAndUpdateJob()
        {
            var uid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await db.Users.SingleOrDefaultAsync(u => u.Uid == uid);
            if (user == null)
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(user.UpdateJobId))
            {
                // Enable the refresh of the data from Netatmo API
                if (string.IsNullOrWhiteSpace(user.RefreshToken))
                {
                    return Conflict("You have not authorized the application to access the Netatmo API on your behalf.");
                }

                BackgroundJob.Enqueue<NetatmoTasks>(x => x.FetchAndUpdate(user.Uid));
                var jobId = $"fetch-update-{Guid.NewGuid()}";
                RecurringJob.AddOrUpdate<NetatmoTasks>(jobId, x => x.FetchAndUpdate(user.Uid), Cron.MinuteInterval(15));
                user.UpdateJobId = jobId;
            }
            else
            {
                // Disable the refresh of the data from Netatmo API
                RecurringJob.RemoveIfExists(user.UpdateJobId);
                user.UpdateJobId = null;
            }

            await db.SaveChangesAsync();
            return Accepted(!string.IsNullOrWhiteSpace(user.UpdateJobId));
        }

        private async Task<string> GetManagementApiAccessToken()
        {
            var body = new
            {
                client_id = options.ClientId,
                client_secret = options.ClientSecret,
                audience = $"https://{options.Domain}/api/v2/",
                grant_type = "client_credentials"
            };
            using (var response = await httpClient.PostAsJsonAsync($"https://{options.Domain}/oauth/token", body))
            {
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsAsync<ManagementApiToken>();
                return data.AccessToken;
            }
        }
    }
}