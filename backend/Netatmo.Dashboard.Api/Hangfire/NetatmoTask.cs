using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using Netatmo.Dashboard.Api.Options;
using Netatmo.Dashboard.Application.Interfaces;
using Netatmo.Dashboard.Application.Netatmo.DTOs;
using Netatmo.Dashboard.Application.Stations.Commands.CreateStation;
using Netatmo.Dashboard.Application.Stations.Commands.UpdateStation;
using Netatmo.Dashboard.Application.Stations.Queries;
using Netatmo.Dashboard.Application.Users.Commands.UpdateUser;
using Netatmo.Dashboard.Application.Users.Queries;
using Netatmo.Dashboard.Domain;
using Netatmo.Dashboard.Infrastructure.Options;
using Unit = Netatmo.Dashboard.Domain.Unit;
using UserDto = Netatmo.Dashboard.Application.Users.DTOs.UserDto;

namespace Netatmo.Dashboard.Api.Hangfire
{
    public class NetatmoTask
    {
        private readonly IMediator mediator;
        private readonly INetatmoService netatmoService;
        private readonly IOptionsMonitor<NetatmoOptions> netatmoOptions;
        private readonly IOptionsMonitor<GoogleMapsOptions> googleMapsOptions;

        public NetatmoTask(IMediator mediator, INetatmoService netatmoService, IOptionsMonitor<NetatmoOptions> netatmoOptions, IOptionsMonitor<GoogleMapsOptions> googleMapsOptions)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.netatmoService = netatmoService ?? throw new ArgumentNullException(nameof(netatmoService));
            this.netatmoOptions = netatmoOptions ?? throw new ArgumentNullException(nameof(netatmoOptions));
            this.googleMapsOptions = googleMapsOptions ?? throw new ArgumentNullException(nameof(googleMapsOptions));
        }

        public void FetchAndUpdate(string uid)
        {
            var task = FetchAndUpdateAsync(uid);
            task.GetAwaiter().GetResult();
        }

        private async Task FetchAndUpdateAsync(string uid)
        {
            var user = await mediator.Send(new GetUserByUidQuery { Uid = uid });
            var (accessToken, accessTokenUpdated) = await EnsureValidAccessToken(user);
            var weatherData = await netatmoService.GetStationData(accessToken);

            bool unitsUpdated = UpdateUnits(user, weatherData.Body.User.AdminData);
            if (accessTokenUpdated || unitsUpdated)
            {
                var updateUserCommand = Mapper.Map<UpdateUserCommand>(user);
                await mediator.Send(updateUserCommand);
            }

            foreach (var deviceDto in weatherData.Body.Devices)
            {
                var station = await mediator.Send(new GetStationByDeviceIdQuery { DeviceId = deviceDto.Id, ThrowWhenNotFound = false });
                int stationId;
                if (station == null)
                {
                    // TODO: send CreateStationCommand
                    stationId = await mediator.Send(new CreateStationCommand
                    {
                        Name = deviceDto.StationName,
                        Altitude = deviceDto.Place.Altitude,
                        City = deviceDto.Place.City,
                        CountryCode = deviceDto.Place.Country,
                        Latitude = deviceDto.Place.Location[0],
                        Longitude = deviceDto.Place.Location[1],
                        Timezone = deviceDto.Place.Timezone,
                        StaticMap = BuildStaticMapUrl(deviceDto.Place.Location[0], deviceDto.Place.Location[1]),
                        UserId = user.Id
                    });
                }
                else
                {
                    stationId = station.Id;
                    await mediator.Send(new UpdateStationCommand
                    {
                        Id = stationId,
                        Name = deviceDto.StationName,
                        Altitude = deviceDto.Place.Altitude,
                        City = deviceDto.Place.City,
                        CountryCode = deviceDto.Place.Country,
                        Latitude = deviceDto.Place.Location[0],
                        Longitude = deviceDto.Place.Location[1],
                        Timezone = deviceDto.Place.Timezone,
                        StaticMap = BuildStaticMapUrl(deviceDto.Place.Location[0], deviceDto.Place.Location[1])
                    });
                }

                // TODO: update modules & add weather data
            }
        }

        private async Task<(string accessToken, bool hasChanged)> EnsureValidAccessToken(UserDto user)
        {
            if (!user.ExpiresAt.HasValue)
            {
                return (null, false);
            }

            var hasChanged = false;
            if (user.ExpiresAt.Value <= DateTime.Now)
            {
                var authorization = await netatmoService.RefreshToken(user.RefreshToken);
                user.AccessToken = authorization.AccessToken;
                user.RefreshToken = authorization.RefreshToken;
                user.ExpiresAt = DateTime.Now.AddSeconds(authorization.ExpiresIn);
                hasChanged = true;
            }

            return (user.AccessToken, hasChanged);
        }

        private bool UpdateUnits(Application.Users.DTOs.UserDto user, UserAdminDataDto adminData)
        {
            var feelLike = (FeelLikeAlgo)adminData.FeelLikeAlgo;
            var pressureUnit = (PressureUnit)adminData.PressureUnit;
            var unit = (Unit)adminData.Unit;
            var windUnit = (WindUnit)adminData.WindUnit;
            var hasChanged = user.FeelLike != feelLike || user.PressureUnit != pressureUnit || user.Unit != unit || user.WindUnit != windUnit;
            user.FeelLike = feelLike;
            user.PressureUnit = pressureUnit;
            user.Unit = unit;
            user.WindUnit = windUnit;
            return hasChanged;
        }

        private string BuildStaticMapUrl(decimal latitude, decimal longitude)
        {
            var key = googleMapsOptions.CurrentValue.Key;
            var location = $"{latitude},{longitude}";
            var staticUrl = $"https://maps.googleapis.com/maps/api/staticmap?size=400x200&scale=2&maptype=roadmap&markers={location}&zoom=15&key={key}";
            return SignStaticMapUrl(staticUrl);
        }

        private string SignStaticMapUrl(string staticUrl)
        {
            var safeSecret = Decode(googleMapsOptions.CurrentValue.Secret);
            var uri = new Uri(staticUrl);
            var signature = Encode(Sign(safeSecret, Encoding.UTF8.GetBytes(uri.PathAndQuery)));
            return $"{uri}&signature={signature}";
        }

        private static byte[] Decode(string data)
        {
            var unsafeData = data.Replace('-', '+').Replace('_', '/');
            return Convert.FromBase64String(unsafeData);
        }

        private static string Encode(byte[] data)
        {
            var unsafeData = Convert.ToBase64String(data);
            return unsafeData.Replace('+', '-').Replace('/', '_');
        }

        private static byte[] Sign(byte[] key, byte[] data)
        {
            using (var hmac = new HMACSHA1(key))
            {
                return hmac.ComputeHash(data);
            }
        }
    }
}
