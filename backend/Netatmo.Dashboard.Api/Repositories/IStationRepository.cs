using Netatmo.Dashboard.Api.Models;
using System.Threading.Tasks;

namespace Netatmo.Dashboard.Api.Repositories
{
    public interface IStationRepository
    {
        Task<Station[]> GetAll();
        Task<Station[]> GetAll(string countryCode);
        Task<Station> GetOne(int id);
    }
}
