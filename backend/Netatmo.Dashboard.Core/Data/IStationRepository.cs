using Netatmo.Dashboard.Core.Models;
using System.Threading.Tasks;

namespace Netatmo.Dashboard.Core.Data
{
    public interface IStationRepository
    {
        Task<Station[]> GetAll();
        Task<Station[]> GetAll(string countryCode);
        Task<Station> GetOne(int id);
    }
}
