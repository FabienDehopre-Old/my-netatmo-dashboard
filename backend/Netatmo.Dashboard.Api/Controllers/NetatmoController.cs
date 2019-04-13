using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Netatmo.Dashboard.Api.DTOs;
using Netatmo.Dashboard.Core.Options;
using Netatmo.Dashboard.Data;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Netatmo.Dashboard.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class NetatmoController : Controller
    {
        private readonly NetatmoDbContext db;
        private readonly HttpClient httpClient;
        private readonly NetatmoOptions options;
        private readonly RNGCryptoServiceProvider rng;

        public NetatmoController(NetatmoDbContext db, HttpClient httpClient, IOptionsMonitor<NetatmoOptions> options)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.options = (options ?? throw new ArgumentNullException(nameof(options))).CurrentValue;
            rng = new RNGCryptoServiceProvider();
        }

        [HttpGet]
        public ActionResult<AuthorizeUrl> BuildAuthorizeUrl(string returnUrl)
        {
            var state = GenerateRandomString(32);
            return new AuthorizeUrl { Url = $"https://api.netatmo.com/oauth2/authorize?client_id={options.ClientId}&redirect_uri={WebUtility.UrlEncode(returnUrl)}&scope=read_station&state={state}", State = state };
        }

        [HttpPost("exchange-code")]
        public async Task<ActionResult> ExchangeCodeForAccessToken([FromBody] ExchangeCode body)
        {
            var uid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await db.Users.SingleOrDefaultAsync(u => u.Uid == uid);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var data = new Dictionary<string, string>
            {
                { "grant_type", "authorization_code" },
                { "client_id", options.ClientId },
                { "client_secret", options.ClientSecret },
                { "code", body.Code },
                { "redirect_uri", body.ReturnUrl },
                { "scope", "read_station" }
            };
            using (var response = await httpClient.PostAsync("https://api.netatmo.com/oauth2/token", new FormUrlEncodedContent(data)))
            {
                response.EnsureSuccessStatusCode();
                var authorization = await response.Content.ReadAsAsync<Core.DTOs.Authorization>();
                user.AccessToken = authorization.AccessToken;
                user.RefreshToken = authorization.RefreshToken;
                user.ExpiresAt = DateTime.Now.AddSeconds(authorization.ExpiresIn);
                await db.SaveChangesAsync();
                return Accepted();
            }
        }

        private string GenerateRandomString(int length)
        {
            var buffer = new byte[length * 2];
            rng.GetNonZeroBytes(buffer);
            return Convert.ToBase64String(buffer).Replace("/", "").Replace("+", "").Replace("=", "").Substring(0, length);
        }
    }
}
