using Netatmo.Dashboard.Api.Models;
using System.Threading.Tasks;

namespace Netatmo.Dashboard.Api.Repositories
{
    public interface ICountryRepository
    {
        Task<Country[]> GetAll();
        Task<Country> GetOne(string alpha2Code);
    }
}
