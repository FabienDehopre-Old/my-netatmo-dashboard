using Netatmo.Dashboard.Application.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace Netatmo.Dashboard.Application.Interfaces
{
    public interface INetatmoService
    {
        AuthorizeUrl BuildAuthorizationUrl(string returnUrl);
        Task<Authorization> ExchangeCodeForAccessToken(ExchangeCode exchangeCode, CancellationToken cancellationToken = default);
        Task<Authorization> RefreshToken(string refreshToken, CancellationToken cancellationToken = default);
        Task<WeatherData> GetStationData(string accessToken, CancellationToken cancellationToken = default);
    }
}
