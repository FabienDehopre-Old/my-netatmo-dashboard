using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Netatmo.Dashboard.Api.DTOs;
using Netatmo.Dashboard.Api.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Netatmo.Dashboard.Api
{
    public class NetatmoHttpClient
    {
        private readonly HttpClient httpClient;
        private readonly NetatmoOptions options;
        private readonly NetatmoDbContext db;

        public NetatmoHttpClient(HttpClient httpClient, NetatmoDbContext db, IOptions<NetatmoOptions> options)
        {
            this.options = options.Value;
            this.db = db;
            this.httpClient = httpClient;
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<Authorization> RefreshToken(string refreshToken, string clientId, string clientSecret)
        {
            var nameValueCollection = new Dictionary<string, string>
            {
                { "grant_type", "refresh_token" },
                { "client_id", clientId },
                { "client_secret", clientSecret },
                { "refresh_token", refreshToken }
            };
            httpClient.DefaultRequestHeaders.Authorization = null;
            var response = await httpClient.PostAsync("https://api.netatmo.com/oauth2/token", new FormUrlEncodedContent(nameValueCollection));
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<Authorization>();
        }

        public async Task<WeatherData> GetStationData(string accessToken)
        {
            var nameValueCollection = new Dictionary<string, string>
            {
                { "access_token", accessToken }
            };
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await httpClient.PostAsync("https://api.netatmo.com/api/getstationsdata", new FormUrlEncodedContent(nameValueCollection));
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<WeatherData>();    // TODO: create a class for the data
        }

        public async Task<string> GetAccessToken(string uid)
        {
            var user = await db.Users.SingleOrDefaultAsync(u => u.Uid == uid);
            if (user == null)
            {
                throw new ApplicationException($"User {uid} does not exist in DB.");
            }

            if (!user.ExpiresAt.HasValue)
            {
                throw new ApplicationException($"User {uid} is not connected to Netatmo yet.");
            }

            if (user.ExpiresAt <= DateTime.Now)
            {
                var authorization = await RefreshToken(user.RefreshToken, options.ClientId, options.ClientSecret);
                user.AccessToken = authorization.AccessToken;
                user.RefreshToken = authorization.RefreshToken;
                user.ExpiresAt = DateTime.Now.AddSeconds(authorization.ExpiresIn);
                await db.SaveChangesAsync();
            }

            return user.AccessToken;
        }
    }
}
