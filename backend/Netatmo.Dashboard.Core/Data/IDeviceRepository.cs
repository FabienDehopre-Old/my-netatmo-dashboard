using Netatmo.Dashboard.Core.Models;
using System.Threading.Tasks;

namespace Netatmo.Dashboard.Core.Data
{
    public interface IDeviceRepository
    {
        Task<Device[]> GetAll(int stationId);
        Task<Device> GetOne(string deviceId);
    }
}
