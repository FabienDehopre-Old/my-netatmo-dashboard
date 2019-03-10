using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Netatmo.Dashboard.Api.DTOs;
using Netatmo.Dashboard.Api.Models;
using Netatmo.Dashboard.Api.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DbUser = Netatmo.Dashboard.Api.Models.User;

namespace Netatmo.Dashboard.Api.Hangfire
{
    public class NetatmoTasks
    {
        private readonly HttpClient httpClient;
        private readonly NetatmoOptions options;
        private readonly NetatmoDbContext db;

        public NetatmoTasks(HttpClient httpClient, NetatmoDbContext db, IOptionsMonitor<NetatmoOptions> options)
        {
            this.options = (options ?? throw new ArgumentNullException(nameof(options))).CurrentValue;
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task FetchAndUpdate(string uid)
        {
            var user = await db.Users
                .Include(u => u.Stations)
                .ThenInclude(s => s.Devices)
                .SingleOrDefaultAsync(u => u.Uid == uid);
            if (user != null && user.Enabled)
            {
                var accessToken = await EnsureValidAccessToken(user);
                var weatherData = await GetStationData(accessToken);

                user.Units.FeelLike = (FeelLikeAlgo)weatherData.Body.User.AdminData.FeelLikeAlgo;
                user.Units.PressureUnit = (PressureUnit)weatherData.Body.User.AdminData.PressureUnit;
                user.Units.Unit = (Unit)weatherData.Body.User.AdminData.Unit;
                user.Units.WindUnit = (WindUnit)weatherData.Body.User.AdminData.WindUnit;

                foreach (var device in weatherData.Body.Devices)
                {
                    var station = user.Stations.SingleOrDefault(s => s.Devices.Select(d => d.Id).Contains(device.Id));
                    if (station == null)
                    {
                        station = new Station { Devices = new List<Models.Device>(), Location = new Location() };
                        user.Stations.Add(station);
                    }

                    station.Name = device.StationName;
                    station.Location.Altitude = device.Place.Altitude;
                    station.Location.City = device.Place.City;
                    station.Location.Country = device.Place.Country;
                    station.Location.GeoLocation = new GeoPoint { Latitude = device.Place.Location[0], Longitude = device.Place.Location[1] };
                    station.Location.Timezone = device.Place.Timezone;
                    // TODO: continue
                }

                await db.SaveChangesAsync();
            }
        }

        private async Task<string> EnsureValidAccessToken(DbUser user)
        {
            if (!user.ExpiresAt.HasValue)
            {
                return null;
            }

            if (user.ExpiresAt.Value <= DateTime.Now)
            {
                var authorization = await RefreshToken(user.RefreshToken);
                user.AccessToken = authorization.AccessToken;
                user.RefreshToken = authorization.RefreshToken;
                user.ExpiresAt = DateTime.Now.AddSeconds(authorization.ExpiresIn);
            }

            return user.AccessToken;
        }

        private async Task<Authorization> RefreshToken(string refreshToken)
        {
            var nameValueCollection = new Dictionary<string, string>
            {
                { "grant_type", "refresh_token" },
                { "client_id", options.ClientId },
                { "client_secret", options.ClientSecret },
                { "refresh_token", refreshToken }
            };
            httpClient.DefaultRequestHeaders.Authorization = null;
            var response = await httpClient.PostAsync("https://api.netatmo.com/oauth2/token", new FormUrlEncodedContent(nameValueCollection));
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<Authorization>();
        }

        private async Task<WeatherData> GetStationData(string accessToken)
        {
            var nameValueCollection = new Dictionary<string, string>
            {
                { "access_token", accessToken }
            };
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await httpClient.PostAsync("https://api.netatmo.com/api/getstationsdata", new FormUrlEncodedContent(nameValueCollection));
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<WeatherData>();
        }
    }
}
