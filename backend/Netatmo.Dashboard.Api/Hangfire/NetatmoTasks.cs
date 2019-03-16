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

        public void FetchAndUpdate(string uid)
        {
            var task = FetchAndUpdateAsync(uid);
            task.GetAwaiter().GetResult();
        }

        private async Task FetchAndUpdateAsync(string uid)
        {
            var user = await db.Users
                .Include(u => u.Stations)
                .ThenInclude(s => s.Devices)
                .SingleOrDefaultAsync(u => u.Uid == uid);
            if (user != null && user.Enabled)
            {
                var accessToken = await EnsureValidAccessToken(user);
                var weatherData = await GetStationData(accessToken);

                user.FeelLike = (FeelLikeAlgo)weatherData.Body.User.AdminData.FeelLikeAlgo;
                user.PressureUnit = (PressureUnit)weatherData.Body.User.AdminData.PressureUnit;
                user.Unit = (Unit)weatherData.Body.User.AdminData.Unit;
                user.WindUnit = (WindUnit)weatherData.Body.User.AdminData.WindUnit;

                foreach (var deviceDto in weatherData.Body.Devices)
                {
                    var station = user.Stations.SingleOrDefault(s => s.Devices.Select(d => d.Id).Contains(deviceDto.Id));
                    if (station == null)
                    {
                        station = new Station { Devices = new List<Models.Device>() };
                        user.Stations.Add(station);
                    }

                    station.Name = deviceDto.StationName;
                    station.Altitude = deviceDto.Place.Altitude;
                    station.City = deviceDto.Place.City;
                    station.CountryCode = deviceDto.Place.Country;
                    station.Latitude = deviceDto.Place.Location[0];
                    station.Longitude = deviceDto.Place.Location[1];
                    station.Timezone = deviceDto.Place.Timezone;

                    var mainModule = station.Devices.OfType<MainDevice>().SingleOrDefault(d => d.Id == deviceDto.Id);
                    if (mainModule == null)
                    {
                        mainModule = new MainDevice
                        {
                            Id = deviceDto.Id,
                            DashboardData = new List<DashboardData>()
                        };
                        station.Devices.Add(mainModule);
                    }

                    mainModule.Name = deviceDto.ModuleName;
                    mainModule.Firmware = deviceDto.FirmwareVersion;
                    mainModule.WifiStatus = deviceDto.WifiStatus;
                    mainModule.DashboardData.Add(Convert2MainDashboardData(deviceDto.DashboardData));

                    foreach (var moduleDto in deviceDto.Modules)
                    {
                        var module = station.Devices.OfType<ModuleDevice>().SingleOrDefault(d => d.Id == moduleDto.Id);
                        if (module == null)
                        {
                            module = new ModuleDevice
                            {
                                Id = moduleDto.Id,
                                Type = ConvertToModuleType(moduleDto.Type),
                                DashboardData = new List<DashboardData>()
                            };
                            station.Devices.Add(module);
                        }

                        module.Name = moduleDto.ModuleName;
                        module.Firmware = moduleDto.FirmwareVersion;
                        module.RfStatus = moduleDto.RFStatus;
                        module.BatteryVp = moduleDto.BatteryPower;
                        module.BatteryPercent = moduleDto.BatteryPercentage;
                        module.DashboardData.Add(Convert2ModuleDashboardData(moduleDto));
                    }
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
            using (var response = await httpClient.PostAsync("https://api.netatmo.com/oauth2/token", new FormUrlEncodedContent(nameValueCollection)))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<Authorization>();
            }
        }

        private async Task<WeatherData> GetStationData(string accessToken)
        {
            var nameValueCollection = new Dictionary<string, string>
            {
                { "access_token", accessToken }
            };
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            using (var response = await httpClient.PostAsync("https://api.netatmo.com/api/getstationsdata", new FormUrlEncodedContent(nameValueCollection)))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<WeatherData>();
            }
        }

        private ModuleDeviceType ConvertToModuleType(ModuleType type)
        {
            switch (type)
            {
                case ModuleType.Indoor:
                    return ModuleDeviceType.Indoor;
                case ModuleType.Outdoor:
                    return ModuleDeviceType.Outdoor;
                case ModuleType.RainGauge:
                    return ModuleDeviceType.RainGauge;
                case ModuleType.WindGauge:
                    return ModuleDeviceType.WindGauge;
                default:
                    throw new InvalidCastException("The module type is not valid.");    // this should never happen
            }
        }

        private MainDashboardData Convert2MainDashboardData(MainModuleDashboardData data)
        {
            return new MainDashboardData
            {
                TimeUtc = DateTimeOffset.FromUnixTimeSeconds(data.TimeUtc).DateTime,
                Temperature = data.Temperature,
                TemperatureMin = data.MinTemperature,
                TemperatureMinTimestamp = DateTimeOffset.FromUnixTimeSeconds(data.MinTemperatureTimeUtc).DateTime,
                TemperatureMax = data.MaxTemperature,
                TemperatureMaxTimestamp = DateTimeOffset.FromUnixTimeSeconds(data.MaxTemperatureTimeUtc).DateTime,
                TemperatureTrend = (Models.Trend)data.TemperatureTrend,
                Pressure = data.Pressure,
                AbsolutePressure = data.AbsolutePressure,
                PressureTrend = (Models.Trend)data.PressureTrend,
                CO2 = data.CO2,
                Humidity = data.Humidity,
                Noise = data.Noise
            };
        }

        private DashboardData Convert2ModuleDashboardData(Module module)
        {
            var outdoorModule = module as OutdoorModule;
            var windGaugeModule = module as WindGaugeModule;
            var rainGaugeModule = module as RainGaugeModule;
            var indoorModule = module as IndoorModule;
            if (outdoorModule != null)
            {
                return Convert2OutdoorDashboardData(outdoorModule.DashboardData);
            }

            if (windGaugeModule != null)
            {
                return Convert2WindGaugeDashboardData(windGaugeModule.DashboardData);
            }

            if (rainGaugeModule != null)
            {
                return Convert2RainGaugeDashboardData(rainGaugeModule.DashboardData);
            }

            if (indoorModule != null)
            {
                return Convert2IndoorDashboardData(indoorModule.DashboardData);
            }

            throw new InvalidCastException("The module is not valid.");    // this should never happen
        }

        private OutdoorDashboardData Convert2OutdoorDashboardData(OutdoorModuleDashboardData data)
        {
            return new OutdoorDashboardData
            {
                TimeUtc = DateTimeOffset.FromUnixTimeSeconds(data.TimeUtc).DateTime,
                Temperature = data.Temperature,
                TemperatureMin = data.MinTemperature,
                TemperatureMinTimestamp = DateTimeOffset.FromUnixTimeSeconds(data.MinTemperatureTimeUtc).DateTime,
                TemperatureMax = data.MaxTemperature,
                TemperatureMaxTimestamp = DateTimeOffset.FromUnixTimeSeconds(data.MaxTemperatureTimeUtc).DateTime,
                TemperatureTrend = (Models.Trend)data.TemperatureTrend,
                Humidity = data.Humidity
            };
        }

        private WindGaugeDashboardData Convert2WindGaugeDashboardData(WindGaugeModuleDashboardData data)
        {
            return new WindGaugeDashboardData
            {
                TimeUtc = DateTimeOffset.FromUnixTimeSeconds(data.TimeUtc).DateTime,
                WindStrength = data.WindStrength,
                WindAngle = data.WindAngle,
                GustStrength = data.GustStrength,
                GustAngle = data.GustAngle
            };
        }

        private RainGaugeDashboardData Convert2RainGaugeDashboardData(RainGaugeModuleDashboardData data)
        {
            return new RainGaugeDashboardData
            {
                TimeUtc = DateTimeOffset.FromUnixTimeSeconds(data.TimeUtc).DateTime,
                Rain = data.Rain,
                Sum24H = data.SumRainOver24h,
                Sum1H = data.SumRainOver1h
            };
        }

        private IndoorDashboardData Convert2IndoorDashboardData(IndoorModuleDashboardData data)
        {
            return new IndoorDashboardData
            {
                TimeUtc = DateTimeOffset.FromUnixTimeSeconds(data.TimeUtc).DateTime,
                Temperature = data.Temperature,
                TemperatureMin = data.MinTemperature,
                TemperatureMinTimestamp = DateTimeOffset.FromUnixTimeSeconds(data.MinTemperatureTimeUtc).DateTime,
                TemperatureMax = data.MaxTemperature,
                TemperatureMaxTimestamp = DateTimeOffset.FromUnixTimeSeconds(data.MaxTemperatureTimeUtc).DateTime,
                TemperatureTrend = (Models.Trend)data.TemperatureTrend,
                CO2 = data.CO2,
                Humidity = data.Humidity
            };
        }
    }
}
