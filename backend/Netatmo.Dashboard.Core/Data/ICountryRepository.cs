using Netatmo.Dashboard.Core.Models;
using System.Threading.Tasks;

namespace Netatmo.Dashboard.Core.Data
{
    public interface ICountryRepository
    {
        Task<Country[]> GetAll();
        Task<Country> GetOne(string alpha2Code);
    }
}
