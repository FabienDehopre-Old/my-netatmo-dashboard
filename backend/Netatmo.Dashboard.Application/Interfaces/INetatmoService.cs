using Netatmo.Dashboard.Application.Netatmo.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace Netatmo.Dashboard.Application.Interfaces
{
    public interface INetatmoService
    {
        AuthorizeUrlDto BuildAuthorizationUrl(string returnUrl);
        Task<AuthorizationDto> ExchangeCodeForAccessToken(ExchangeCodeDto exchangeCode, CancellationToken cancellationToken = default);
        Task<AuthorizationDto> RefreshToken(string refreshToken, CancellationToken cancellationToken = default);
        Task<WeatherDataDto> GetStationData(string accessToken, CancellationToken cancellationToken = default);
    }
}
