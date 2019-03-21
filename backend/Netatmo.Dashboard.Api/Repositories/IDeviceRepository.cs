using Netatmo.Dashboard.Api.Models;
using System.Threading.Tasks;

namespace Netatmo.Dashboard.Api.Repositories
{
    public interface IDeviceRepository
    {
        Task<Device[]> GetAll(int stationId);
        Task<Device> GetOne(string deviceId);
    }
}
