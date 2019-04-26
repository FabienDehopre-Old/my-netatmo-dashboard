using Netatmo.Dashboard.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Netatmo.Dashboard.Core.Data
{
    public interface ICountryRepository
    {
        Task<Country> GetOne(string alpha2Code, CancellationToken cancellationToken);
        Task<Country[]> GetAll(int? first, string afterCode, CancellationToken cancellationToken);
        Task<Country[]> GetAllReverse(int? last, string beforeCode, CancellationToken cancellationToken);
        Task<bool> GetHasNextPage(int? first, string afterCode, CancellationToken cancellationToken);
        Task<bool> GetHasPreviousPage(int? last, string beforeCode, CancellationToken cancellationToken);
        Task<int> GetTotalCount(CancellationToken cancellationToken);
    }
}
